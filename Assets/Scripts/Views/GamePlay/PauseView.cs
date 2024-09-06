using UnityEngine.Events;

public class PauseView : InfoBonusesView
{
    public UnityEvent ResumeGameEventHandler = new UnityEvent();

    protected override void Start()
    {
        base.Start();
        _againGame.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            ResumeGameEventHandler?.Invoke();
        });
    }
}
