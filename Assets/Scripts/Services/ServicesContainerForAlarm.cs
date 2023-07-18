using UnityEngine;

public class ServicesContainerForAlarm : MonobehaviourSystem
{
    [SerializeField] private ClockView _clockView;
    [SerializeField] private HandView _secondHand;
    [SerializeField] private HandView _minuteHand;
    [SerializeField] private HandView _hourHand;
    [SerializeField] private DigitView _digitView;

    protected ClockViewModel ClockViewModel;
    protected HandViewModel SecondHandViewModel;
    protected HandViewModel MinuteHandViewModel;
    protected HandViewModel HourHandViewModel;
    protected DigitViewModel DigitViewModel;

    public override void SystemAwake()
    {
        ClockViewModel = new AlarmViewmodel(AppStrings.KeyAlarm);
        SecondHandViewModel = new HandViewModel(AppStrings.KeyAlarmSecondHand);
        MinuteHandViewModel = new HandViewModel(AppStrings.KeyAlarmMintumeHand);
        HourHandViewModel = new HandViewModel(AppStrings.KeyAlarmHourHand);
        DigitViewModel = new DigitViewModel(AppStrings.KeyAlarmDigitView);

        ServiceLocator.Instance.Bind(_clockView, ClockViewModel);
        ServiceLocator.Instance.Bind(_secondHand, SecondHandViewModel);
        ServiceLocator.Instance.Bind(_minuteHand, MinuteHandViewModel);
        ServiceLocator.Instance.Bind(_hourHand, HourHandViewModel);
        ServiceLocator.Instance.Bind(_digitView, DigitViewModel);
    }
}
