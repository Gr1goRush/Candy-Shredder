using UnityEngine;
using UnityEngine.Events;

public class InputMouseBullet : BaseInputManager<Vector2>
{
    private Transform _transform;

    [HideInInspector]
    public UnityEvent OnUpdateEventHandler = new UnityEvent();

    public void UpdateInput()
    {
        Vector2 targetPosition = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
            targetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)_transform.position;
            
        if(Input.touches.Length != 0)
            targetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - (Vector2)_transform.position;

        InputEventHandler?.Invoke(targetPosition);
    }

    public void OnMouseFromPlatform(bool isMouseOnPlatform)
    {
        if (isMouseOnPlatform)
            OnUpdateEventHandler.RemoveListener(UpdateInput);
        else
            OnUpdateEventHandler.AddListener(UpdateInput);
    }

    private void Start()
    {
        _transform = transform;
        OnUpdateEventHandler.AddListener(UpdateInput);
    }

    private void Update()
    {
        OnUpdateEventHandler?.Invoke();
    }

    private void OnDestroy()
    {
        InputEventHandler.RemoveAllListeners();
        OnUpdateEventHandler.RemoveAllListeners();
    }
}
