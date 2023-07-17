using UnityEngine;
using UnityEngine.EventSystems;

public class DependencyHandView : HandView
{
    [SerializeField] private HandView _dependencyHandView;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        InitDependency(_dependencyHandView);
    }

    private void InitDependency(HandView handView)
    {
        if (handView == null)
        {
            return;
        }

        ViewModel.Hand.AddDependency(handView.Hand);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        Hand.UndependencyAngle = Hand.CurrentAngle + Mathf.Abs(Hand.CurrentAngle % Hand.GetDelta());
    }

    public override void Open()
    {
        gameObject.SetActive(false);
    }

    public override void Close()
    {
        gameObject.SetActive(true);
    }
}