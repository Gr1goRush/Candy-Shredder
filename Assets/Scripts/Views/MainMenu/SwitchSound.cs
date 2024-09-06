public class SwitchSound : SwitchToggleView
{
    public override int LoadDataState() => ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnSound;

    public override void SaveDataState(int value)
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnSound = value;

        if (value == 0)
            AudioManager.Instance.StopSoundAction();
    }
}