using UnityEngine;

public class MiniGame : MonoBehaviour
{
    protected ClickCountManager clickCountManager;
    protected MiniGameManager miniGameManager;

    private void Awake()
    {
        clickCountManager = FindObjectOfType<ClickCountManager>();
        miniGameManager = FindObjectOfType<MiniGameManager>();
        miniGameManager.miniGames.Add(this);
    }

    public virtual void OnMiniGameStart() { }
    public virtual void OnMiniGameEnd() 
    {
        miniGameManager.EmptyMiniGame();
    }

    public virtual void OnUpdate() { }
}
