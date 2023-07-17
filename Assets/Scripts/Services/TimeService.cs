using System;
using UnityEngine;

public class TimeService : MonobehaviourSystem, IService
{
    public static TimeService _instance;

    private const int UpdateDelay = 3600;
    private float _elapseTime = 0;

    public event Action RaiseUpdate;

    public static TimeService Instance
    {
        get => _instance;
    }

    public override void SystemAwake()
    {
        base.SystemAwake();
        _instance = this;
    }

    public override void SystemStart()
    {
        base.SystemStart();

        RaiseUpdate?.Invoke();
    }

    private void Update()
    {
        _elapseTime += Time.deltaTime;

        if(_elapseTime >= UpdateDelay)
        {
            _elapseTime = 0;
            RaiseUpdate?.Invoke();
        }
    }

    public int GetCurrentHour()
    {
        return DateTime.Now.Hour;
    }

    public int GetCurrentMinute()
    {
        return DateTime.Now.Minute;
    }

    public int GetCurrentSecond()
    {
        return DateTime.Now.Second;
    }
}
