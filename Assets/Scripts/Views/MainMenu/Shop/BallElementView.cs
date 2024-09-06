using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallElementView : BaseElementView
{
    private int _stepBuy = 1;

    public override void OnBuy()
    {
        if (Price > ContainerSaveerPlayerPrefs.Instance.SaveerData.Money)
            return;

        var countBuyBalls = ContainerSaveerPlayerPrefs.Instance.SaveerData.CountBalls + _stepBuy;
        if (TryViewProgressBuy(countBuyBalls))
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.CountBalls = countBuyBalls;
            ContainerSaveerPlayerPrefs.Instance.SaveerData.Money -= Price;
            OnPurchasedEventHandler?.Invoke();
        }

    }

    protected override void Start()
    {
        base.Start();
        TryViewProgressBuy(ContainerSaveerPlayerPrefs.Instance.SaveerData.CountBalls);
    }
}
