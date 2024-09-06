using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformElementView : BaseElementView
{
    private int _stepBuy = 1;

    public override void OnBuy()
    {
        if (Price > ContainerSaveerPlayerPrefs.Instance.SaveerData.Money)
            return;

        var countBuyPlatform = ContainerSaveerPlayerPrefs.Instance.SaveerData.CountPlaforms + _stepBuy;
        if (TryViewProgressBuy(countBuyPlatform))
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.CountPlaforms = countBuyPlatform;
            ContainerSaveerPlayerPrefs.Instance.SaveerData.Money -= Price;
            OnPurchasedEventHandler?.Invoke();
        }
    }

    protected override void Start()
    {
        base.Start();
        TryViewProgressBuy(ContainerSaveerPlayerPrefs.Instance.SaveerData.CountPlaforms);
    }
}
