namespace Dbarone.Net.Fake;

/// <summary>
/// Generates fake products from a fake Scandanavian homewares store.
/// </summary>
public class ProductSampler : AbstractSampler<ProductInfo>, ISampler<ProductInfo>
{
    WeightedRandomSampler<ProductTypeInfo> ProductTypesSampler { get; set; } = default!;
    UniqueSampler<string> UniqueScandanavianWordSampler { get; set; } = default!;
    SequenceSampler SequenceSampler = new SequenceSampler(100001, 1.05);

    public ProductSampler()
    {
        // We use the product_types.csv file, which contains weighting of different types of products
        // From this we create actual products, using a product name from a Markov model using
        // the Scandanavian_Words file.
        var productTypes = Dataset.GetData(DatasetEnum.Product_Types).Select(d => new WeightedItem<ProductTypeInfo>(d, d => new ProductTypeInfo(d)));
        ProductTypesSampler = new WeightedRandomSampler<ProductTypeInfo>(productTypes, Random);

        // Sampler for product names
        var data = Dataset.GetData(DatasetEnum.Scandanavian_Words).Select(w => (string)w["Word"]).ToList();
        var corpus = string.Join("|", data);

        MarkovChainTrainer trainer = new MarkovChainTrainer();
        MarkovChainTrainerConfiguration configuration = new MarkovChainTrainerConfiguration
        {
            Order = 3,
            WordDelimiters = new string[] { "|" },
            Level = MarkovChainLevel.Character
        };
        var model = trainer.Train(corpus, configuration);
        MarkovChainSampler sampler = new MarkovChainSampler(model);
        UniqueScandanavianWordSampler = new UniqueSampler<string>(sampler);
    }

    public override ProductInfo Next()
    {
        var productType = ProductTypesSampler.Next();
        var product = new ProductInfo(productType);
 
        // Sku
        product.Sku = SequenceSampler.Next();

       // Product Name
       product.ProductName = UniqueScandanavianWordSampler.Next();

        // Randomise the variance
        BoxMullerTransform bmVariance = new BoxMullerTransform(this.Random, productType.Variance, (productType.Variance * productType.Variance));
        productType.Variance = bmVariance.Next();

        // calculate actual price.
        BoxMullerTransform bmPrice = new BoxMullerTransform(this.Random, (double)productType.Price, ((double)productType.Price * productType.Variance));
        product.Price = decimal.Round((decimal)bmPrice.Next(), 2);

        // Calculate margin
        BoxMullerTransform bmMargin = new BoxMullerTransform(this.Random, productType.Margin, (productType.Margin * productType.Variance));
        productType.Margin = bmMargin.Next();

        // Calculate Cost (Price * (1-Margin))
        product.Cost = decimal.Round(product.Price * (decimal)(1 - productType.Margin), 2);

        // Material
        var materialSampler = new WeightedRandomSampler<string>(
            productType.Materials.Split(",").Select(m => new WeightedItem<string>(m, 1)),
            this.Random);

        product.Material = materialSampler.Next();

        // Color
        var colorSampler = new WeightedRandomSampler<string>(
            productType.Colors.Split(",").Select(m => new WeightedItem<string>(m, 1)),
            this.Random);

        product.Color = colorSampler.Next();

        return product;
    }
}