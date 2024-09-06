using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CandyLineView : MonoBehaviour
{
    [SerializeField] private List<CandyView> _candies;
    [SerializeField] private int _numberLine;

    public int NumberLine => _numberLine;
    public List<CandyView> Candies => _candies;

    public void SetActiveCandyLine(bool value) => transform.gameObject.SetActive(value);

    public void AddListenerBrokenCandy(UnityAction<Transform> action)
    {
        foreach (var candy in _candies)
            candy.BrokeCandyEventHandler.AddListener(action);
    }

    public bool TryBrokenAllCandiesInLine()
    {
        foreach (var candy in _candies)
        {
            if (candy.IsActive())
                return false;
        }

        return true;
    }

    private void OnValidate()
    {
        if (_candies == null || _candies.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var candyView = transform.GetChild(i)?.GetComponent<CandyView>();
                if (candyView != null)
                    _candies.Add(candyView);
            }
        }
    }
}
