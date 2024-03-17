using System.Reflection;
using Dbarone.Net.Csv;

namespace Dbarone.Net.Fake;

/// <summary>
/// Returns data based on a weighted list of values.
/// </summary>
public class WeightedListStrategy : IFakerStrategy<string>
{
    public WeightedListStrategy(WeightedListEnum list) : this(list, new Lcg()) { }

    public WeightedListStrategy(IList<WeightedListItem> data, IRandom<double> random)
    {
        this.Random = random;
        this.data = data;
        this.TotalWeight = this.data.Sum(d => d.Weight);
    }

    public WeightedListStrategy(WeightedListEnum list, IRandom<double> random)
    {
        this.Random = random;
        this.data = GetItemsFromList(list);
        this.TotalWeight = this.data.Sum(d => d.Weight);
    }

    // Private members
    private IList<WeightedListItem>? data = null;
    public int TotalWeight { get; set; }
    public WeightedListEnum List { get; set; }
    public IRandom<double> Random { get; set; } = new Lcg();

    private IList<WeightedListItem> GetItemsFromList(WeightedListEnum list) {
        this.List = list;
        var listStr = list.ToString();
        var assembly = this.GetType().GetTypeInfo().Assembly;
        var path = $"{assembly.GetName().Name}.Datasets.{listStr}.csv";
        Stream resource = assembly.GetManifestResourceStream(path)!;

        CsvConfiguration configuration = new CsvConfiguration
        {
            ProcessRowHandler = (int record, string[] headers, ref object[]? values) =>
            {
                values[1] = int.Parse((string)values[1]);   // weight
                return true;
            }
        };

        CsvReader csv = new CsvReader(resource, configuration);
        var data = csv.Read().Select(d => new WeightedListItem { Value = (string)d["Value"], Weight = (int)d["Weight"] });
        return data.ToList();
    }

    public string Next(int i, string? last = null)
    {
        // Get random number 0 <= x < TotalWeight
        var rand = this.Random.Next() * this.TotalWeight;
        var total = 0;
        string? value = null;
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