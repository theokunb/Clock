public class BaseViewModel : IService
{
    public BaseViewModel(string key)
    {
        Key = key;
    }

    public string Key { get; private set; }

    public virtual void OnInitialized() { }
    public virtual void OnDestroy() { }
}
