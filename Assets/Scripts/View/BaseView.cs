using UnityEngine;

public abstract class BaseView<T> : MonoBehaviour, IMenu where T : BaseViewModel
{
    protected T ViewModel { get; private set; }

    public void DataBind(T viewModel)
    {
        ViewModel = viewModel;
        ViewModel.OnInitialized();
        OnInitialized();
    }

    protected virtual void OnInitialized() { }
    public abstract void Open();
    public abstract void Close();
}

public class BaseViewSystem<T> : MonobehaviourSystem where T: BaseViewModel
{
    protected T ViewModel { get; private set; }

    public void DataBind(T viewModel)
    {
        ViewModel = viewModel;
        ViewModel.OnInitialized();
    }
}

public interface IMenu
{
    void Open();
    void Close();
}