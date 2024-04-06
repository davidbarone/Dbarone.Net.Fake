using System.Reflection;
using Dbarone.Net.Csv;

namespace Dbarone.Net.Fake;

/// <summary>
/// Returns data based on a weighted list of values.
/// </summary>
public class WeightedRandomSampler<T> : ISampler<T>
{
    private IList<WeightedItem<T>>? data = null;

    public double TotalWeight { get; set; }

    public IRandom<double> Random { get; set; }

    public WeightedRandomSampler(IEnumerable<WeightedItem<T>> data) : this(data.ToList(), new Lcg()) { }

    public WeightedRandomSampler(params WeightedItem<T>[] data) : this(data.ToList(), new Lcg()) { }
    public WeightedRandomSampler(IRandom<double> random, params WeightedItem<T>[] data) : this(data.ToList(), random) { }

    public WeightedRandomSampler(
        IEnumerable<IDictionary<string, object>> data,
        IRandom<double> random,
        Func<IDictionary<string, object>, WeightedItem<T>> mapper) : this(data.Select(d => mapper(d)), random) { }

    public WeightedRandomSampler(IEnumerable<WeightedItem<T>> data, IRandom<double> random)
    {
        this.Random = random;
        this.data = data.ToList();
        this.TotalWeight = this.data.Sum(d => d.Weight);
    }



    public T Next()
    {
        // Get random number 0 <= x < TotalWeight
        var rand = this.Random.Next() * this.TotalWeight;
        var total = 0;
        T? value = default;
        for (int i = 0; i < this.data!.Count(); i++)
        {
            value = this.data![i].Value;
            total += (int)this.data![i].Weight;
            if (total > rand)
            {
                break;
            }
        }
        return value;
    }
}