using UnityEngine;
using UnityEngine.Events;

public class BonusView : MonoBehaviour
{
    private Transform _positionPlatform;
    private Transform _transform;

    [SerializeField] private float _sensitivity = 0.01f;
    [SerializeField] private float _minDistanceStop = 0.1f;
    [SerializeField] private TypeBonus _typeBonus;
    [SerializeField] private Sound _soundAction;

    public TypeBonus Type => _typeBonus;
    public Sound SoundAction => _soundAction;

    [HideInInspector]
    public UnityEvent ReceivingBonusEventHandler = new UnityEvent();

    public virtual void View()
    {
        gameObject.SetActive(true);
    }

    public virtual void Move(Transform platform)
    {
        _positionPlatform = platform;
    }

    public void ReceivingBonus() => gameObject.SetActive(false);

    private void OnInteractionWithPlatform(GameObject gameObjectInteraction)
    {
        var platformView = gameObjectInteraction.GetComponent<PlatformView>();
        if (platformView != null)
            ReceivingBonusEventHandler?.Invoke();
    }

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        if(Vector2.Distance(_transform.position, _positionPlatform.position) >= _minDistanceStop)
            _transform.position = Vector3.Lerp(_transform.position, new Vector3(_positionPlatform.position.x, _positionPlatform.position.y, _transform.position.z), _sensitivity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnInteractionWithPlatform(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnInteractionWithPlatform(collision.gameObject);
    }

    private void OnDestroy()
    {
        ReceivingBonusEventHandler.RemoveAllListeners();
    }
}
