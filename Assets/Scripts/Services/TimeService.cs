using System;
using System.Collections;
using UnityEngine;

public abstract class TimeService : MonobehaviourSystem, IService
{
    private const int UpdateDelay = 3600;
    private float _elapseTime = 0;

    public event Action RaiseUpdate;

    public DateTime CurrentDateTime { get; protected set; }

    public override void SystemStart()
    {
        base.SystemStart();

        StartCoroutine(TaskGetTime(() =>
        {
            RaiseUpdate?.Invoke();
        },
        () =>
        {
            CurrentDateTime = DateTime.Now;
        }));
    }

    private void Update()
    {
        _elapseTime += Time.deltaTime;

        if(_elapseTime >= UpdateDelay)
        {
            _elapseTime = 0;

            StartCoroutine(TaskGetTime(() =>
            {
                RaiseUpdate?.Invoke();
            },
            () =>
            {
                CurrentDateTime = DateTime.Now;
            }));
        }
    }

    protected abstract IEnumerator TaskGetTime(Action successCallback, Action errorCallback);

    public int GetCurrentHour() => CurrentDateTime.Hour;

    public int GetCurrentMinute() => CurrentDateTime.Minute;

    public int GetCurrentSecond() => CurrentDateTime.Second;
}
