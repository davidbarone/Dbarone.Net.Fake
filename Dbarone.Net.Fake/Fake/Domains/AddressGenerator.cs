using Dbarone.Net.Fake;

public class AddressGenerator
{
    public IRandom<double> Random { get; set; } = new Lcg();

    public int Seed { get; set; }

    public AddressInfo Next()
    {

        // Street number
        BoxMullerTransform bm = new BoxMullerTransform();
        bm.Seed = this.Seed;
        bm.Random = this.Random;
        bm.Mean = 0.005;
        bm.StdDev = 0.2;
        var streetNumber = bm.Next() * 10000;

        // Street names simple
        var streetNamesSimple = new WeightedListStrategy(WeightedListEnum.en_GB_Street_Names_Simple);
        var streetNamesCompound = new WeightedListStrategy(WeightedListEnum.en_GB_Street_Names_Compound);

        return null;


    }
}