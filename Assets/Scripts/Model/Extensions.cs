using UnityEngine;

public static class Extensions
{
}

public static class FloatExtension
{
    private static float _eps = 5f;

    public static bool ApproximatlyEuqal(this float number1, float number2)
    {
        return (Mathf.Abs(number1) + _eps) > Mathf.Abs(number2);
    }
}
