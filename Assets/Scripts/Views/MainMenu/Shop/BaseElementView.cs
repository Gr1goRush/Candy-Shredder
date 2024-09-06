using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseElementView : ItemShopView
{
    [SerializeField] List<Image> _progressBuy;
    [SerializeField] Color _colorProgress;

    public bool TryViewProgressBuy(int quantity)
    {
        if (quantity <= _progressBuy.Count)
        {
            for (int i = 0; i < quantity; i++)
                _progressBuy[i].color = _colorProgress;
        }
        else
        {
            SetEnableButtonBuy(false);
            return false;
        }
        if(quantity == _progressBuy.Count)
            SetEnableButtonBuy(false);

        return true;
    }

    protected override void Start()
    {
        base.Start();
    }
}
