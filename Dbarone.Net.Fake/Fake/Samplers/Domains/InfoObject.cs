namespace Dbarone.Net.Fake;

/// <summary>
/// Base class for all info objects.
/// </summary>
public class InfoObject
{
    /// <summary>
    /// Converts the current object to a dictionary.
    /// </summary>
    /// <returns></returns>
    public IDictionary<string, object?> ToDictionary()
    {
        var type = this.GetType();
        var properties = type.GetProperties();

        Dictionary<string, object?> dict = new Dictionary<string, object?>();
        foreach (var property in properties)
        {
            dict[property.Name] = property.GetValue(this);
        }
        return dict;
    }
}