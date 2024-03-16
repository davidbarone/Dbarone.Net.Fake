using System.Reflection;
using Dbarone.Net.Csv;

namespace Dbarone.Net.Fake;

/// <summary>
/// Returns data based on a weighted list of values.
/// </summary>
public class WeightedListStrategy : IFakerStrategy
{
    // Private members
    private IList<IDictionary<string, object>>? data = null;
    private int totalWeight { get; set; }
    private WeightedListEnum list;

    public IRandom Random { get; set; } = new Lcg();
    public WeightedListEnum List
    {
        get { return list; }
        set
        {
            this.list = value;
            var listStr = value.ToString();
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
            this.data = csv.Read().ToList();
            this.totalWeight = this.data.Sum(d => (int)d["Weight"]);
        }
    }

    public object Next(int i, object? last = null)
    {
        // Get random number 0 <= x < TotalWeight
        var rand = this.Random.Next() * this.totalWeight;
        var total = 0;
        string? value = null;
        for (int j = 0; j < this.data!.Count(); j++)
        {
            value = (string)this.data![j]["Value"];
            total += (int)this.data![j]["Weight"];
            if (total > rand)
            {
                break;
            }
        }
        return value;
    }
}