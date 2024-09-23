

using System.Numerics;

namespace Hr.Api;

public class Calculator
{

    public T Add<T>(T a, T b) where T : INumber<T>
    {

        return a + b;
    }

    //public string Add(string a, string b)
    //{
    //    return a + b;
    //}
}
