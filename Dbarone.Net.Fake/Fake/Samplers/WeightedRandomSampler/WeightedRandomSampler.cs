using System.Reflection;
using Dbarone.Net.Csv;

namespace Dbarone.Net.Fake;

/// <summary>
/// Returns data based on a weighted list of values.
/// </summary>
public class WeightedRandomSampler<T> : AbstractSampler<T>, ISampler<T>
{
    #region Fields
    private IList<WeightedItem<T>>? data = null;

    public double TotalWeight { get; set; }

    #endregion

    #region Ctor

    public WeightedRandomSampler(
        IEnumerable<IDictionary<string, object>> data,
        IRandom<double> random,
        Func<IDictionary<string, object>, WeightedItem<T>> mapper) : this(data.Select(d => mapper(d)), random) { }

    public WeightedRandomSampler(IEnumerable<WeightedItem<T>> data, IRandom<double> random) : base(random)
    {
        this.data = data.ToList();
        this.TotalWeight = this.data.Sum(d => d.Weight);
    }

    public WeightedRandomSampler(IEnumerable<WeightedItem<T>> data) : base()
    {
        this.data = data.ToList();
        this.TotalWeight = this.data.Sum(d => d.Weight);
    }

    public WeightedRandomSampler(params WeightedItem<T>[] data) : this(data.ToList()) { }

    public WeightedRandomSampler(IRandom<double> random, params WeightedItem<T>[] data) : this(data.ToList(), random) { }

    #endregion

    #region Methods   
    public override T Next()
    {
        // Get random number 0 <= x < TotalWeight
        var rand = this.Random.Next() * this.TotalWeight;
        var total = 0;
        T value = default!;
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

    #endregion
}