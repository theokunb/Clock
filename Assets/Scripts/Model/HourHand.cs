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
            int number = (int)Mathf.Floor((-CurrentAngle / AllCircle) * HoursCount);
            return (number % HoursCount + HoursCount) % HoursCount;
        }
    }

    public override float GetDelta()
    {
        return AllCircle / HoursCount;
    }
}