using UnityEngine;

public abstract class MonobehaviourSystem : MonoBehaviour
{
    [SerializeField] private int _priority;

    public int Priority => _priority;
    public virtual void SystemAwake() { }
    public virtual void SystemEnable() { }
    public virtual void SystemDisable() { }
    public virtual void SystemStart() { }
}