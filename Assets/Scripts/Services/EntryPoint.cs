using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private List<MonobehaviourSystem> _systems;

    private void Awake()
    {
        foreach (var system in _systems.OrderBy(element => element.Priority))
        {
            system.SystemAwake();
        }
    }

    private void OnEnable()
    {
        foreach (var system in _systems.OrderBy(element => element.Priority))
        {
            system.SystemEnable();
        }
    }

    private void Start()
    {
        foreach (var system in _systems.OrderBy(element => element.Priority))
        {
            system.SystemStart();
        }
    }

    private void OnDisable()
    {
        foreach (var system in _systems.OrderByDescending(element => element.Priority))
        {
            system.SystemDisable();
        }
    }
}

public abstract class MonobehaviourSystem : MonoBehaviour
{
    [SerializeField] private int _priority;

    public int Priority => _priority;
    public virtual void SystemAwake() { }
    public virtual void SystemEnable() { }
    public virtual void SystemDisable() { }
    public virtual void SystemStart() { }
}