using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityFigmaBridge.Runtime.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BlocksCandyView _blocksCandyView;
    [SerializeField] private PlatformView _platformView;
    [SerializeField] private BulletView _bulletViewPrefab;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private List<BonusView> _bonuses;
    [SerializeField] private Transform _canvas;
    [SerializeField] private List<AllBonusesView> _allBonusesView;
    [SerializeField] private FigmaImage _background;
    [SerializeField] private List<BackgroundGame> _backgrounds;
    [SerializeField] private InputMouseBullet _inputMouseBullet;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private LevelManager _levelManager;

    private BlocksCandyController _candyLineStorageController;
    private PlatformController _platformController;
    private BulletController _bulletController;
    private BonusController _bonusController;

    private Platform _platform;

    private void Start()
    {
        TimerForLevel();
        _candyLineStorageController = new BlocksCandyController(_blocksCandyView);
        _candyLineStorageController.Initialize();

        _platform = new Platform(_platformView.StartPosition);
        _platformController = new PlatformController(_platformView, _platform);
        _platformController.Initialize();

        _bulletController = new BulletController();
        _bulletController.CreateBullets(_bulletViewPrefab, _background.transform, _platformView.transform.position);

        _bonusController = new BonusController(_bonuses, _platformView.transform, _background.transform);
        _bonusController.SetViewAllBonuses(_allBonusesView);

        _inputMouseBullet.InputEventHandler.AddListener(_bulletController.OnInputEvent);
        _platformView.Input.OnMouseFromPlatformEventHandler.AddListener(_inputMouseBullet.OnMouseFromPlatform);
        _platform.OnUpdatePositionEventHandler.AddListener(_bulletController.OnChangePostionPlatform);
        _platformView.OnGameOverEvetHandler.AddListener(() =>
        {
            var coinsView = _allBonusesView.Find(bonuseView => bonuseView.BonusType == TypeBonus.Coin);
            var moneyView = _allBonusesView.Find(bonuseView => bonuseView.BonusType == TypeBonus.Money);

            _gameOverView.ViewCountBonuses(moneyView.CountBonus, coinsView.CountBonus);

            ContainerSaveerPlayerPrefs.Instance.SaveerData.Money += moneyView.CountBonus;
            if (coinsView.CountBonus > ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins)
                ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins = coinsView.CountBonus;

            _gameOverView.OnGameOver();
            SetDefaultValues();
            _background.gameObject.SetActive(false);
        });

        foreach (var listCandyLine in _blocksCandyView.ListCandyLine)
            listCandyLine.AddListenerBrokenCandy(_bonusController.OnBrokeCandy);

        LoadBackground();
        LoadImprovements();

        _pauseButton.onClick.AddListener(() =>
        {
            _blocksCandyView.IncrementPerSecond = -1;
            _pauseView.gameObject.SetActive(true);
            _inputMouseBullet.InputEventHandler.RemoveAllListeners();
        });
        _pauseView.ResumeGameEventHandler.AddListener(() =>
        {
            TimerForLevel();
            _candyLineStorageController.OnResumeGame();
            _candyLineStorageController.UpdateCandyLine();
            _inputMouseBullet.InputEventHandler.AddListener(_bulletController.OnInputEvent);
        });
    }

    private void TimerForLevel()
    {
        switch (ContainerSaveerPlayerPrefs.Instance.SaveerData.Level)
        {
            case 1:
                _blocksCandyView.IncrementPerSecond = 16;
                break;
            case 2:
                _blocksCandyView.IncrementPerSecond = 12;
                break;
            case 3:
                _blocksCandyView.IncrementPerSecond = 8;
                break;
            case 4:
                _blocksCandyView.IncrementPerSecond = 4;
                break;
            case 5:
                _blocksCandyView.IncrementPerSecond = 2;
                break;
        }
    }

    private void LoadBackground()
    {
        var currentBackground = ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentBackground;
        var findedBackground = _backgrounds.Find(item => item.Name == currentBackground);

        if (findedBackground != null)
        {
            _background.sprite = findedBackground.Sprite;
            _background.FillColor = new Color(0.3019608f, 0.2941177f, 0.2941177f);
        }
    }

    private void LoadImprovements()
    {
        _platformView.SetSprite(ContainerSaveerPlayerPrefs.Instance.SaveerData.CountPlaforms);
    }

    private void OnValidate()
    {
        if (_inputMouseBullet == null)
            _inputMouseBullet = GetComponent<InputMouseBullet>();

        if (_levelManager == null)
            _levelManager = GetComponent<LevelManager>();
    }

    private void SetDefaultValues()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CountPlaforms = 1;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CountBalls = 1;
    }
}