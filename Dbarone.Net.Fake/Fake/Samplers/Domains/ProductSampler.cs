namespace Dbarone.Net.Fake;

/// <summary>
/// Generates fake products from a fake Scandanavian homewares store.
/// </summary>
public class ProductSampler : ISampler<ProductInfo>
{
    public IRandom<double> Random { get; set; } = new Lcg();

    public ProductSampler()
    {

    }

    public ProductInfo Next()
    {
        throw new NotImplementedException();
    }
}