using Dbarone.Net.Csv;
using System.Reflection;

/// <summary>
/// Gets internal datasets.
/// </summary>
public static class Dataset
{
    /// <summary>
    /// Gets a dataset from an embedded dataset resource.
    /// </summary>
    /// <param name="dataset">The dataset to get.</param>
    /// <param name="onProcessRow">Callback function to map each row in the CSV file.</param>
    /// <returns></returns>
    public static IEnumerable<IDictionary<string, object>> GetData(DatasetEnum dataset, ProcessRowDelegate? onProcessRow = null)
    {
        var datasetStr = dataset.ToString();
        var assembly = typeof(Dataset).GetTypeInfo().Assembly;
        var path = $"{assembly.GetName().Name}.Datasets.{datasetStr}.csv";
        Stream resource = assembly.GetManifestResourceStream(path)!;

        CsvConfiguration configuration = new CsvConfiguration
        {
            ProcessRowHandler = onProcessRow
        };

        CsvReader csv = new CsvReader(resource, configuration);
        var data = csv.Read();
        return data;
    }
}