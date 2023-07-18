using UnityEngine;

public class SecondHand : Hand
{
    private const int SecondsInMinute = 60;

    public SecondHand(int digitValue) : base(digitValue)
    {
    }

    public override int DigitValue
    {
        get
        {
            int number = (int)Mathf.Floor((-CurrentAngle / AllCircle) * SecondsInMinute);
            return (number % SecondsInMinute + SecondsInMinute) % SecondsInMinute;
        }
    }

    public override float GetDelta()
    {
        return AllCircle / SecondsInMinute;
    }
}