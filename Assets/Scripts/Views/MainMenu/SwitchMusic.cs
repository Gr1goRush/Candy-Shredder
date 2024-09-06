public class SwitchMusic : SwitchToggleView
{
    public override int LoadDataState() => ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnMusic;

    public override void SaveDataState(int value)
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnMusic = value;

        if (value == 0)
            AudioManager.Instance.StopSound();
        else
            AudioManager.Instance.ChangeSound();

    }
}
