using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneNumberMini : MiniGame
{
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private TextMeshProUGUI guessText = null;
    [SerializeField] private TextMeshProUGUI answerText = null;

    [SerializeField] private List<GameObject> objectsToDisableOnStart = null;
    [SerializeField] private List<GameObject> objectsToEnableOnStart = null;

    [SerializeField] private float rewardMultiplierLength = 0;
    [SerializeField] private float rewardMultiplierIncrease = 0;

    private readonly List<string> phoneNumbers = new List<string>();
    private string guessedPhoneNumber = "";

    private int numbersEntered = 0;
    private int currentPhoneNumber = 0;

    private int phoneNumsToGenerate = 0;
    private const int NUM_TO_GENERATE_MAX = 1;
    private const int NUM_TO_GENERATE_MIN = 1;

    private float timer = 0;
    private const float TIME_ALLOTED_MIN = 30;
    private const float TIME_ALLOTED_MAX = 45;
    private const int MAX_NUMBER_OF_NUMS = 10;

    public override void OnMiniGameVictory()
    {
        clickCountManager.IncreaseBuffClickMultiplierTimed(rewardMultiplierIncrease, rewardMultiplierLength);
        base.OnMiniGameVictory();
    }

    public override void OnMiniGameStart()
    {
        base.OnMiniGameStart();
        numbersEntered = 0;
        currentPhoneNumber = 0;
        guessText.text = "";

        GeneratePhoneNumber();
        OnEnterChangeActive();

        timer = Random.Range(TIME_ALLOTED_MIN, TIME_ALLOTED_MAX);
    }

    public override void OnMiniGameEnd()
    {
        phoneNumbers.Clear();
        guessedPhoneNumber = "";

        UpdateGuess();
        OnExitChangeActive();
        base.OnMiniGameEnd();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            OnMiniGameEnd();//has lost
        }
        timerText.text = "Time left: " + Mathf.RoundToInt(timer);
    }

    public void DeleteNumber()
    {
        if (numbersEntered > 0)
        {
            numbersEntered--;
            guessedPhoneNumber = guessedPhoneNumber.Remove(guessedPhoneNumber.Length - 1);
            if (numbersEntered == 3 || numbersEntered == 6)
            {
                guessedPhoneNumber = guessedPhoneNumber.Remove(guessedPhoneNumber.Length - 1);
            }
            UpdateGuess();
        }
    }

    public void AddNumberToGuess(int num)
    {
        if (numbersEntered < MAX_NUMBER_OF_NUMS)
        {
            numbersEntered++;
            if (numbersEntered == 4 || numbersEntered == 7)
            {
                guessedPhoneNumber += "-";
            }
            guessedPhoneNumber += num.ToString();
            UpdateGuess();
        }
    }

    public void CheckIfCorrect()
    {
        if(guessedPhoneNumber == phoneNumbers[currentPhoneNumber])
        {
            if (currentPhoneNumber == phoneNumsToGenerate - 1)
            {
                OnMiniGameVictory();
            }
            else
            {
                currentPhoneNumber++;
                UpdateExpectedNumber();
            }
        }
        else
        {
            //do something when wrong like flash red
        }

        guessedPhoneNumber = "";
        UpdateGuess();
        numbersEntered = 0;
    }


    private void GeneratePhoneNumber()
    {
        phoneNumsToGenerate = Random.Range(NUM_TO_GENERATE_MIN, NUM_TO_GENERATE_MAX + 1);

        for (int j = 0; j < phoneNumsToGenerate; j++)
        {
            string s = "";
            for (int i = 0; i < 10; i++)
            {
                int num = Random.Range(0, 10);
                s += num.ToString();
                if(i == 2 || i == 5)
                {
                    s += "-";
                }
            }
            phoneNumbers.Add(s);
        }
        UpdateExpectedNumber();
    }

    private void UpdateGuess() => guessText.text = guessedPhoneNumber;
    private void UpdateExpectedNumber() => answerText.text = "Call: " + phoneNumbers[currentPhoneNumber];

    private void OnEnterChangeActive()
    {
        foreach(GameObject g in objectsToDisableOnStart)
        {
            g.SetActive(false);
        }
        foreach(GameObject g in objectsToEnableOnStart)
        {
            g.SetActive(true);
        }
    }
    private void OnExitChangeActive()
    {
        foreach (GameObject g in objectsToDisableOnStart)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in objectsToEnableOnStart)
        {
            g.SetActive(false);
        }
    }
}
