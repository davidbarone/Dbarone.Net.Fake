namespace Dbarone.Net.Fake;

/// <summary>
/// Represents a fake person.
/// </summary>
public class PersonInfo
{
    public int PersonId { get; set; }
    public string FirstName { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public char Sex { get; set; }
    public DateTime DoB { get; set; }
}