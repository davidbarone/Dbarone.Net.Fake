namespace Dbarone.Net.Fake;

/// <summary>
/// Generates fake products from a fake Scandanavian homewares store.
/// </summary>
public class ProductSampler : AbstractSampler<ProductInfo>, ISampler<ProductInfo>
{
    public ProductSampler()
    {

    }

    public ProductInfo Next()
    {
        throw new NotImplementedException();
    }
}