using Dbarone.Net.Fake;

/// <summary>
/// Generates random people.
/// </summary>
public class PersonSampler : AbstractSampler<PersonInfo>, ISampler<PersonInfo>
{
    #region Properties

    SequenceSampler personIdSampler;
    WeightedRandomSampler<char> sexSampler { get; set; }
    BoxMullerTransform ageSampler { get; set; }
    WeightedRandomSampler<string> girlNameSampler { get; set; }
    WeightedRandomSampler<string> boyNameSampler { get; set; }
    WeightedRandomSampler<string> surnameSampler { get; set; }
    double MinAge { get; set; }
    double MaxAge { get; set; }

    #endregion

    #region Ctor

    public PersonSampler() : base()
    {
        // PersonId sampler
        personIdSampler = new SequenceSampler(1, 1.1);

        // Sex + Surname to have different seeds to ensure low entropy between the 2 variables:
        var randomSex = new Lcg(1);
        var randomSurname = new Lcg(2);

        // Sex sampler
        sexSampler = new WeightedRandomSampler<char>(
            randomSex,
            new WeightedItem<char>('M', 1),
            new WeightedItem<char>('F', 1)
        );

        // Age sampler
        ageSampler = new BoxMullerTransform(new Lcg(), 50, 10);

        // Girls name sample
        girlNameSampler = new WeightedRandomSampler<string>(
            Dataset.GetData(DatasetEnum.en_GB_Names_Girl)
            .Select(d => new WeightedItem<string>(d, d => (string)d["Value"])));

        // Boys name sample
        boyNameSampler = new WeightedRandomSampler<string>(
            Dataset.GetData(DatasetEnum.en_GB_Names_Boy)
            .Select(d => new WeightedItem<string>(d, d => (string)d["Value"])));

        // Surname sample
        surnameSampler = new WeightedRandomSampler<string>(
            Dataset.GetData(DatasetEnum.Surnames_US_Census_2010)
            .Select(d => new WeightedItem<string>(d, d => (string)d["Value"])),
            randomSurname
            );

        MinAge = 10;
        MaxAge = 90;
    }

    public PersonSampler(IRandom<double> random, double maleWeight = 1, double femaleWeight = 1, double averageAge = 50, double stdDevAge = 10, double minAge = 18, double MaxAge = 90) : base(random)
    {
        // PersonId sampler
        personIdSampler = new SequenceSampler(1, 1.1);

        // Sex sampler
        sexSampler = new WeightedRandomSampler<char>(
            new WeightedItem<char>('M', maleWeight),
            new WeightedItem<char>('F', femaleWeight)
        );

        // Age sampler
        ageSampler = new BoxMullerTransform(new Lcg(), averageAge, stdDevAge);

        // Girls name sample
        girlNameSampler = new WeightedRandomSampler<string>(
            Dataset.GetData(DatasetEnum.en_GB_Names_Girl)
            .Select(d => new WeightedItem<string>(d, d => (string)d["Value"])));

        // Boys name sample
        boyNameSampler = new WeightedRandomSampler<string>(
            Dataset.GetData(DatasetEnum.en_GB_Names_Boy)
            .Select(d => new WeightedItem<string>(d, d => (string)d["Value"])));

        // Surname sample
        surnameSampler = new WeightedRandomSampler<string>(
            Dataset.GetData(DatasetEnum.Surnames_US_Census_2010)
            .Select(d => new WeightedItem<string>(d, d => (string)d["Value"])));
    }

    #endregion

    #region Methods

    public override PersonInfo Next()
    {
        double daysInYear = 365.2425;
        var sex = sexSampler.Next();
        double age;
        do
        {
            age = ageSampler.Next();
        } while (age < MinAge || age > MaxAge);

        return new PersonInfo
        {
            PersonId = personIdSampler.Next(),
            Sex = sex,
            FirstName = (sex == 'M') ? boyNameSampler.Next() : girlNameSampler.Next(),
            Surname = surnameSampler.Next(),
            DoB = DateTime.Now.AddDays(age * daysInYear * -1).Date
        };
    }

    #endregion
}