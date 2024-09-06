using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ManagerScenes : MonoBehaviour
{
    private bool _isLoadedScene;

    [SerializeField] private LoaderView _loaderView;

    public static ManagerScenes Instance { get; private set; }
    public LoaderView Loader => _loaderView;
    public string NameActiveScene => SceneManager.GetActiveScene().name;
    [HideInInspector]
    public UnityEvent<float> LoadingSceneEventHandler = new UnityEvent<float>();
    [HideInInspector]
    public UnityEvent StartLoadingSceneEventHandler = new UnityEvent();

    public void LoadAsyncFromCoroutine(string nameScene) => StartCoroutine(LoadAsync(nameScene));

    public void SetLoadedScene() => _isLoadedScene = true;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
        DontDestroyOnLoad(this);
        _loaderView.Subscribe();
        _isLoadedScene = false;
    }

    private IEnumerator LoadAsync(string nameScene)
    {   
        _isLoadedScene = false;
        var operation = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);
        StartLoadingSceneEventHandler?.Invoke();

        while (operation.progress <= 1)
        {
            var progressInPercent = (int)(operation.progress * 100);
            LoadingSceneEventHandler?.Invoke(progressInPercent);

            if (_isLoadedScene)
                break;

            yield return null;
        }
    }
}
