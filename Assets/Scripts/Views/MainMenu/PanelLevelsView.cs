using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelLevelsView : MonoBehaviour
{
    [SerializeField] Button _upLevel;
    [SerializeField] Button _downLevel;
    [SerializeField] TextMeshProUGUI _viewLevel;

    private void Start()
    {
        UpdateLevel();

        _upLevel.onClick.AddListener(OnUpLevel);
        _downLevel.onClick.AddListener(OnDownLevel);
    }

    private void OnUpLevel()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.Level < ContainerSaveerPlayerPrefs.Instance.SaveerData.CountLevels)
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.Level += 1;
            UpdateLevel();
        }
    }

    private void OnDownLevel()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.Level != 1)
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.Level -= 1;
            UpdateLevel();
        }
    }

    private void UpdateLevel() => _viewLevel.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Level.ToString();
}
