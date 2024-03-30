namespace Dbarone.Net.Fake;

public class StringArrayComparer : IComparer<string[]>
{
    public int Compare(string[]? x, string[]? y)
    {
        if (x is null || y is null)
        {
            throw new Exception("Cannot compare null arrays.");
        }
        else if (x.Length != y.Length)
        {
            throw new Exception("Cannot compare. Array lengths are different.");
        }
        else
        {
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i].CompareTo(y[i]) == -1)
                {
                    return -1;
                }
                else if (x[i].CompareTo(y[i]) == 1)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}