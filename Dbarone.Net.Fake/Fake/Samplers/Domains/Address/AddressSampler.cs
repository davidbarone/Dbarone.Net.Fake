using Dbarone.Net.Fake;

public class AddressSampler : ISampler<AddressInfo>
{
	BoxMullerTransform StreetNumberSampler { get; set; }
	WeightedRandomSampler<string> StreetNamesSimpleSampler { get; set; }
	WeightedRandomSampler<string> StreetNamesCompoundSampler { get; set; }
	WeightedRandomSampler<string> StreetTypesSampler { get; set; }
	WeightedRandomSampler<string> FlatStyleSampler { get; set; }
	WeightedRandomSampler<string> FlatStyleASampler { get; set; }
	BoxMullerTransform FlatStyleBSampler { get; set; }
	MarkovChainSampler TownSampler { get; set; }
	MarkovChainSampler RegionSampler { get; set; }
	WeightedRandomSampler<string> CountrySampler { get; set; }

	/// <summary>
	/// Cache of regions for each town. Ensures that we maintain a M:1 relationship
	/// </summary>
	Dictionary<string, string> TownRegion { get; set; }

	/// <summary>
	/// Cache of country for each town. Ensures that we maintain a M:1 relationship
	/// </summary>
	Dictionary<string, string> RegionCountry { get; set; }

	/// <summary>
	/// Postcode comprises 5 digits 'ddddd'
	/// </summary>
	Dictionary<string, int> RegionPostcode { get; set; }	// region provides 2 digits of postcode
	Dictionary<string, int> TownPostcode { get; set; }	// Town provides 2 digits of postcode
	Dictionary<string, int> StreetPostcode { get; set; }	// Street provides 1 digit of postcode

	public AddressSampler() : base()
	{
		var streetNamesSimple = Dataset.GetData(DatasetEnum.en_GB_Street_Names_Simple).Select(d => new WeightedItem<string>(d, d => (string)d["Value"]));
		var streetNamesCompound = Dataset.GetData(DatasetEnum.en_GB_Street_Names_Compound).Select(d => new WeightedItem<string>(d, d => (string)d["Value"]));
		var streetTypes = Dataset.GetData(DatasetEnum.en_GB_Street_Types).Select(d => new WeightedItem<string>(d, d => (string)d["Value"]));
		var stochasticModelTowns = Dataset.GetString(DatasetEnum.Stochastic_Model_Town);
		var stochasticModelRegions = Dataset.GetString(DatasetEnum.Stochastic_Model_Region);

		// Create Flat samplers
		// Style A: '123b Main Street'
		FlatStyleASampler = new WeightedRandomSampler<string>(this.Random,
			new WeightedItem<string>("a", 34),
			new WeightedItem<string>("b", 34),
			new WeightedItem<string>("c", 13.5),
			new WeightedItem<string>("d", 13.5),
			new WeightedItem<string>("e", 2.5),
			new WeightedItem<string>("f", 2.5)
			);

		// Style B: Flat 4, 123 Main Street
		FlatStyleBSampler = new BoxMullerTransform(this.Random, 0, 10);
		FlatStyleSampler = new WeightedRandomSampler<string>(
			this.Random,
			new WeightedItem<string>("A", 10),
			new WeightedItem<string>("B", 90)
		);

		StreetNumberSampler = new BoxMullerTransform(this.Random, 0, 300);
		StreetNamesSimpleSampler = new WeightedRandomSampler<string>(streetNamesSimple, Random);
		StreetNamesCompoundSampler = new WeightedRandomSampler<string>(streetNamesCompound, Random);
		StreetTypesSampler = new WeightedRandomSampler<string>(streetTypes, Random);
		var townModel = MarkovChainModel.Deserialise(stochasticModelTowns);
		TownSampler = new MarkovChainSampler(townModel);
		var regionModel = MarkovChainModel.Deserialise(stochasticModelRegions);
		RegionSampler = new MarkovChainSampler(regionModel);

		TownRegion = new Dictionary<string, string>();
		RegionCountry = new Dictionary<string, string>();

		CountrySampler = new WeightedRandomSampler<string>(
			new WeightedItem<string>("England", 57),
			new WeightedItem<string>("Scotland", 5),
			new WeightedItem<string>("Wales", 3),
			new WeightedItem<string>("Northern Ireland", 2)
		);

		RegionPostcode = new Dictionary<string, int>();
		TownPostcode = new Dictionary<string, int>();
		StreetPostcode = new Dictionary<string, int>();
	}

