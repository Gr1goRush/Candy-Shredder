using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using AppsFlyerSDK;

public class AppsFlyerObjectScript : MonoBehaviour, IAppsFlyerConversionData
{
    [SerializeField] private string _devKey, _appID;

    public static Dictionary<string, object> DeepLinkParamsDictionary;
    public static UnityEvent<bool> OnDeepLinkProcessingSuccesfullyDone;
    public static bool? IsDeepLink;

    private void Awake()
    {
        DeepLinkParamsDictionary = null;
        OnDeepLinkProcessingSuccesfullyDone = new UnityEvent<bool>();
        IsDeepLink = null;
    }

    private void Start()
    {
        AppsFlyer.setIsDebug(false);
        AppsFlyer.initSDK(_devKey, _appID, this);

        AppsFlyer.OnDeepLinkReceived += OnDeepLink;

        AppsFlyer.waitForATTUserAuthorizationWithTimeoutInterval(3);

        AppsFlyer.startSDK();
    }

    private void OnDeepLink(object sender, EventArgs args)
    {
        var deepLinkEventArgs = args as DeepLinkEventsArgs;

        switch (deepLinkEventArgs.status)
        {
            case DeepLinkStatus.FOUND:
                if (deepLinkEventArgs.isDeferred())
                    AppsFlyer.AFLog("OnDeepLink", "This is a deferred deep link");
                else
                    AppsFlyer.AFLog("OnDeepLink", "This is a direct deep link");

#if UNITY_IOS && !UNITY_EDITOR
                if (deepLinkEventArgs.deepLink.ContainsKey("click_event") && deepLinkEventArgs.deepLink["click_event"] != null) 
                {
                    DeepLinkParamsDictionary = deepLinkEventArgs.deepLink["click_event"] as Dictionary<string, object>;

                    IsDeepLink = true;

                    OnDeepLinkProcessingSuccesfullyDone?.Invoke(IsDeepLink.Value);
                }

                else
                {
                    IsDeepLink = false;

                    OnDeepLinkProcessingSuccesfullyDone?.Invoke(IsDeepLink.Value);
                }
#endif

                break;
            case DeepLinkStatus.NOT_FOUND:
                AppsFlyer.AFLog("OnDeepLink", "Deep link not found");
                IsDeepLink = false;

                OnDeepLinkProcessingSuccesfullyDone?.Invoke(IsDeepLink.Value);

                break;

            default:
                AppsFlyer.AFLog("OnDeepLink", "Deep link error");
                IsDeepLink = false;

                OnDeepLinkProcessingSuccesfullyDone?.Invoke(IsDeepLink.Value);

                break;
        }
    }

    public void onConversionDataSuccess(string popoxc)
    {
        AppsFlyer.AFLog("didReceiveConversionData", popoxc);

        Dictionary<string, object> convData = AppsFlyer.CallbackStringToDictionary(popoxc);

        string aghsd = "";

        if (convData.ContainsKey("campaign"))
        {
            object conv = null;

            if (convData.TryGetValue("campaign", out conv))
            {
                string[] list = conv.ToString().Split('_');

                if (list.Length > 0)
                {
                    aghsd = "&";

                    for (int a = 0; a < list.Length; a++)
                    {
                        aghsd += string.Format("sub{0}={1}", (a + 1), list[a]);

                        if (a < list.Length - 1)
                            aghsd += "&";
                    }
                }
            }
        }
        PlayerPrefs.SetString("glrobo", aghsd);
    }

    public void onConversionDataFail(string error)
    {
        AppsFlyer.AFLog("didReceiveConversionDataWithError", error);

        PlayerPrefs.SetString("glrobo", "");
    }

    public void onAppOpenAttribution(string attributionData)
    {
        AppsFlyer.AFLog("onAppOpenAttribution", attributionData);

        PlayerPrefs.SetString("glrobo", "");
    }

    public void onAppOpenAttributionFailure(string error)
    {
        AppsFlyer.AFLog("onAppOpenAttributionFailure", error);

        PlayerPrefs.SetString("glrobo", "");
    }

    private void OnDestroy() => AppsFlyer.OnDeepLinkReceived -= OnDeepLink;
}
