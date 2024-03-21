namespace Dbarone.Net.Fake;

public class StateComparer : IEqualityComparer<string[]>
{
    public bool Equals(string[] x, string[] y)
    {
        if (x.Length != y.Length)
        {
            return false;
        }
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i])
            {
                return false;
            }
        }
        return true;
    }

    public int GetHashCode(string[] obj)
    {
        int result = 17;
        for (int i = 0; i < obj.Length; i++)
        {
            unchecked
            {
                result = result * 23 + obj[i].GetHashCode();
            }
        }
        return result;
    }
}