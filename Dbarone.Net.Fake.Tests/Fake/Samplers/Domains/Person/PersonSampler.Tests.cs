using System.Collections.Generic;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class PersonSamplerTests {

    [Fact]
    public void TestPersonSampler() {
        List<PersonInfo> persons = new List<PersonInfo>();
        PersonSampler ps = new PersonSampler();

        for (int i = 0; i<10; i++) {
            persons.Add(ps.Next());
        }
    }
}