using UnityEngine;
using UnityEngine.UI;

public class StarsView : MonoBehaviour
{
    private float _currentAlfa;

    [SerializeField] private Image _image;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _minAlfa;

    private void Start()
    {
        _currentAlfa = _minAlfa;
    }

    private void FixedUpdate()
    {
        FlickerStars();
    }

    private void FlickerStars()
    {
        if (_image.color.a >= 1)
        {
            _sensitivity *= -1;
            _currentAlfa = 1;
        }
        if (_image.color.a <= _minAlfa)
        {
            _sensitivity *= -1;
            _currentAlfa = _minAlfa;
        }

        _currentAlfa += _sensitivity;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _currentAlfa);
    }

    private void OnValidate()
    {
        if (_image == null)
            _image = GetComponent<Image>();
    }
}
