using UnityEngine;

public class HourHand : Hand
{
    private const int HoursCount = 12;

    public HourHand(int digitValue) : base(digitValue)
    {
    }

    public override int DigitValue
    {
        get
        {
            return (int)Mathf.Floor((-CurrentAngle / AllCircle) * HoursCount);
        }
    }

    public override float GetDelta()
    {
        return AllCircle / HoursCount;
    }
}