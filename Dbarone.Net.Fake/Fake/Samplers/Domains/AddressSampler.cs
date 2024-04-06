using Dbarone.Net.Fake;

public class AddressSampler : ISampler<AddressInfo>
{
	BoxMullerTransform StreetNumberSampler { get; set; }
	WeightedRandomSampler<string> StreetNamesSimpleSampler { get; set; }
	WeightedRandomSampler<string> StreetNamesCompoundSampler { get; set; }
	WeightedRandomSampler<string> StreetTypesSampler { get; set; }
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

	public AddressSampler() : base()
	{
		var streetNamesSimple = Dataset.GetData(DatasetEnum.en_GB_Street_Names_Simple).Select(d => new WeightedItem<string>(d, d => (string)d["Value"]));
		var streetNamesCompound = Dataset.GetData(DatasetEnum.en_GB_Street_Names_Compound).Select(d => new WeightedItem<string>(d, d => (string)d["Value"]));
		var streetTypes = Dataset.GetData(DatasetEnum.en_GB_Street_Types).Select(d => new WeightedItem<string>(d, d => (string)d["Value"]));
		var stochasticModelTowns = Dataset.GetString(DatasetEnum.Stochastic_Model_Town);
		var stochasticModelRegions = Dataset.GetString(DatasetEnum.Stochastic_Model_Region);

		// Create samplers
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
	}

	public IRandom<double> Random { get; set; } = new Lcg();

	public ulong Seed { get; set; }

	public AddressInfo Next()
	{
		int streetNumber = 0;
		do
		{
			streetNumber = Math.Abs((int)StreetNumberSampler.Next());
		} while (streetNumber == 0);

		// Street type - assume 5% are simple street names (like 'Westside') without a street type.
		var streetName = "";
		var rnd = Random.Next();
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

		return new AddressInfo
		{
			AddressLine1 = $"{streetNumber} {streetName}",
			Town = town,
			Region = TownRegion[town],
			Country = country
		};
	}
}