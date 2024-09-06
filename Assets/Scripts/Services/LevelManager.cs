using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private float _nextNeedScore;

    [SerializeField] private float _scoreStep;
    [SerializeField] private TextMeshProUGUI _viewScore;
    [SerializeField] private Sound _soundNextLevel;

    [HideInInspector]
    public UnityEvent ChangedLevelEventHandler = new UnityEvent();

    private void Start()
    {
        _nextNeedScore = _scoreStep;
    }

    private void FixedUpdate()
    {
        CheckOnGoNextLevel();
    }

    private void CheckOnGoNextLevel()
    {
        if (int.Parse(_viewScore.text) >= _nextNeedScore)
        {
            ChangedLevelEventHandler?.Invoke();
            _nextNeedScore = int.Parse(_viewScore.text) + _scoreStep;
            if (ContainerSaveerPlayerPrefs.Instance.SaveerData.Level < ContainerSaveerPlayerPrefs.Instance.SaveerData.CountLevels)
            {
                ContainerSaveerPlayerPrefs.Instance.SaveerData.Level += 1;
                AudioManager.Instance.PlaySound(_soundNextLevel.Name);
            }
        }
    }

    private void OnDestroy()
    {
        ChangedLevelEventHandler.RemoveAllListeners();
    }
}
