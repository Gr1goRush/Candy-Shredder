using System;
using UnityEngine;

public class SaveerDataInPlayerPrefs : SaveerData
{
    private readonly string _keyNumberLevel = "Level";
    private readonly string _keyCoins = "Coins";
    private readonly string _keyMoney = "Money";
    private readonly string _keyIsTurnSound = "IsTurnSound";
    private readonly string _keyIsTurnMusic = "IsTurnMusic";
    private readonly string _keyCountBalls = "CountBalls";
    private readonly string _keyCountPlaforms = "CountPlaforms";
    private readonly string _keyPurchasedBackgrounds = "PurchasedBackgrounds";
    private readonly string _keyCurrentBackground = "CurrentBackground";
    private readonly string _keyCurrentSound = "CurrentSound";
    private readonly string _keyPurchasedSounds = "PurchasedSounds";

    private const int _countLevels = 5;

    public int Level { get { return Load<int>(_keyNumberLevel, 1); } set { Save<int>(_keyNumberLevel, value); } }
    public int Coins { get { return Load<int>(_keyCoins, 0); } set { Save<int>(_keyCoins, value); } }
    public int Money { get { return Load<int>(_keyMoney, 0); } set { Save<int>(_keyMoney, value); } }
    public int IsTurnSound { get { return Load<int>(_keyIsTurnSound, 1); } set { Save<int>(_keyIsTurnSound, value); } }
    public int IsTurnMusic { get { return Load<int>(_keyIsTurnMusic, 1); } set { Save<int>(_keyIsTurnMusic, value); } }
    public int CountBalls { get { return Load<int>(_keyCountBalls, 1); } set { Save<int>(_keyCountBalls, value); } }
    public int CountPlaforms { get { return Load<int>(_keyCountPlaforms, 1); } set { Save<int>(_keyCountPlaforms, value); } }
    public string PurchasedBackgrounds { get { return Load<string>(_keyPurchasedBackgrounds, "base"); } set { Save<string>(_keyPurchasedBackgrounds, value); } }
    public string CurrentBackground { get { return Load<string>(_keyCurrentBackground, "base"); } set { Save<string>(_keyCurrentBackground, value); } }
    public string CurrentSound { get { return Load<string>(_keyCurrentSound, "Standart"); } set { Save<string>(_keyCurrentSound, value); } }
    public string PurchasedSounds { get { return Load<string>(_keyPurchasedSounds, "Standart"); } set { Save<string>(_keyPurchasedSounds, value); } }
    public int CountLevels => _countLevels;

    public override T Load<T>(string nameParameter, T defaultValue)
    {
        if (PlayerPrefs.HasKey(nameParameter) == false)
            return defaultValue;

        Type inType = typeof(T);

        if (inType == typeof(int))
            return (T)(object)PlayerPrefs.GetInt(nameParameter);
        else if (inType == typeof(float))
            return (T)(object)PlayerPrefs.GetFloat(nameParameter);
        else
            return (T)(object)PlayerPrefs.GetString(nameParameter);
    }

    public override void Save<T>(string nameParameter, T value)
    {
        Type inType = typeof(T);

        if (inType == typeof(int))
            PlayerPrefs.SetInt(nameParameter, int.Parse(value.ToString()));
        else if (inType == typeof(float))
            PlayerPrefs.SetFloat(nameParameter, float.Parse(value.ToString()));
        else if (inType == typeof(string))
            PlayerPrefs.SetString(nameParameter, value.ToString());
    }
}
