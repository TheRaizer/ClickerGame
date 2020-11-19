using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneNumberMini : MiniGame
{
    [Header("Animations")]
    [SerializeField] private Animator animator = null;

    [Header("Text Components")]
    [SerializeField] private Text timerText = null;
    [SerializeField] private Text guessText = null;
    [SerializeField] private Text answerText = null;

    [Header("Punishments")]
    [SerializeField] private float timeDeductionOnWrong = 5f;
    [SerializeField] private ImageFade filterFlash = null;

    [Header("Objects to Manage")]
    [SerializeField] private GameObject miniGamePhone = null;
    [SerializeField] private List<GameObject> objectsToDisableOnStart = null;
    [SerializeField] private List<Image> dialNumbers = null;

    [Header("Rewards")]
    [SerializeField] private float rewardMultiplierLength = 0;
    [SerializeField] private float rewardMultiplierIncrease = 0;


    private readonly List<string> phoneNumbers = new List<string>();
    private string guessedPhoneNumber = "";

    private int numbersEntered = 0;
    private int currentPhoneNumber = 0;

    private int phoneNumsToGenerate = 0;
    private const int NUM_TO_GENERATE_MAX = 3;
    private const int NUM_TO_GENERATE_MIN = 2;

    private float timer = 0;
    private const float TIME_ALLOTED_MIN = 30;
    private const float TIME_ALLOTED_MAX = 45;
    private const int MAX_NUMBER_OF_NUMS = 10;

    public override void OnMiniGameVictory()
    {
        clickCountManager.IncreaseBuffClickMultiplierTimed(rewardMultiplierIncrease, rewardMultiplierLength);
        animator.SetTrigger("SlideOut");
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
        animator.SetTrigger("SlideIn");
    }

    public override void OnMiniGameEnd()
    {
        phoneNumbers.Clear();
        guessedPhoneNumber = "";

        UpdateGuess();
        OnExitChangeActive();
        filterFlash.ResetColor();
        animator.SetTrigger("SlideOut");
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
            if (currentPhoneNumber == phoneNumsToGenerate - 1)//is it the final number
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
            filterFlash.UnFade = true;
            timer -= timeDeductionOnWrong;//do something to signal this
            if(timer < 0)
            {
                timer = 0;
            }
        }

        guessedPhoneNumber = "";
        numbersEntered = 0;
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
            FadeUnFadeManager fadeManager = g.GetComponent<FadeUnFadeManager>();
            fadeManager.DisableInteractivity();
            fadeManager.Fade = true;
            fadeManager.UnFade = false;
        }
        miniGamePhone.SetActive(true);
        foreach (Image i in dialNumbers)
        {
            i.raycastTarget = true;
        }
    }
    private void OnExitChangeActive()
    {
        foreach (GameObject g in objectsToDisableOnStart)
        {
            FadeUnFadeManager fadeManager = g.GetComponent<FadeUnFadeManager>();
            fadeManager.EnableInteractivity();
            fadeManager.UnFade = true;
            fadeManager.Fade = false;
        }
        foreach (Image i in dialNumbers)
        {
            i.raycastTarget = false;
        }
    }
}
