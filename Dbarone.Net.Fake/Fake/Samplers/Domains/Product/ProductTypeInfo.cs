namespace Dbarone.Net.Fake;

/// <summary>
/// A fake product type - used to generate fake products.
/// </summary>
public class ProductTypeInfo
{
    /// <summary>
    /// The department that the product is sold from.
    /// </summary>
    public string Department { get; set; } = default!;

    /// <summary>
    /// The description of the product.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// The relative weight with which to generate new products.
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// The relative weight that the product type is sold.
    /// </summary>
    public double Popularity { get; set; }

    /// <summary>
    /// The mean price for the product type.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The mean margin for the product type.
    /// </summary>
    public double Margin { get; set; }

    /// <summary>
    /// The standard deviation of the product type, expressed as a fraction of the price.
    /// </summary>
    public double Variance { get; set; }

    /// <summary>
    /// A comma-separated list of Material values for the product type.
    /// </summary>
    public string Materials { get; set; }

    /// <summary>
    /// A comma-separated list of color values for the product type.
    /// </summary>
    public string Colors { get; set; }

    public ProductTypeInfo(IDictionary<string, object> data)
    {
        this.Department = (string)data["Department"];
        this.Description = (string)data["Description"];
        this.Weight = double.Parse((string)data["Weight"]);
        this.Popularity = double.Parse((string)data["Popularity"]);
        this.Price = decimal.Parse((string)data["Price"]);
        this.Variance = double.Parse((string)data["Variance"]);
        this.Margin = double.Parse((string)data["Margin"]);
        this.Materials = (string)data["Materials"];
        this.Colors = (string)data["Colors"];
    }
}