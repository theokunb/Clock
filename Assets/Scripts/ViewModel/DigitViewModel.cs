using System;

public class DigitViewModel : BaseViewModel
{
    public Action<string> UpdateFunc;
    public int Seconds { get; set; }
    public int Minutes { get; set; }
    public int Hours { get; set; }

    public DigitViewModel(string key) : base(key)
    {
        
    }

    public void Update()
    {
        UpdateFunc?.Invoke($"{Hours.ToString("00")}:{Minutes.ToString("00")}:{Seconds.ToString("00")}");
    }
}
