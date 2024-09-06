using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleBackground;
    [SerializeField] private string _textTitle;
    [SerializeField] private Button _back;

    public void Show()
    {
        _back.onClick.AddListener(Hide);
        _titleBackground.text = _textTitle;
        transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void SubscribeToBackButton()
    {
        _back.onClick.RemoveAllListeners();
        _back.onClick.AddListener(Hide);
    }

    private void Hide()
    {
        _back.onClick.RemoveAllListeners();
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
