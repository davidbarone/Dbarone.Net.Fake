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

    public WeightedRandomSampler(IEnumerable<WeightedItem<T>> data)
    {
        this.Random = new Lcg();
        this.data = data.ToList();
        this.TotalWeight = this.data.Sum(d => d.Weight);
    }

    public WeightedRandomSampler(IEnumerable<WeightedItem<T>> data, IRandom<double> random)
    {
        this.Random = random;
        this.data = data.ToList();
        this.TotalWeight = this.data.Sum(d => d.Weight);
    }

    public WeightedRandomSampler(
        IEnumerable<IDictionary<string, object>> data,
        IRandom<double> random,
        Func<IDictionary<string, object>, WeightedItem<T>> mapper) : this(data.Select(d => mapper(d)), random) { }


    public T Next(int i, T? last = default)
    {
        // Get random number 0 <= x < TotalWeight
        var rand = this.Random.Next() * this.TotalWeight;
        var total = 0;
        T? value = default;
        for (int j = 0; j < this.data!.Count(); j++)
        {
            value = this.data![j].Value;
            total += (int)this.data![j].Weight;
            if (total > rand)
            {
                break;
            }
        }
        return value;
    }
}