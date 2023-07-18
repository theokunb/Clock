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
            int number = (int)Mathf.Floor((-CurrentAngle / AllCircle) * MinutesInHour);
            return (number % MinutesInHour + MinutesInHour) % MinutesInHour;
        }
    }

    public override float GetDelta()
    {
        return AllCircle / MinutesInHour;
    }
}