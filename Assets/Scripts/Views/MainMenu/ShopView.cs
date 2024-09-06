using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopView : ItemView
{
    [SerializeField] private TextMeshProUGUI _countMoney;
    [SerializeField] private List<ItemShopView> _items;

    private void SetActiveParentMoney(bool value) =>
        _countMoney.transform.parent.gameObject.SetActive(value);

    private void Start()
    {
        foreach (var item in _items)
            item.OnPurchasedEventHandler.AddListener(UpdateViewMoney);

        UpdateViewMoney();
        SubscribeOnEntryItem();
    }

    private void SubscribeOnEntryItem()
    {
        var backgrounds = _items.FindAll(item => item is BackgroundShopView);
        foreach (BackgroundShopView background in backgrounds)
            background.OnEntryItemEventHandler.AddListener(() =>
            {
                foreach (BackgroundShopView backgroundSubscriber in backgrounds)
                {
                    if (ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentBackground == backgroundSubscriber.Name)
                        backgroundSubscriber.SetFocusItem(false);
                }
            });

        var sounds = _items.FindAll(item => item is SoundView);
        foreach (SoundView sound in sounds)
            sound.OnEntryItemEventHandler.AddListener(() =>
            {
                foreach (SoundView soundSubsccriber in sounds)
                {
                    if (ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSound == soundSubsccriber.Name)
                        soundSubsccriber.SetFocusItem(false);
                }
            });
    }

    private void UpdateViewMoney() =>
        _countMoney.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Money.ToString();

    private void OnDisable() =>
        SetActiveParentMoney(false);

    private void OnEnable() =>
        SetActiveParentMoney(true);
}
