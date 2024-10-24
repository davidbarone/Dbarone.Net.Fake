using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace Dbarone.Net.Fake;

public class CompanySamplerTests
{

    [Fact]
    public void TestCompanies()
    {
        List<string> companies = new List<string>();
        CompanySampler sampler = new CompanySampler(new Lcg(1));
        for (int i = 0; i < 10; i++)
        {
            companies.Add(sampler.Next());
        }
        var actuals = string.Join(",", companies);
        Assert.Equal("Sinotral Bank,Steel Serv,Risei Bank,Norwegistrancorp,Kore-Maries,First Avalurgutneft,UGI,Delefónics,Genero Gangolidas,Soup", actuals);
    }
}