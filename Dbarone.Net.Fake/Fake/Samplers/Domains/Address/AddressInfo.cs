using Dbarone.Net.Fake;

public class AddressInfo : InfoObject
{
    /// <summary>
    /// Organisational department, sub building name,  Apartment / unit / flat number.
    /// </summary>
    public string AddressLine1 { get; set; } = default!;

    /// <summary>
    /// The building number and street name.
    /// </summary>
    public string AddressLine2 { get; set; } = default!;

    /// <summary>
    /// The postal town / city.
    /// </summary>
    public string Town { get; set; } = default!;

    /// <summary>
    /// The Region.
    /// </summary>
    public string Region { get; set; } = default!;

    /// <summary>
    /// The postcode.
    /// </summary>
    public string Postcode { get; set; } = default!;

    /// <summary>
    /// The country.
    /// </summary>
    public string Country { get; set; }

}