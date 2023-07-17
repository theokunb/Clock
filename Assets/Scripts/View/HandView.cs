using UnityEngine;
using UnityEngine.EventSystems;

public abstract class HandView : BaseView<HandViewModel>, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private HandType _handType;
    [SerializeField] private int _defaultValue;

    private bool _isEditMode = false;

    public Hand Hand => ViewModel?.Hand;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var hand = HandFactory.CreateHand(_handType);
        ViewModel.Init(hand);
    }

    private void Update()
    {
        if (_isEditMode == false)
        {
            ViewModel?.Update();
        }
        
        ViewModel?.Rotate(transform);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isEditMode = true;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _isEditMode = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mousePosition = Input.mousePosition;
        var direction = mousePosition - transform.position;

        var angle = (-1) * Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        if (angle > 0)
        {
            Hand.CurrentAngle = angle - Hand.AllCircle;
        }
        else
        {
            Hand.CurrentAngle = angle;
        }
    }
}