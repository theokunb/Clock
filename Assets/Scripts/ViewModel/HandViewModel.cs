using UnityEngine;

public class HandViewModel : BaseViewModel
{
    private Hand _hand;

    public HandViewModel(string key) : base(key)
    {
    }

    public Hand Hand => _hand;

    public void Init(Hand hand)
    {
        _hand = hand;
    }

    public void Update()
    {
        if (_hand == null)
        {
            return;
        }

        _hand.Update();
    }

    public void Rotate(Transform transform)
    {
        transform.rotation = Quaternion.Euler(0, 0, _hand.CurrentAngle);
    }
}
