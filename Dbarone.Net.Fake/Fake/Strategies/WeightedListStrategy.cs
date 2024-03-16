using System.Reflection;

namespace Dbarone.Net.Fake;

/// <summary>
/// Returns data based on a weighted list of values.
/// </summary>
public class WeightedListStrategy : IFakerStrategy
{
    public IRandom Random { get; set; } = new Lcg();

    private WeightedListEnum _list;
    public WeightedListEnum List
    {
        get { return _list; }
        set
        {
            var list = value.ToString();
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var path = $"{assembly.GetName().Name}.Datasets.{list}";
            Stream resource = assembly.GetManifestResourceStream(path);
            var qq = 0;
        }

    }

    public object Next(int i, object? last = null)
    {
        throw new NotImplementedException();
    }
}