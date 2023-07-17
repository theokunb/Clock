using UnityEngine;

public class MinuteHand : Hand
{
    private const int MinutesInHour = 60;

    public MinuteHand(int digitValue) : base(digitValue)
    {
    }

    public override int DigitValue
    {
        get
        {
            return (int)Mathf.Floor((-CurrentAngle / AllCircle) * MinutesInHour);
        }
    }

    public override float GetDelta()
    {
        return AllCircle / MinutesInHour;
    }
}