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
            return (int)Mathf.Floor((-CurrentAngle / AllCircle) * SecondsInMinute);
        }
    }

    public override float GetDelta()
    {
        return AllCircle / SecondsInMinute;
    }
}