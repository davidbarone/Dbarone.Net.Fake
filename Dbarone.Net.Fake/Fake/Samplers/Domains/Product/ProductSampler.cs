namespace Dbarone.Net.Fake;

/// <summary>
/// Generates fake products from a fake Scandanavian homewares store.
/// </summary>
public class ProductSampler : AbstractSampler<ProductInfo>, ISampler<ProductInfo>
{
    WeightedRandomSampler<ProductInfo> ProductTypesSampler { get; set; } = default!;
    UniqueSampler<string> UniqueScandanavianWordSampler { get; set; } = default!;
    SequenceSampler SequenceSampler = new SequenceSampler(100000, 1.05);

    public ProductSampler()
    {
        // We use the product_types.csv file, which contains weighting of different types of products
        // From this we create actual products, using a product name from a Markov model using
        // the Scandanavian_Words file.
        var productTypes = Dataset.GetData(DatasetEnum.Product_Types).Select(d => new WeightedItem<ProductInfo>(d, d => new ProductInfo(d)));
        ProductTypesSampler = new WeightedRandomSampler<ProductInfo>(productTypes, Random);

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

    public ProductInfo Next()
    {
        var nextProduct = ProductTypesSampler.Next();
        nextProduct.ProductName = UniqueScandanavianWordSampler.Next();

        // Sku
        nextProduct.Sku = SequenceSampler.Next();

        // Randomise the variance
        BoxMullerTransform bmVariance = new BoxMullerTransform(this.Random, nextProduct.Variance, (nextProduct.Variance * nextProduct.Variance));
        nextProduct.Variance = bmVariance.Next();

        // calculate actual price.
        BoxMullerTransform bm = new BoxMullerTransform(this.Random, nextProduct.Price, (nextProduct.Price * nextProduct.Variance));
        nextProduct.Price = bm.Next();

        // Calculate margin
        BoxMullerTransform bmMargin = new BoxMullerTransform(this.Random, nextProduct.Margin, (nextProduct.Margin * nextProduct.Variance));
        nextProduct.Margin = bm.Next();

        // Calculate Cost (Price * (1-Margin))
        nextProduct.Cost = nextProduct.Price * (1 - nextProduct.Margin);

        // Material
        var materialSampler = new WeightedRandomSampler<string>(
            nextProduct.Material.Split(",").Select(m => new WeightedItem<string>(m, 1)),
            this.Random);

        nextProduct.Material = materialSampler.Next();

        // Color
        var colorSampler = new WeightedRandomSampler<string>(
            nextProduct.Color.Split(",").Select(m => new WeightedItem<string>(m, 1)),
            this.Random);

        nextProduct.Color = colorSampler.Next();

        return nextProduct;
    }
}