using UnityEngine; 
using UnityEngine.UI;

public class SoundView : ItemShopView
{
    [Header("UI")]
    [SerializeField] private Sprite _noPurchased;
    [SerializeField] private Sprite _purchased;
    [SerializeField] private Sound _sound;
    [SerializeField] private Button _entrySound;
    [SerializeField] private Button _playCheck;
    [SerializeField] private Image _imageBackground;

    public string Name => _sound.Name;

    public override void OnBuy()
    {
        if (Price > ContainerSaveerPlayerPrefs.Instance.SaveerData.Money)
            return;

        SetSpriteButton(_purchased);
        SetEnableButtonBuy(false);
        _entrySound.enabled = true;

        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedSounds != "Standart")
            ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedSounds += "," + _sound.Name;
        else
            ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedSounds = _sound.Name;

        ContainerSaveerPlayerPrefs.Instance.SaveerData.Money -= Price;

        OnPurchasedEventHandler?.Invoke();
    }

    protected override void Start()
    {
        _playCheck.onClick.AddListener(() => { AudioManager.Instance.PlayPartSound(_sound.Name); });

        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedSounds.Contains(_sound.Name))
        {
            _entrySound.enabled = true;
            SetSpriteButton(_purchased);
            SetEnableButtonBuy(false);
        }
        else
            _entrySound.enabled = false;

        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSound == _sound.Name)
            SetFocusItem(true);

        OnPurchasedEventHandler.AddListener(() =>
        {
            if(ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSound == _sound.Name)
                SetFocusItem(false);
        });
        _entrySound.onClick.AddListener(() =>
        {
            OnEntryItemEventHandler?.Invoke();
            SetFocusItem(true);
            ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSound = _sound.Name;
        });
        base.Start();
    }

    public void SetFocusItem(bool value) =>
    _imageBackground.color = value ? new Color(0.3568628f, 0.3568628f, 0.3568628f, 1) : new Color(1, 1, 1, 1);

    private void OnValidate()
    {
        if (_entrySound == null)
            _entrySound = GetComponent<Button>();

        if (_imageBackground == null)
            _imageBackground = GetComponent<Image>();
    }
}
