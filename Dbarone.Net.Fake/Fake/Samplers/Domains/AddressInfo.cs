public class AddressInfo
{
    /// <summary>
    /// Organisational department, sub building name,  Apartment / unit / flat number.
    /// </summary>
    public string AddressLine1 { get; set; }

    /// <summary>
    /// The building number and street name.
    /// </summary>
    public string AddressLine2 { get; set; }

    /// <summary>
    /// Optional locality to disinguish multiple similar streets in town.
    /// </summary>
    public string Locality { get; set; }
    
    /// <summary>
    /// The postal town / city.
    /// </summary>
    public string Town { get; set; }
    
    /// <summary>
    /// The Region.
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// The postcode.
    /// </summary>
    public string Postcode { get; set; }
    
    /// <summary>
    /// The country.
    /// </summary>
    public string Country { get; set; }

}