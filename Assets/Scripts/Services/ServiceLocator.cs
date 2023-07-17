using System.Collections.Generic;

public class ServiceLocator : MonobehaviourSystem
{
    private Dictionary<string, IService> _services;

    public static ServiceLocator Instance { get; private set; }

    public override void SystemAwake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            _services = new Dictionary<string, IService>();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public T Get<T>() where T : IService
    {
        string name = typeof(T).Name;

        return Get<T>(name);
    }

    public T Get<T>(string key) where T : IService
    {
        if (_services.ContainsKey(key))
        {
            return (T)_services[key];
        }

        return default;
    }

    public void Register<T>(T service) where T : IService
    {
        string name = typeof(T).Name;

        if (_services.ContainsKey(name))
        {
            return;
        }

        _services.Add(name, service);
    }

    public void Unregister<T>() where T : IService
    {
        string name = typeof(T).Name;

        Unregister<T>(name);
    }

    public void Unregister<T>(string key) where T : IService
    {
        if (_services.ContainsKey(name))
        {
            _services.Remove(name);
        }
    }

    public void Bind<T1, T2>(T1 view, T2 viewmodel) where T1 : BaseView<T2> where T2 : BaseViewModel
    {
        string key = viewmodel.Key;

        if (_services.ContainsKey(key))
        {
            return;
        }

        view.DataBind(viewmodel);
        _services.Add(key, viewmodel);
    }
}

public interface IService
{

}