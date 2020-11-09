using UnityEngine;

public class MiniGameObject : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 0f;
    private MiniGameManager miniGameManager;
    public MiniGame MiniGame { get; set; }
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        miniGameManager = FindObjectOfType<MiniGameManager>();
    }

    private void Update()
    {
        rect.Translate(new Vector3(0, -fallSpeed * Time.deltaTime), Space.Self);
    }

    public void OnPress()
    {
        miniGameManager.ChangeMiniGame(MiniGame);
    }
}
