using Dbarone.Net.Fake;

public class AddressGenerator
{
    public IRandom<double> Random { get; set; } = new Lcg();

    public int Seed { get; set; }

    public AddressInfo Next()
    {
        // Post towns + localities based UK Ordnance survey data.
		// Street names are randomised and any correlation with actual street names is purely accidental.
        // See:
		// https://en.wikipedia.org/wiki/List_of_postcode_areas_in_the_United_Kingdom_by_population
		// https://www.doogal.co.uk/PostcodeDistricts#google_vignette



/*
USE sandpit
GO

; WITH cteRaw
AS
(
	SELECT
		ROW_NUMBER() OVER (ORDER BY Postcode) SEQ,
		Postcode,
		Town_Area,
		Region,
		Households,
		UK_Region,
		Post_Town
	FROM
		PostcodeDistricts
	WHERE
		Households IS NOT NULL
	AND
		Town_Area <> 'Non-geographic'
)

, CTE2
AS
(
	SELECT
		SEQ,
		Postcode,
		CASE
			WHEN CHARINDEX('(', A.Value, 1) <> 0 OR  CHARINDEX(')', A.Value, 1) <> 0 THEN NULL ELSE TRIM(A.value)
		END Locality,
		Post_Town,
		Region,
		UK_Region,
		Households
	FROM
		cteRaw R1
	CROSS APPLY
		string_split(Town_Area,',') A
)

, CTE3
AS
(
	SELECT
		*,
		Households / (SELECT COUNT(*) FROM CTE2 T2 WHERE T2.SEQ = T1.SEQ) Weight
	FROM
		CTE2 T1
)

SELECT
	SEQ,
	Postcode,
	CASE
		WHEN Post_Town NOT LIKE '%' + Locality + '%' AND Locality NOT LIKE '%' + Post_Town + '%'
		THEN Locality
		ELSE NULL
	END Locality,
	Post_Town,
	Region,
	UK_region,
	Weight/1000 Weight FROM CTE3
WHERE
	Weight/1000 >= 1
ORDER BY
	Weight
*/

        
        // Assume that all postcode districts and post towns / districts have uniform population.

        // Street number
        BoxMullerTransform bm = new BoxMullerTransform();
        bm.Seed = this.Seed;
        bm.Random = this.Random;
        bm.Mean = 0.005;
        bm.StdDev = 0.2;
        var streetNumber = bm.Next() * 10000;

        // Street names simple
        var streetNamesSimple = new WeightedRandomSampler<string>(
			Dataset.GetData(DatasetEnum.en_GB_Street_Names_Simple),
			this.Random,
			(IDictionary<string, object> data, )
        var streetNamesCompound = new WeightedListStrategy(WeightedListEnum.en_GB_Street_Names_Compound);

        return null;


    }
}