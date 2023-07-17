using UnityEngine;

public class ServicesContainer : MonobehaviourSystem
{
    [SerializeField] private ClockView _clockView;
    [SerializeField] private HandView _secondHand;
    [SerializeField] private HandView _minuteHand;
    [SerializeField] private HandView _hourHand;
    [SerializeField] private DigitView _digitView;
    [SerializeField] private TimeService _timeService;

    protected ClockView ClockView => _clockView;
    protected HandView SecondHand => _secondHand;
    protected HandView MinuteHand => _minuteHand;
    protected HandView HourHand => _hourHand;
    protected DigitView DigitView => _digitView;

    protected ClockViewModel ClockViewModel;
    protected HandViewModel SecondHandViewModel;
    protected HandViewModel MinuteHandViewModel;
    protected HandViewModel HourHandViewModel;
    protected DigitViewModel DigitViewModel;

    public override void SystemAwake()
    {
        base.SystemAwake();

        ClockViewModel = new ClockViewModel(AppStrings.KeyClock);
        SecondHandViewModel = new HandViewModel(AppStrings.KeySecondHand);
        MinuteHandViewModel = new HandViewModel(AppStrings.KeyMinuteHand);
        HourHandViewModel = new HandViewModel(AppStrings.KeyHourHand);
        DigitViewModel = new DigitViewModel(AppStrings.KeyDigitView);

        ServiceLocator.Instance.Bind(_clockView, ClockViewModel);
        ServiceLocator.Instance.Bind(_secondHand, SecondHandViewModel);
        ServiceLocator.Instance.Bind(_minuteHand, MinuteHandViewModel);
        ServiceLocator.Instance.Bind(_hourHand, HourHandViewModel);
        ServiceLocator.Instance.Bind(_digitView, DigitViewModel);
        ServiceLocator.Instance.Register(_timeService);
    }
}
