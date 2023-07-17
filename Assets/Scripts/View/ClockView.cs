using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ClockView : BaseView<ClockViewModel>
{
    [SerializeField] private HandView _secondView;
    [SerializeField] private HandView _minuteView;
    [SerializeField] private HandView _hourView;

    private CanvasGroup _canvasGroup;

    public event Action<int> SecondsChanged;
    public event Action<int> MinutesChanged;
    public event Action<int> HoursChanged;

    public CanvasGroup CanvasGroup => _canvasGroup;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _canvasGroup = GetComponent<CanvasGroup>();

        ViewModel.SecondsChanged += OnSecondChanged;
        ViewModel.MinutesChanged += OnMinutesChanged;
        ViewModel.HoursChanged += OnHourChanged;

        ViewModel.ChangeSeconds += OnChangeSeconds;
        ViewModel.ChangeMinutes += OnChangeMinutes;
        ViewModel.ChangeHours += OnChangeHours;

        ViewModel.Show += Open;
        ViewModel.Hide += Close;
    }

    private void OnChangeHours(int obj)
    {
        _hourView.Hand.SetAngle(obj);
    }

    private void OnChangeMinutes(int obj)
    {
        _minuteView.Hand.SetAngle(obj);
    }

    private void OnChangeSeconds(int obj)
    {
        _secondView.Hand.SetAngle(obj);
    }

    private void OnHourChanged(int obj)
    {
        HoursChanged?.Invoke(obj);
    }

    private void OnMinutesChanged(int obj)
    {
        MinutesChanged?.Invoke(obj);
    }

    private void OnSecondChanged(int obj)
    {
        SecondsChanged?.Invoke(obj);
    }

    private void Update()
    {
        ViewModel.DisplaySeconds = _secondView.Hand.DigitValue;
        ViewModel.DisplayMinutes = _minuteView.Hand.DigitValue;
        ViewModel.DisplayHours = _hourView.Hand.DigitValue;
    }

    public override void Open()
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1;
    }

    public override void Close()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0;
    }
}

public static class AppStrings
{
    public static string KeyClock = nameof(KeyClock);
    public static string KeySecondHand = nameof(KeySecondHand);
    public static string KeyMinuteHand = nameof(KeyMinuteHand);
    public static string KeyHourHand = nameof(KeyHourHand);
    public static string KeyDigitView = nameof(KeyDigitView);

    public static string KeyAlarm = nameof(KeyAlarm);
    public static string KeyAlarmSecondHand = nameof(KeyAlarmSecondHand);
    public static string KeyAlarmMintumeHand = nameof(KeyAlarmMintumeHand);
    public static string KeyAlarmHourHand = nameof(KeyAlarmHourHand);
    public static string KeyAlarmDigitView = nameof(KeyAlarmDigitView);
}