using System.Collections.Generic;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class ProductSamplerTests {

    [Fact]
    public void TestProductSampler() {
        List<ProductInfo> products = new List<ProductInfo>();
        ProductSampler ps = new ProductSampler();

        for (int i = 0; i<100; i++) {
            products.Add(ps.Next());
        }
        var a = 1;
    }
}