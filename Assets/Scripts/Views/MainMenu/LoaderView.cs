using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

public class LoaderView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _viewPercent;
    [SerializeField] private List<BlockProgressBarView> _blocksProgressBar;

    [HideInInspector]
    public UnityEvent FinishLoadingSceneEventHandler = new UnityEvent();

    public async void UpdatePercentLoading(float valuePercent)
    {
        _viewPercent.text = valuePercent.ToString() + "%";
        ShowBlocksProgressLoader(valuePercent);
        if (valuePercent == 100)
        {
            await Task.Delay(TimeSpan.FromSeconds(1.5));
            FinishLoadingSceneEventHandler.Invoke();
        }
    }

    public void SetActive(bool value) => gameObject.SetActive(value);

    public void Subscribe()
    {
        ManagerScenes.Instance.LoadingSceneEventHandler.AddListener((valuePercent) => { UpdatePercentLoading(valuePercent); });
        ManagerScenes.Instance.StartLoadingSceneEventHandler.AddListener(() => { SetActive(true); });
        FinishLoadingSceneEventHandler.AddListener(() =>
        {
            SetActive(false);
            _viewPercent.text = "0%";
        });
        FinishLoadingSceneEventHandler.AddListener(ManagerScenes.Instance.SetLoadedScene);
        FinishLoadingSceneEventHandler.AddListener(HideBlocksProgressLoader);
    }

    private void ShowBlocksProgressLoader(float valuePercent)
    {
        for (int i = 0; i < (valuePercent * _blocksProgressBar.Count) / 100; i++)
        {
            if (i < _blocksProgressBar.Count)
            {
                if (_blocksProgressBar[i] != null)
                    _blocksProgressBar[i].SetActive(true);
            }
        }
    }

    private void HideBlocksProgressLoader()
    {
        foreach(var progressBlock in _blocksProgressBar)
            progressBlock.SetActive(false);
    }

    private void Start()
    {
        HideBlocksProgressLoader();
        LoadingStartGame();
    }

    private async void LoadingStartGame()
    {
        for (int i = 0; i < 101; i++)
        {
            UpdatePercentLoading(i);
            await Task.Delay(20);
        }
    }
}
