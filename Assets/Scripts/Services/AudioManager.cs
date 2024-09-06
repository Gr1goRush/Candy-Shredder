using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourceActions;
    [SerializeField] private List<Sound> _sounds;
    [SerializeField] private List<Sound> _soundsAction;
    [Header("Кол-во мсек. прослушивания платной музыки в магазине")]
    [SerializeField] private int _secondsPlayPaidMusic = 4000;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SetDefaultTools();
        ManagerScenes.Instance.Loader.FinishLoadingSceneEventHandler.AddListener(ChangeSound);
        ManagerScenes.Instance.StartLoadingSceneEventHandler.AddListener(StopSound);
    }

    private void SetDefaultTools()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnMusic = ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnMusic;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnSound = ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnSound;
    }

    public void ChangeSound()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnMusic == 0)
            return;

        switch (ManagerScenes.Instance.NameActiveScene)
        {
            case "MainMenu":
                {
                    PlaySound("Menu");
                    break;
                }
            case "Game":
                {
                    PlaySound(ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSound);
                    break;
                }
        }
    }

    public void PlaySound(string nameSound)
    {
        var findedSound = FindSound(nameSound);
        var findedSoundAction = FindSoundAction(nameSound);

        if (findedSound != null && ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnMusic == 1)
        {
            _audioSource.clip = findedSound.Music;
            _audioSource.loop = findedSound.IsLoop;
            _audioSource.PlayDelayed(0.1f);
        }

        if (findedSoundAction != null && ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnSound == 1)
        {
            _audioSourceActions.clip = findedSoundAction.Music;
            _audioSourceActions.loop = findedSoundAction.IsLoop;
            _audioSourceActions.PlayDelayed(0.1f);
        }
    }

    public async void PlayPartSound(string nameSound)
    {
        var findedSound = FindSound(nameSound);
        if (findedSound != null)
        {
            _audioSource.clip = findedSound.Music;
            _audioSource.loop = findedSound.IsLoop;
            _audioSource.PlayDelayed(0.1f);
            await Task.Delay(_secondsPlayPaidMusic);
            if (_audioSource.clip.name == findedSound.Music.name)
            {
                if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsTurnMusic == 0)
                    StopSound();
                else
                    ChangeSound();
            }
        }
    }

    public void StopSoundAction() =>
        _audioSourceActions.Stop();

    public void StopSound() =>
        _audioSource.Stop();

    private Sound FindSound(string nameSound) =>
        _sounds.Find(sound => sound.Name == nameSound);

    private Sound FindSoundAction(string nameSound) =>
    _soundsAction.Find(sound => sound.Name == nameSound);
}
