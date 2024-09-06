using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet
{
    private Vector2 _velocity;
    private Vector2 _position;

    public UnityEvent<Vector2> OnUpdateVelocityEventHandler;
    public UnityEvent<Vector2> OnUpdatePositionEventHandler;

    public Bullet(Vector2 startPosition)
    {
        OnUpdateVelocityEventHandler = new UnityEvent<Vector2>();
        OnUpdatePositionEventHandler = new UnityEvent<Vector2>();
        _position = startPosition;
    }

    public void UpdateVelocity(Vector2 value)
    {
        _velocity = value;
        OnUpdateVelocityEventHandler?.Invoke(_velocity);
    }

    public void UpdatePosition(Vector2 newPosition)
    {
        if (IsStopped())
        {
            _position = new Vector2(newPosition.x, _position.y);
            OnUpdatePositionEventHandler?.Invoke(_position);
        }
    }

    public bool IsStopped() => _velocity == Vector2.zero;
}
