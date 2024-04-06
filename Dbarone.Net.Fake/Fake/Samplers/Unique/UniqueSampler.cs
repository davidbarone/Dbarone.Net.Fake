using System.Reflection;
using Dbarone.Net.Csv;

namespace Dbarone.Net.Fake;

/// <summary>
/// Sampler that guarantees each sampled item to be unique.
/// </summary>
public class UniqueSampler<T> : ISampler<T>
{
    ISampler<T> sampler { get; set; }
    int maxAttempts { get; set; } = 1000;
    HashSet<T> set = new HashSet<T>();
    IEqualityComparer<T>? equalityComparer { get; set; }

    public IRandom<double> Random { get; set; }

    public UniqueSampler(ISampler<T> sampler, IEqualityComparer<T>? equalityComparer = null, int? maxAttempts = null)
    {
        this.sampler = sampler;
        this.equalityComparer = equalityComparer;
        this.maxAttempts = maxAttempts ?? 1000;
        if (equalityComparer != null)
        {
            this.set = new HashSet<T>(equalityComparer);
        }
    }

    public T Next()
    {
        int attempt = 1;
        while (attempt <= maxAttempts)
        {
            T next = sampler.Next();
            var added = set.Add(next);
            if (added) return next;
            attempt++;
        }
        // if got here, then cannot generate a unique value
        throw new Exception($"Unable to generate a unique value after {maxAttempts} attempts.");
    }
}