using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class PersonSamplerTests
{

    [Fact]
    public void TestPersonSampler()
    {
        List<PersonInfo> persons = new List<PersonInfo>();
        PersonSampler ps = new PersonSampler();

        for (int i = 0; i < 10; i++)
        {
            persons.Add(ps.Next());
        }
    }

    /// <summary>
    /// This method is used to generate customers for Enorab test dataset.
    /// 50/50 split male/female, average age = 50, stdDev = 10 years.
    /// </summary>
    [Fact]
    public void Create1000People()
    {
        List<PersonInfo> persons = new List<PersonInfo>();
        PersonSampler ps = new PersonSampler();

        for (int i = 0; i < 1000; i++)
        {
            persons.Add(ps.Next());
        }
        var str = JsonConvert.SerializeObject(persons);
    }
}