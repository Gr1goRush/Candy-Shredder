using UnityEngine;
using UnityEngine.UI;

public class SwitchToggleView : MonoBehaviour
{
    [SerializeField] Sprite _togglwOff;
    [SerializeField] Sprite _togglwOn;
    [SerializeField] Button _toggle;
    [SerializeField] Image _toggleImage;
    [SerializeField] string _keySaveerTool;

    public int LoadToggleState()
    {
        var value = LoadDataState();
        UpdateSpriteToggle(value);

        return value;
    }

    public virtual int LoadDataState() => ContainerSaveerPlayerPrefs.Instance.SaveerData.Load<int>(_keySaveerTool, 1);

    public virtual void SaveDataState(int value) => ContainerSaveerPlayerPrefs.Instance.SaveerData.Save<int>(_keySaveerTool, value);

    public void UpdateSpriteToggle(int value) => _toggleImage.sprite = value == 1 ? _togglwOn : _togglwOff;

    private void Start()
    {
        _toggle.onClick.AddListener(Switch);
    }

    private void OnEnable()
    {
        LoadToggleState();
    }

    private void Switch()
    {
        var currentState = LoadToggleState();
        var nextState = currentState == 1 ? 0 : 1;
        SaveDataState(nextState);
        _toggleImage.sprite = nextState == 1 ? _togglwOn : _togglwOff;
    }

    private void OnValidate()
    {
        if (_toggle == null)
            _toggle = transform.GetComponent<Button>();

        if (_toggleImage == null)
            _toggleImage = transform.GetComponent<Image>();
    }
}