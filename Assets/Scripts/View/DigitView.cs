using TMPro;
using UnityEngine;

public class DigitView : BaseView<DigitViewModel>
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ClockView _clockView;

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        ViewModel.UpdateFunc = UpdateFunc;
        _clockView.SecondsChanged += OnSecondsChanged;
        _clockView.MinutesChanged += OnMinutesChanged;
        _clockView.HoursChanged += OnHoursChanged;
    }

    private void OnHoursChanged(int value)
    {
        ViewModel.Hours = value;
        ViewModel.Update();
    }

    private void OnMinutesChanged(int value)
    {
        ViewModel.Minutes = value;
        ViewModel.Update();
    }

    private void OnSecondsChanged(int value)
    {
        ViewModel.Seconds = value;
        ViewModel.Update();
    }

    private void UpdateFunc(string text)
    {
        _text.text = text;
    }
}
