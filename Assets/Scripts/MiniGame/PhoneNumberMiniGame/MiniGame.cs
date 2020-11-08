using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [field: SerializeField] public float ChanceToAppear { get; private set; } = 100f;
    protected ClickCountManager clickCountManager;

    private void Awake()
    {
        clickCountManager = FindObjectOfType<ClickCountManager>();
    }

    public virtual void OnMiniGameStart() { }
    public virtual void OnMiniGameEnd() { }

    public virtual void OnUpdate() { }
}
