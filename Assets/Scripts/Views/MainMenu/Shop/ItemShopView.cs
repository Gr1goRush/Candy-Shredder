using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ItemShopView : MonoBehaviour
{
    [SerializeField] private Button _buy;
    [SerializeField] private TextMeshProUGUI _priceView;
    [SerializeField] private int _price;

    public int Price => _price;
    [HideInInspector]
    public UnityEvent OnPurchasedEventHandler = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnEntryItemEventHandler = new UnityEvent();

    public virtual void OnBuy()
    {

    }

    public void SetEnableButtonBuy(bool value) => _buy.enabled = value;

    public void SetSpriteButton(Sprite sprite)
    {
        var spriteButton = _buy.GetComponent<Image>();
        if(spriteButton != null)
            spriteButton.sprite = sprite;
    }

    protected virtual void Start()
    {
        _priceView.text = _price.ToString();
        _buy.onClick.AddListener(OnBuy);
    }

    private void OnValidate()
    {
        if (_price < 0)
            _price = 0;
    }

    private void OnDestroy()
    {
        OnPurchasedEventHandler.RemoveAllListeners();
        OnEntryItemEventHandler.RemoveAllListeners();
    }
}
