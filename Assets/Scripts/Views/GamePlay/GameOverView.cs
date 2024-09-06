using UnityEngine;

public class GameOverView : InfoBonusesView
{
    [SerializeField] private Sound _soundActionGameOver;

    public void OnGameOver()
    {
        AudioManager.Instance.PlaySound(_soundActionGameOver.Name);
        PoolObjects<BonusView>.Clear();
        gameObject.SetActive(true);
    }

    protected override void Start()
    {
        base.Start();
        _againGame.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
        });
    }
}
