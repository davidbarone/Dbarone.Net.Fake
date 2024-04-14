/// <summary>
/// Represents a fake product in a store inventory.
/// </summary>
public class ProductInfo {
    public string Sku { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Colour { get; set; }
    public string Category { get; set; }
    public string IntroducedDate { get; set; }
    public string DiscontinuedDate { get; set; }
    public double ListPrice { get; set; }
    public int SafetyStockLevel { get; set; }
}