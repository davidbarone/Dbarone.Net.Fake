namespace Dbarone.Net.Fake;

/// <summary>
/// Represents a fake person.
/// </summary>
public class PersonInfo
{
    /// <summary>
    /// A unique person surrogate key.
    /// </summary>
    public int PersonId { get; set; }
    
    /// <summary>
    /// The person's first / given name.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// The person's last name.
    /// </summary>
    public string Surname { get; set; } = default!;
    
    /// <summary>
    /// The person's sex.
    /// </summary>
    public char Sex { get; set; }

    /// <summary>
    /// The person's date of birth.
    /// </summary>
    public DateTime DoB { get; set; }
}