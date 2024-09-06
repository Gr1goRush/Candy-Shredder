using UnityEngine;
using UnityEngine.Events;
using UnityFigmaBridge.Runtime.UI;

public class CandyView : MonoBehaviour
{
    [SerializeField] private FigmaImage _image;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    public UnityEvent<Transform> BrokeCandyEventHandler = new UnityEvent<Transform>();

    public void SetImage(Sprite spriteImage) => _image.sprite = spriteImage;

    public void SetActive(bool value)
    {
        _image.gameObject.SetActive(value);
        _boxCollider2D.enabled = value;

        if (value == true)
        {
            if (gameObject.activeSelf == false)
                gameObject.SetActive(true);
        }
    }

    public bool IsActive() =>
        _image.gameObject.activeSelf && _boxCollider2D.enabled;

    private void Awake()
    {
        SetActive(false);
    }


    private void Start()
    {
        BrokeCandyEventHandler.AddListener((position) => { SetActive(false); });
    }

    private void OnValidate()
    {
        if (_image == null)
        {
            var transformChild = transform.GetChild(0);
            if (transformChild != null)
                _image = transformChild.GetComponent<FigmaImage>();
        }

        var boxCollider = transform.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
            transform.gameObject.AddComponent<BoxCollider2D>();

        if (boxCollider != null)
            boxCollider.size = new Vector2(24, 24);

        if (_boxCollider2D == null)
            _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnDestroy()
    {
        BrokeCandyEventHandler.RemoveAllListeners();
    }
}
