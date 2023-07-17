using System;

public static class HandFactory
{
    public static Hand CreateHand(HandType handType, int defaultValue = 0)
    {
        switch (handType)
        {
            case HandType.SecondHand:
                return new SecondHand(defaultValue);
            case HandType.MinuteHand:
                return new MinuteHand(defaultValue);
            case HandType.HourHand:
                return new HourHand(defaultValue);
            default:
                throw new NotImplementedException();
        }
    }
}