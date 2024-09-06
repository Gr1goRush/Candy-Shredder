using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform
{
    private Vector2 _position;

    public UnityEvent<Vector2> OnUpdatePositionEventHandler;

    public Platform(Vector2 xPosition)
    {
        _position = xPosition;
        OnUpdatePositionEventHandler = new UnityEvent<Vector2>();
    }

    public void UpdatePosition(Vector2 newPosition)
    {
        _position = new Vector2(newPosition.x, _position.y);
        OnUpdatePositionEventHandler?.Invoke(_position);
    }
}