	public IRandom<double> Random { get; init; } = new Lcg();

	public AddressInfo Next()
	{
		var rnd = Random.Next();

		var isFlat = rnd < 0.2; // 20% addresses deemed to be flats
		string flatStyle = "";  // two types of flats, ones with 123a, 123b addresses, and the others labelled 'Flat 1', 'Flat 2' etc.
		string flatSuffix = ""; // a,b,c,d etc.
		int flatNumber = 0; // Flat 1, Flat 2 etc.

		if (isFlat)
		{
			flatStyle = FlatStyleSampler.Next();
			if (flatStyle == "A") { flatSuffix = FlatStyleASampler.Next(); }
			if (flatStyle == "B")
			{
				do
				{
					flatNumber = Math.Abs((int)FlatStyleBSampler.Next());
				} while (flatNumber == 0);
			}
		}

		int streetNumber = 0;
		do
		{
			streetNumber = Math.Abs((int)StreetNumberSampler.Next());
		} while (streetNumber == 0);

		// Street type - assume 5% are simple street names (like 'Westside') without a street type.
		var streetName = "";
		if (rnd < 0.05)
		{
			// simple street name
			streetName = StreetNamesSimpleSampler.Next();
		}
		else
		{
			streetName = StreetNamesCompoundSampler.Next() + " " + StreetTypesSampler.Next();
		}

		// Post Town
		var town = TownSampler.Next();

		// Region
		var region = RegionSampler.Next();
		if (TownRegion.ContainsKey(town))
		{
			region = TownRegion[town];
		}
		else
		{
			TownRegion[town] = region;
		}

		// Country
		var country = CountrySampler.Next();
		if (RegionCountry.ContainsKey(region))
		{
			country = RegionCountry[region];
		}
		else
		{
			RegionCountry[region] = country;
		}

		// Postcode
		var regionPostcode = (int)(10 + (rnd * 90));	// 10-99
		if (regionPostcode >=100) {
			throw new Exception("Whoops");
		}
		if (RegionPostcode.ContainsKey(region)) {
			regionPostcode = RegionPostcode[region]; 
		} else {
			RegionPostcode[region] = regionPostcode;
		}

		var townPostcode = (int)(rnd * 100);	// 0-99
		if (townPostcode >=100) {
			throw new Exception("Whoops");
		}
		if (TownPostcode.ContainsKey(town)) {
			townPostcode = TownPostcode[town]; 
		} else {
			TownPostcode[town] = townPostcode;
		}

		var streetPostcode = (int)(rnd * 10);	// 0-9
		if (streetPostcode >=10) {
			throw new Exception("Whoops");
		}
		if (StreetPostcode.ContainsKey(streetName)) {
			streetPostcode = StreetPostcode[streetName]; 
		} else {
			StreetPostcode[streetName] = streetPostcode;
		}

		int postcode = (regionPostcode * 1000) + (townPostcode * 10) + streetPostcode;
		
		return new AddressInfo
		{
			AddressLine1 = flatNumber != 0 ? $"Flat {flatNumber}" : $"{streetNumber}{flatSuffix} {streetName}",
			AddressLine2 = flatNumber != 0 ? $"{streetNumber}{flatSuffix} {streetName}" : "",
			Town = town,
			Region = TownRegion[town],
			Postcode = postcode.ToString(),
			Country = country
		};
	}
}