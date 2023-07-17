using System;
using TMPro;
using UnityEngine;

public class MainView : BaseViewSystem<MainViewModel>
{
    [SerializeField] private TMP_Text _alarmStatus;

    public override void SystemStart()
    {
        DataBind(new MainViewModel(string.Empty));

        ViewModel.AlarmButtonStateChanged += OnStatusChanged;

        ViewModel.CurrentState = AlarmButtonState.Edit;
    }

    private void OnStatusChanged(AlarmButtonState currentState)
    {
        switch (currentState)
        {
            case AlarmButtonState.Edit:
                _alarmStatus.text = "Edit";
                break;
            case AlarmButtonState.Confirm:
                _alarmStatus.text = "Confirm";
                break;
            case AlarmButtonState.Cancel:
                _alarmStatus.text = "Cancel";
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public void AlarmButtonClick()
    {
        ViewModel.PerformAlarmButton();
    }
}

public class MainViewModel : BaseViewModel
{
    private AlarmButtonState _currentState;
    private ClockViewModel _clock;
    private AlarmViewmodel _alarm;
    private TimeService _timeService;

    public event Action<AlarmButtonState> AlarmButtonStateChanged;

    public MainViewModel(string key) : base(key)
    {
    }

    public AlarmButtonState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            AlarmButtonStateChanged?.Invoke(CurrentState);
        }
    }

    public override void OnInitialized()
    {
        base.OnInitialized();

        _clock = ServiceLocator.Instance.Get<ClockViewModel>(AppStrings.KeyClock);
        _alarm = ServiceLocator.Instance.Get<AlarmViewmodel>(AppStrings.KeyAlarm);
        _timeService = ServiceLocator.Instance.Get<TimeService>();

        _clock.SetHours(_timeService.GetCurrentHour());
        _clock.SetMinutes(_timeService.GetCurrentMinute());
        _clock.SetSeconds(_timeService.GetCurrentSecond());

        _clock.Open();
        _alarm.Close();
    }

    public void PerformAlarmButton()
    {
        switch (_currentState)
        {
            case AlarmButtonState.Edit:
                EditAlarm();
                break;
            case AlarmButtonState.Confirm:
                ConfirmAlarm();
                break;
            case AlarmButtonState.Cancel:
                CancelAlarm();
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void EditAlarm()
    {
        _clock.Close();
        _alarm.Open();

        CurrentState = AlarmButtonState.Confirm;
    }

    private void ConfirmAlarm()
    {
        _clock.RaiseAlarm += OnAlarmRaised;

        _clock.ChargeAlarm(_alarm.GetTime());
        _clock.Open();
        _alarm.Close();

        CurrentState = AlarmButtonState.Edit;
    }


    private void OnAlarmRaised()
    {
        CurrentState = AlarmButtonState.Cancel;
        Debug.Log("bzzzzzzz");
    }

    private void CancelAlarm()
    {
        _clock.RaiseAlarm -= OnAlarmRaised;
        CurrentState = AlarmButtonState.Edit;
    }
}

public enum AlarmButtonState
{
    Edit,
    Confirm,
    Cancel
}