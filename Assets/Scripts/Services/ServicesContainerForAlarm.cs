public class ServicesContainerForAlarm : ServicesContainer
{
    public override void SystemAwake()
    {
        ClockViewModel = new AlarmViewmodel(AppStrings.KeyAlarm);
        SecondHandViewModel = new HandViewModel(AppStrings.KeyAlarmSecondHand);
        MinuteHandViewModel = new HandViewModel(AppStrings.KeyAlarmMintumeHand);
        HourHandViewModel = new HandViewModel(AppStrings.KeyAlarmHourHand);
        DigitViewModel = new DigitViewModel(AppStrings.KeyAlarmDigitView);

        ServiceLocator.Instance.Bind(ClockView, ClockViewModel);
        ServiceLocator.Instance.Bind(SecondHand, SecondHandViewModel);
        ServiceLocator.Instance.Bind(MinuteHand, MinuteHandViewModel);
        ServiceLocator.Instance.Bind(HourHand, HourHandViewModel);
        ServiceLocator.Instance.Bind(DigitView, DigitViewModel);
    }
}
