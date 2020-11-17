using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [field: SerializeField] public Sprite SpriteToFall { get; private set; }
    protected ClickCountManager clickCountManager;
    protected MiniGameManager miniGameManager;

    private void Awake()
    {
        clickCountManager = FindObjectOfType<ClickCountManager>();
        miniGameManager = FindObjectOfType<MiniGameManager>();
        miniGameManager.MiniGames.Add(this);
    }

    public virtual void OnMiniGameStart() { }
    public virtual void OnMiniGameEnd() 
    {
        miniGameManager.EmptyMiniGame();
    }

    public virtual void OnMiniGameVictory()
    {
        OnMiniGameEnd();
    }

    public virtual void OnUpdate() { }
}
