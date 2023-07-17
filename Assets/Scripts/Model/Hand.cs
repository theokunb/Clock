using System;
using UnityEngine;

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
            if (_currentAngle == value)
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
        if (_dependenyHand == null)
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
        if (hand == null)
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
        if (_dependenyHand != null)
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