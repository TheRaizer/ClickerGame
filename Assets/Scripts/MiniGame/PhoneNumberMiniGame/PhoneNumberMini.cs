using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneNumberMini : MiniGame
{
    [SerializeField] private TextMeshProUGUI[] textNumbers = new TextMeshProUGUI[10];
    [SerializeField] private TextMeshProUGUI timerText = null;

    private List<string> phoneNumbers = new List<string>();
    private string guessedPhoneNumber = "";

    private int numbersEntered = 0;
    private int currentPhoneNumber = 0;

    private int phoneNumsToGenerate = 0;
    private const int NUM_TO_GENERATE_MAX = 5;
    private const int NUM_TO_GENERATE_MIN = 3;

    private float timer = 0;
    private const float TIME_ALLOTED_MIN = 60;
    private const float TIME_ALLOTED_MAX = 90;

    public override void OnMiniGameStart()
    {
        base.OnMiniGameStart();
        GeneratePhoneNumber();
        timer = Random.Range(TIME_ALLOTED_MIN, TIME_ALLOTED_MAX);
    }

    public override void OnMiniGameEnd()
    {
        base.OnMiniGameEnd();
        phoneNumbers.Clear();
        guessedPhoneNumber = "";
        foreach (TextMeshProUGUI t in textNumbers)
        {
            t.text = "";
        }
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
        textNumbers[numbersEntered].text = "";
        guessedPhoneNumber.Remove(guessedPhoneNumber.Length - 1);
        guessedPhoneNumber.Trim();
        numbersEntered--;
    }

    public void AddNumberToGuess(int num)
    {
        guessedPhoneNumber += num.ToString();
        textNumbers[numbersEntered].text = num.ToString();
        numbersEntered++;
    }

    public void CheckIfCorrect()
    {
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
        foreach(TextMeshProUGUI t in textNumbers)
        {
            t.text = "";
        }
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
}
