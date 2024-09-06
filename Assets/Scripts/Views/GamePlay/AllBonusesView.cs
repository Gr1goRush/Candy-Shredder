using UnityEngine;
using TMPro;

public class AllBonusesView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private TypeBonus _typeBonus;

    public TypeBonus BonusType => _typeBonus;
    public int CountBonus => int.Parse(_count.text);

    public void UpdateCount(int count) => _count.text = (int.Parse(_count.text) + count).ToString();

    public void ViewAllCount(int allCount) => _count.text = allCount.ToString();

    private void OnValidate()
    {
        if (_count == null)
            _count = gameObject.GetComponent<TextMeshProUGUI>();
    }
}
