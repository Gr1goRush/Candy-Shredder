using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.UI;

public class ToolsView : ItemView
{
    [SerializeField] Button _privacyPolicy;
    [SerializeField] Button _sound;
    [SerializeField] Button _music;
    [SerializeField] Button _rateOurGame;

    private void Start()
    {
        _rateOurGame.onClick.AddListener(() => { Device.RequestStoreReview(); });
    }
}
