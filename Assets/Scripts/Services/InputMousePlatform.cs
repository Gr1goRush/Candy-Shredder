using UnityEngine;
using UnityEngine.Events;

public class InputMousePlatform : BaseInputManager<Vector2>
{
    private Transform _transform;
    private Vector2 _pointClick;

    [HideInInspector]
    public UnityEvent<bool> OnMouseFromPlatformEventHandler = new UnityEvent<bool>();

    private void Start()
    {
        _transform = transform;
    }

    private void OnMouseDown()
    {
        _pointClick = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)_transform.position;
    }

    private void OnMouseDrag()
    {
        var pointClick = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - _pointClick;
        InputEventHandler?.Invoke(pointClick);
    }

    private void OnMouseOver()
    {
        OnMouseFromPlatformEventHandler?.Invoke(true);
    }

    private void OnMouseExit()
    {
        OnMouseFromPlatformEventHandler?.Invoke(false);
    }

    private void OnDestroy()
    {
        InputEventHandler.RemoveAllListeners();
        OnMouseFromPlatformEventHandler.RemoveAllListeners();
    }
}
