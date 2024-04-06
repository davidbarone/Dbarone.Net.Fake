using Dbarone.Net.Fake;

/// <summary>
/// Generates random people.
/// </summary>
public class PersonSampler : ISampler<PersonInfo>
{
    SequenceSampler personIdSampler;
    WeightedRandomSampler<char> sexSampler { get; set; }
    BoxMullerTransform ageSampler { get; set; }
    WeightedRandomSampler<string> girlNameSampler { get; set; }
    WeightedRandomSampler<string> boyNameSampler { get; set; }
    WeightedRandomSampler<string> surnameSampler { get; set; }
    double MinAge { get; set; }
    double MaxAge { get; set; }

    public PersonSampler() : this(1, 1, 50, 10, 18, 90) { }

    public PersonSampler(double maleWeight = 1, double femaleWeight = 1, double averageAge = 50, double stdDevAge = 10, double minAge = 18, double MaxAge = 90)
    {
        // PersonId sampler
        personIdSampler = new SequenceSampler(1.1);

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

    public IRandom<double> Random { get; set; }

    public PersonInfo Next()
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
}