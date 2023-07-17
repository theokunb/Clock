using System;

public class ClockViewModel : BaseViewModel
{
    public event Action RaiseAlarm;
    public event Action<int> SecondsChanged;
    public event Action<int> MinutesChanged;
    public event Action<int> HoursChanged;
    public event Action<int> ChangeSeconds;
    public event Action<int> ChangeMinutes;
    public event Action<int> ChangeHours;
    public event Action Show;
    public event Action Hide;

    private TimeSpan _alarmTime;

    public ClockViewModel(string key) : base(key)
    {

    }

    private int _seconds;
    private int _minutes;
    private int _hours;

    public int DisplaySeconds
    {
        get => _seconds;
        set
        {
            if (_seconds == value)
            {
                return;
            }

            _seconds = value;
            SecondsChanged?.Invoke(DisplaySeconds);
            CheckAlarmTime();
        }
    }
    public int DisplayMinutes
    {
        get => _minutes;
        set
        {
            if (_minutes == value)
            {
                return;
            }

            _minutes = value;
            MinutesChanged?.Invoke(DisplayMinutes);
            CheckAlarmTime();
        }
    }
    public int DisplayHours
    {
        get => _hours;
        set
        {
            if (_hours == value)
            {
                return;
            }

            _hours = value;
            HoursChanged?.Invoke(DisplayHours);
            CheckAlarmTime();
        }
    }

    public void SetSeconds(int seconds)
    {
        ChangeSeconds?.Invoke(seconds);
    }
    public void SetMinutes(int minutes)
    {
        ChangeMinutes?.Invoke(minutes);
    }
    public void SetHours(int hours)
    {
        ChangeHours?.Invoke(hours);
    }

    public void Open()
    {
        Show?.Invoke();
    }

    public void Close()
    {
        Hide?.Invoke();
    }

    public void ChargeAlarm(TimeSpan time)
    {
        _alarmTime = time;
    }

    private void CheckAlarmTime()
    {
        if (_alarmTime.Hours == DisplayHours && _alarmTime.Minutes == DisplayMinutes)
        {
            RaiseAlarm?.Invoke();
        }
    }
}

public class AlarmViewmodel : ClockViewModel
{
    public AlarmViewmodel(string key) : base(key)
    {
    }

    public TimeSpan GetTime()
    {
        return new TimeSpan(DisplayHours, DisplayMinutes, DisplaySeconds);
    }
}