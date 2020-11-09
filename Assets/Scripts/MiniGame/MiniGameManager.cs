using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private float chanceToRollMiniGame = 100f;
    [SerializeField] private float intervalToRoll = 1f;
    [SerializeField] private RectTransform veryRightSpawn = null;
    [SerializeField] private RectTransform rightSpawn = null;
    [SerializeField] private RectTransform veryLeftSpawn = null;
    [SerializeField] private RectTransform leftSpawn = null;

    [SerializeField] private float timer = 0f;

    public List<MiniGame> miniGames = new List<MiniGame>();
    private MiniGame currentMiniGame = null;
    private ObjectPooler objectPooler;

    private void Awake()
    {
        timer = intervalToRoll;
        objectPooler = FindObjectOfType<ObjectPooler>();
        Debug.Log(rightSpawn.anchoredPosition.x);
    }

    private void Update()
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

                    int randomMiniGame = Random.Range(0, miniGames.Count);
                    Vector3 anchor = fallingMiniGame.GetComponent<RectTransform>().anchoredPosition3D;
                    anchor = new Vector3(anchor.x, anchor.y, 0);
                    fallingMiniGame.GetComponent<RectTransform>().anchoredPosition3D = anchor;

                    fallingMiniGame.GetComponent<MiniGameObject>().MiniGame = miniGames[randomMiniGame];
                    Debug.Log("enter minigame");
                    //drop spawnMinigame symbol which will fall and may or may not be clicked.
                }
            }
        }
        else
        {
            timer = intervalToRoll;
        }

        if(currentMiniGame != null)
        {
            currentMiniGame.OnUpdate();
        }
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
}
