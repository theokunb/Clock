using System;

public class TimeService : IService
{
    public static TimeService _instance;

    public static TimeService Instance
    {
        get => _instance ?? (_instance = new TimeService());
    }

    public int GetCurrentHour()
    {
        return DateTime.Now.Hour;
    }

    public int GetCurrentMinute()
    {
        return DateTime.Now.Minute;
    }

    public int GetCurrentSecond()
    {
        return DateTime.Now.Second;
    }
}
