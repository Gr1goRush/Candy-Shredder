using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sound _soundAction;

    private void Start()
    {
        _button.onClick.AddListener(() => { AudioManager.Instance.PlaySound(_soundAction.Name); });
    }

    private void OnValidate()
    {
        if (_button == null)
            _button = GetComponent<Button>();
    }
}
