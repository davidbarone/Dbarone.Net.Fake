using System.Collections.Generic;
using Xunit;

namespace Dbarone.Net.Fake;

public class AddressSamplerTests {

    [Fact]
    public void TestAddresses() {
        List<AddressInfo> addresses = new List<AddressInfo>();
        AddressSampler sampler = new AddressSampler();
        for (int i=0; i<50; i++){
            addresses.Add(sampler.Next());
        }
        var a = 123;
    }
}