using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlatformView : MonoBehaviour
{
    private Transform _transform;
    private Vector2 _startPosition;

    [SerializeField] float _intensity;
    [SerializeField] private InputMousePlatform _input;
    [SerializeField] private CircleCollider2D _circleCollider2D;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Image _currentImage;
    [SerializeField] private Sprite _onePlatform;
    [SerializeField] private Sprite _doublePlatform;
    [SerializeField] private RectTransform _rectTransform;

    public Vector2 StartPosition => _startPosition;
    public InputMousePlatform Input => _input;
    [HideInInspector]
    public UnityEvent OnGameOverEvetHandler = new UnityEvent();

    public void UpdatePosition(Vector2 newPosition) => _transform.position = new Vector3(newPosition.x, _transform.position.y, _transform.position.z);

    public void SetSprite(int countPlatform)
    {
        if (countPlatform == 1)
        {
            _rectTransform.sizeDelta = new Vector2(52, _rectTransform.sizeDelta.y);
            _circleCollider2D.enabled = true;
            _boxCollider2D.enabled = false;
            _currentImage.sprite = _onePlatform;
        }
        else
        {
            _rectTransform.sizeDelta = new Vector2(128, _rectTransform.sizeDelta.y);
            _circleCollider2D.enabled = false;
            _boxCollider2D.enabled = true;
            _currentImage.sprite = _doublePlatform;
        }
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _startPosition = _transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckingGameOver(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckingGameOver(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckingGameOver(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckingGameOver(collision.gameObject);
    }

    private void CheckingGameOver(GameObject collision)
    {
        var candyView = collision.gameObject.GetComponent<CandyView>();
        if (candyView != null)
        {
            OnGameOverEvetHandler?.Invoke();
            OnGameOverEvetHandler.RemoveAllListeners();
        }
    }

    private void OnValidate()
    {
        if (_circleCollider2D == null)
            _circleCollider2D = transform.GetComponent<CircleCollider2D>();

        if (_boxCollider2D == null)
            _boxCollider2D = transform.GetComponent<BoxCollider2D>();

        if (_currentImage == null)
            _currentImage = transform.GetComponent<Image>();

        if (_rectTransform == null)
            _rectTransform = transform.GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        OnGameOverEvetHandler.RemoveAllListeners();
    }
}
