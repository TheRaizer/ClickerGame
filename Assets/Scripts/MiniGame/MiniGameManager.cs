using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private float chanceToRollMiniGame = 100f;
    [SerializeField] private float intervalToRoll = 1f;
    [SerializeField] private RectTransform veryRightSpawn = null;
    [SerializeField] private RectTransform rightSpawn = null;
    [SerializeField] private RectTransform veryLeftSpawn = null;
    [SerializeField] private RectTransform leftSpawn = null;

    [SerializeField] private float timer = 0f;

    public List<MiniGame> MiniGames { get; set; } = new List<MiniGame>();
    private MiniGame currentMiniGame = null;
    private ObjectPooler objectPooler;

    private void Awake()
    {
        timer = intervalToRoll;
        objectPooler = FindObjectOfType<ObjectPooler>();
    }

    private void Start()
    {
        LoadMiniGameDataIntoVariables();
    }

    private void Update()
    {
        ManageFallingObjects();

        if (currentMiniGame != null)
        {
            currentMiniGame.OnUpdate();
        }
    }

    public MiniGameData GenerateMiniGameData()
    {
        MiniGameData miniGameData = new MiniGameData()
        {
            timer = timer
        };

        return miniGameData;
    }

    public void ChangeMiniGame(MiniGame miniGame)
    {
        currentMiniGame = miniGame;
        currentMiniGame.OnMiniGameStart();
    }
    public void EmptyMiniGame()
    {
        currentMiniGame = null;
    }

    private void ManageFallingObjects()
    {
        if (currentMiniGame == null)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = intervalToRoll;
                float loadMiniGame = Random.Range(0, 101);
                if (loadMiniGame <= chanceToRollMiniGame)
                {
                    float posX;
                    float posY;
                    bool spawnRight = Random.Range(0, 2) == 1;
                    if (spawnRight)
                    {
                        posX = Random.Range(rightSpawn.anchoredPosition.x, veryRightSpawn.anchoredPosition.x);
                        posY = rightSpawn.anchoredPosition.y;
                    }
                    else
                    {
                        posX = Random.Range(leftSpawn.anchoredPosition.x, veryLeftSpawn.anchoredPosition.x);
                        posY = leftSpawn.anchoredPosition.y;
                    }

                    GameObject fallingMiniGame = objectPooler.SpawnUIObject(new Vector2(posX, posY), Quaternion.identity, "FallingMiniGame");

                    int randomMiniGame = Random.Range(0, MiniGames.Count);
                    fallingMiniGame.GetComponent<RectTransform>().ZeroOutZ();

                    MiniGameObject miniGameObject = fallingMiniGame.GetComponent<MiniGameObject>();
                    miniGameObject.MiniGame = MiniGames[randomMiniGame];
                    fallingMiniGame.GetComponent<Image>().sprite = miniGameObject.MiniGame.SpriteToFall;
                }
            }
        }
        else
        {
            timer = intervalToRoll;
        }
    }
    private void LoadMiniGameDataIntoVariables()
    {
        MiniGameData data = SaveSystem.Instance.LoadMiniGameData();
        timer = data.timer;
    }

}
