using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneNumberMini : MiniGame
{
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private TextMeshProUGUI guessText = null;
    [SerializeField] private List<GameObject> objectsToDisableOnStart = null;
    [SerializeField] private List<GameObject> objectsToEnableOnStart = null;

    private readonly List<string> phoneNumbers = new List<string>();
    private string guessedPhoneNumber = "";

    private int numbersEntered = 0;
    private int currentPhoneNumber = 0;

    private int phoneNumsToGenerate = 0;
    private const int NUM_TO_GENERATE_MAX = 5;
    private const int NUM_TO_GENERATE_MIN = 3;

    private float timer = 0;
    private const float TIME_ALLOTED_MIN = 60;
    private const float TIME_ALLOTED_MAX = 90;
    private const int MAX_NUMBER_OF_NUMS = 10;

    public override void OnMiniGameStart()
    {
        base.OnMiniGameStart();
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
        guessedPhoneNumber = guessedPhoneNumber.Replace("-", "");

        if(guessedPhoneNumber == phoneNumbers[currentPhoneNumber])
        {
            if (currentPhoneNumber == phoneNumsToGenerate)
            {
                //has won
            }
            currentPhoneNumber++;
        }
        else
        {
            //do something when wrong like flash red
        }

        guessedPhoneNumber = "";
        UpdateGuess();
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
            }
            phoneNumbers.Add(s);
        }
    }

    private void UpdateGuess() => guessText.text = guessedPhoneNumber;

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
