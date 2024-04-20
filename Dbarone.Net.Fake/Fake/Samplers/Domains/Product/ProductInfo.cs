namespace Dbarone.Net.Fake;

/// <summary>
/// A fake product.
/// </summary>
public class ProductInfo
{
    /// <summary>
    /// Stock keeping unit.
    /// </summary>
    public int Sku { get; set; }

    /// <summary>
    /// The name of the product.
    /// </summary>
    public string ProductName { get; set; } = default!;

    /// <summary>
    /// The department that the product is sold from.
    /// </summary>
    public string Department { get; set; } = default!;

    /// <summary>
    /// The description of the product.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// The relative weight that the product type is sold.
    /// </summary>
    public double Popularity { get; set; }

    /// <summary>
    /// The mean price for the product type.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The standard cost of the product.
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// The main material used.
    /// </summary>
    public string Material { get; set; } = default!;

    /// <summary>
    /// The main color of the product.
    /// </summary>
    public string Color { get; set; } = default!;

    #region Fields not currently used

    public DateTime? IntroducedDate { get; set; }
    public DateTime? DiscontinuedDate { get; set; }
    public int SafetyStockLevel { get; set; }

    #endregion
    
    /// <summary>
    /// Creates a new product from a product type.
    /// </summary>
    /// <param name="productType">The product type.</param>
    public ProductInfo(ProductTypeInfo productType)
    {
        this.Department = productType.Department;
        this.Description = productType.Description;
        this.Popularity = productType.Popularity;
        this.Price = productType.Price;
    }
}