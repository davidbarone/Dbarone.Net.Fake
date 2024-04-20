using Dbarone.Net.Csv;
using System.Reflection;

/// <summary>
/// Gets datasets from internal assembly resources.
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
        var resources = GetResources();
        var path = resources.First(r => r.Contains(datasetStr));
        Stream resource = assembly.GetManifestResourceStream(path)!;

        CsvConfiguration configuration = new CsvConfiguration
        {
            ProcessRowHandler = onProcessRow ?? CsvConfiguration.RowProcessDefault
        };

        CsvReader csv = new CsvReader(resource, configuration);
        var data = csv.Read();
        return data;
    }

    /// <summary>
    /// Gets the string value of a dataset resource.
    /// </summary>
    /// <param name="dataset">The data to get.</param>
    /// <returns>Returns the string contents of the resource.</returns>
    public static string GetString(DatasetEnum dataset)
    {
        var datasetStr = dataset.ToString();
        var assembly = typeof(Dataset).GetTypeInfo().Assembly;
        var path = GetResources().First(r => r.Contains(datasetStr));
        Stream resource = assembly.GetManifestResourceStream(path)!;
        StreamReader sr = new StreamReader(resource);
        return sr.ReadToEnd();
    }

    /// <summary>
    /// Gets a list of all the resources available.
    /// </summary>
    /// <returns>A string array of all the dataset names.</returns>
    public static string[] GetResources()
    {
        var assembly = typeof(Dataset).GetTypeInfo().Assembly;
        return assembly.GetManifestResourceNames();
    }
}