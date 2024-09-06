using UnityEngine;
using UnityEngine.UI;

public class BackgroundShopView : ItemShopView
{
    [Header("UI")]
    [SerializeField] private Sprite _noPurchased;
    [SerializeField] private Sprite _purchased;
    [SerializeField] private BackgroundGame _backgroundGame;
    [SerializeField] private Button _entryBackground;
    [SerializeField] private Image _imageBackground;

    public string Name => _backgroundGame.Name;

    public override void OnBuy()
    {
        if (Price > ContainerSaveerPlayerPrefs.Instance.SaveerData.Money)
            return;

        SetSpriteButton(_purchased);
        SetEnableButtonBuy(false);
        _entryBackground.enabled = true;      

        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedBackgrounds != "base")
            ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedBackgrounds += "," + _backgroundGame.Name;
        else
            ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedBackgrounds = _backgroundGame.Name;

        ContainerSaveerPlayerPrefs.Instance.SaveerData.Money -= Price;

        OnPurchasedEventHandler?.Invoke();
    }

    protected override void Start()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedBackgrounds.Contains(_backgroundGame.Name))
        {
            _entryBackground.enabled = true;
            SetSpriteButton(_purchased);
            SetEnableButtonBuy(false);
        }
        else
            _entryBackground.enabled = false;

        if(ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentBackground == _backgroundGame.Name)
            SetFocusItem(true);

        OnPurchasedEventHandler.AddListener(() =>
        {
            if (ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentBackground == _backgroundGame.Name)
                SetFocusItem(false);
        });
        _entryBackground.onClick.AddListener(() =>
        {
            OnEntryItemEventHandler?.Invoke();
            SetFocusItem(true);
            ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentBackground = _backgroundGame.Name;
        });
        base.Start();
    }

    public void SetFocusItem(bool value) =>
        _imageBackground.color = value ? new Color(0.3568628f, 0.3568628f, 0.3568628f, 1) : new Color(1, 1, 1, 1);

    private void OnValidate()
    {
        if (_entryBackground == null)
            _entryBackground = GetComponent<Button>();

        if (_imageBackground == null)
            _imageBackground = GetComponent<Image>();
    }
}
