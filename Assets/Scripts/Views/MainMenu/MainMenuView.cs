using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startGame;
    [SerializeField] private Button _help;
    [SerializeField] private Button _shop;
    [SerializeField] private Button _tools;
    [SerializeField] private ItemView _helpPlayView;
    [SerializeField] private ToolsView _toolsView;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private TextMeshProUGUI _viewMaxScore;

    private void Start()
    {
        _startGame.onClick.AddListener(() =>
        {
            ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
            transform.parent.gameObject.SetActive(false);
        });

        _help.onClick.AddListener(_helpPlayView.Show);
        _tools.onClick.AddListener(_toolsView.Show);
        _shop.onClick.AddListener(_shopView.Show);

        _viewMaxScore.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins.ToString();
    }
}
