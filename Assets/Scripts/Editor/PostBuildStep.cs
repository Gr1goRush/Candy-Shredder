using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.IO;

public class PostBuildStep
{
    private static string _trackingDescription = Application.productName.ToString() + " requests permission to track user data for analytics, aiming to improve the game by understanding when players usually close it";
    private static string _advertisingAttributionDescription = "https://appsflyer-skadnetwork.com/";

    [PostProcessBuild(0)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode)
    {
        if (buildTarget == BuildTarget.iOS)
            AddPListValues(pathToXcode);
    }

    static void AddPListValues(string pathToXcode)
    {
        string plistPath = pathToXcode + "/Info.plist";
        PlistDocument plistObj = new PlistDocument();

        plistObj.ReadFromString(File.ReadAllText(plistPath));

        PlistElementDict plistRoot = plistObj.root;

        plistRoot.SetString("NSUserTrackingUsageDescription", _trackingDescription);
        plistRoot.SetString("NSAdvertisingAttributionReportEndpoint", _advertisingAttributionDescription);

        File.WriteAllText(plistPath, plistObj.WriteToString());
    }
}
