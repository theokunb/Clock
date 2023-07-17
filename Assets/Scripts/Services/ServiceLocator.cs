using System;
using System.Collections.Generic;
using UnityEngine;

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

    public void Unregister<T>(string key) where T: IService
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

public abstract class Hand
{
    public const int AllCircle = 360;

    private float _previousAngle;
    private float _currentAngle;
    private Hand _dependenyHand;
    private float _dependencyAngle;
    private float _undependencyAngle;

    public event Action<float> ProportionChanged;
    public event Action Increased;
    public event Action Decreased;

    public abstract int DigitValue { get; }
    public float UndependencyAngle
    {
        get => _undependencyAngle;
        set
        {
            if (value == _undependencyAngle)
            {
                return;
            }

            _undependencyAngle = value;
        }
    }
    public float CurrentAngle
    {
        get => _currentAngle;
        set
        {
            if(_currentAngle == value)
            {
                return;
            }

            _previousAngle = _currentAngle;
            _currentAngle = value % AllCircle;

            ProportionChanged?.Invoke(CurrentAngle / AllCircle);
            LoopProccess();
        }
    }

    public Hand(int digitValue)
    {
        SetAngle(digitValue);
    }

    public void SetAngle(int digitValue)
    {
        CurrentAngle = -digitValue * GetDelta();
        _undependencyAngle = CurrentAngle;
    }

    public void Update()
    {
        if(_dependenyHand == null)
        {
            CurrentAngle -= GetDelta() * Time.deltaTime;
        }
        else
        {
            CurrentAngle = _dependencyAngle + _undependencyAngle;
        }
    }

    public void AddDependency(Hand hand)
    {
        if(hand == null)
        {
            return;
        }

        _dependenyHand = hand;
        _dependenyHand.ProportionChanged += OnProportionChanged;
        _dependenyHand.Increased += OnIncreased;
        _dependenyHand.Decreased += OnDecreased;
    }

    public void RemoveDependency()
    {
        if( _dependenyHand != null )
        {
            _dependenyHand.ProportionChanged -= OnProportionChanged;
            _dependenyHand.Increased -= OnIncreased;
            _dependenyHand.Decreased -= OnDecreased;
        }
    }

    private void LoopProccess()
    {
        const float eps = 5;

        if (CurrentAngle.ApproximatlyEuqal(0) == true)
        {
            if (CurrentAngle >= -eps && _previousAngle <= (-1) * (AllCircle - eps))
            {
                Increased?.Invoke();
            }
            else if (CurrentAngle <= (-1) * (AllCircle - eps) && _previousAngle >= -eps)
            {
                Decreased?.Invoke();
            }
        }
    }

    protected void OnDecreased()
    {
        _undependencyAngle += GetDelta();
    }

    protected void OnIncreased()
    {
        _undependencyAngle -= GetDelta();
    }

    private void OnProportionChanged(float proportion)
    {
        _dependencyAngle = proportion * GetDelta();
    }

    public abstract float GetDelta();
}

public class SecondHand : Hand
{
    private const int SecondsInMinute = 60;

    public SecondHand(int digitValue) : base(digitValue)
    {
    }

    public override int DigitValue
    {
        get
        {
            return (int)Mathf.Floor((-CurrentAngle / AllCircle) * SecondsInMinute);
        }
    }

    public override float GetDelta()
    {
        return AllCircle / SecondsInMinute;
    }
}

public class MinuteHand : Hand
{
    private const int MinutesInHour = 60;

    public MinuteHand(int digitValue) : base(digitValue)
    {
    }

    public override int DigitValue
    {
        get
        {
            return (int)Mathf.Floor((-CurrentAngle / AllCircle) * MinutesInHour);
        }
    }

    public override float GetDelta()
    {
        return AllCircle / MinutesInHour;
    }
}

public class HourHand : Hand
{
    private const int HoursCount = 12;

    public HourHand(int digitValue) : base(digitValue)
    {
    }

    public override int DigitValue
    {
        get
        {
            return (int)Mathf.Floor((-CurrentAngle / AllCircle) * HoursCount);
        }
    }

    public override float GetDelta()
    {
        return AllCircle / HoursCount;
    }
}

public static class HandFactory
{
    public static Hand CreateHand(HandType handType, int defaultValue = 0)
    {
        switch (handType)
        {
            case HandType.SecondHand:
                return new SecondHand(defaultValue);
            case HandType.MinuteHand:
                return new MinuteHand(defaultValue);
            case HandType.HourHand:
                return new HourHand(defaultValue);
            default:
                throw new NotImplementedException();
        }
    }
}

public enum HandType
{
    SecondHand,
    MinuteHand,
    HourHand
}

public static class FloatExtension
{
    private static float _eps = 5f;

    public static bool ApproximatlyEuqal(this float number1, float number2)
    {
        return (Mathf.Abs(number1) + _eps) > Mathf.Abs(number2);
    }
}