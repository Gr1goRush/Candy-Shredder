using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListCandyLineView : MonoBehaviour
{
    [SerializeField] private List<CandyLineView> _listCandyLine;

    public List<CandyLineView> CandyLines => _listCandyLine;

    public void AddListenerBrokenCandy(UnityAction<Transform> action)
    {
        foreach (var listCandyLine in _listCandyLine)
            listCandyLine.AddListenerBrokenCandy(action);
    }

    public int GetIndexFreeCandyLine()
    {
        for (int i = _listCandyLine.Count - 1; i >= 0; i--)
        {
            if (_listCandyLine[i] == null)
                continue;

            if(_listCandyLine[i].TryBrokenAllCandiesInLine() == false)
                return i + 1 == _listCandyLine.Count ? _listCandyLine.IndexOf(_listCandyLine[i]) : _listCandyLine.IndexOf(_listCandyLine[i + 1]);
        }

        return 0;
    }

    private void OnValidate()
    {
        if (_listCandyLine == null || _listCandyLine.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var candyLineView = transform.GetChild(i)?.GetComponent<CandyLineView>();
                if (candyLineView != null)
                    _listCandyLine.Add(candyLineView);
            }
        }
    }
}
