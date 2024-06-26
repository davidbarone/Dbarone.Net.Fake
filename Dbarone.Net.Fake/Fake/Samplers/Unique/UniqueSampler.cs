using System.Reflection;
using Dbarone.Net.Csv;

namespace Dbarone.Net.Fake;

/// <summary>
/// Sampler that guarantees each sampled item to be unique.
/// </summary>
public class UniqueSampler<T> : AbstractSampler<T>, ISampler<T>
{
    #region Fields

    ISampler<T> sampler { get; set; }
    int maxAttempts { get; set; } = 1000;
    HashSet<T> set = new HashSet<T>();
    IEqualityComparer<T>? equalityComparer { get; set; }

    #endregion

    #region Ctor

    public UniqueSampler(ISampler<T> sampler, IEqualityComparer<T>? equalityComparer = null, int? maxAttempts = null):base()
    {
        this.sampler = sampler;
        this.equalityComparer = equalityComparer;
        this.maxAttempts = maxAttempts ?? 1000;
        if (equalityComparer != null)
        {
            this.set = new HashSet<T>(equalityComparer);
        }
    }

    #endregion

    #region Methods

    public override T Next()
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

    #endregion
}