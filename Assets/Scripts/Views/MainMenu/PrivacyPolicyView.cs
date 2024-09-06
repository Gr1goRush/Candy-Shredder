using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicyView : MonoBehaviour
{
    [SerializeField] private Button _privacyPolicy;
    [SerializeField] private Button _back;
    [SerializeField] private GameObject _dataPrivacyPolicy;
    [SerializeField] private ItemView _itemView;

    private void Start()
    {
        _privacyPolicy.onClick.AddListener(() =>
        {
            _back.onClick.RemoveAllListeners();
            _back.onClick.AddListener(() =>
            {
                transform.parent.gameObject.SetActive(true);
                _dataPrivacyPolicy.gameObject.SetActive(false);
                _itemView.SubscribeToBackButton();
            });
            transform.parent.gameObject.SetActive(false);
            _dataPrivacyPolicy.gameObject.SetActive(true);
        });
    }

    private void OnValidate()
    {
        if (_privacyPolicy == null)
            _privacyPolicy = transform.GetComponent<Button>();
    }
}
