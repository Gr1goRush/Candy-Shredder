using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBonusesView : MonoBehaviour
{
    [SerializeField] private List<AllBonusesView> _allBonusesView;
    [SerializeField] protected Button _againGame;
    [SerializeField] protected Button _backToMenu;

    public void ViewCountBonuses(int countMoney, int countCoins)
    {
        var viewCoins = _allBonusesView.Find(view => view.BonusType == TypeBonus.Coin);
        var viewMoney = _allBonusesView.Find(view => view.BonusType == TypeBonus.Money);
        viewCoins.ViewAllCount(countCoins);
        viewMoney.ViewAllCount(countMoney);
    }

    protected virtual void Start()
    {
        _backToMenu.onClick.AddListener(() => { ManagerScenes.Instance.LoadAsyncFromCoroutine("MainMenu"); });
    }
}
