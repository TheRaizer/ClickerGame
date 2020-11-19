using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCountManager : MonoBehaviour
{
    [SerializeField] private int numberOfManualClicks = 0;
    [field: SerializeField] public long ClickCount { get; private set; }
    [SerializeField] private Text countText = null;
    [SerializeField] private Text autoClicksPerSecond = null;
    [SerializeField] private Text clickMultiplierText = null;
    [SerializeField] private Slider clickSlider = null;

    public ClickSliderManager ClickSliderManager { get; private set; }
    private readonly List<ClickBoostTimer> boostTimers = new List<ClickBoostTimer>();

    private float baseClickMultiplier = 1;

    private float autoClickAmt = 0;
    private float timer = 0;
    private float buffedClickMultiplier = 1;

    private float autoClickAmtDecimal = 0.0f;
    private float clickCountDecimal = 0.0f;

    private const float AUTO_CLICK_INTERVAL = 0.1f;

    private void Awake()
    {
        ClickSliderManager = new ClickSliderManager(clickSlider, this);
    }

    private void Start()
    {
        LoadClickerDataIntoVariables();
    }

    private void Update()
    {
        AutoClick();
        CountDownBoosts();
        ClickSliderManager.OnUpdate();
    }

    private void CountDownBoosts()
    {
        for(int i = 0; i < boostTimers.Count; i++)
        {
            if (boostTimers[i].Finished)
            {
                boostTimers.Remove(boostTimers[i]);
                continue;
            }

            boostTimers[i].OnUpdate();
        }
    }


    private void AutoClick()
    {
        if (autoClickAmt > 0 || autoClickAmtDecimal > 0)
        {
            timer += Time.deltaTime;
            if (timer >= AUTO_CLICK_INTERVAL)
            {
                timer = 0;
                ClickCount += Mathf.FloorToInt(autoClickAmt);
                clickCountDecimal += (float)(autoClickAmt - Math.Truncate(autoClickAmt));
                if (clickCountDecimal >= 1)
                {
                    ClickCount += Mathf.FloorToInt(clickCountDecimal);
                    clickCountDecimal = (float)(autoClickAmtDecimal - Math.Truncate(autoClickAmtDecimal));
                }
                UpdateAutoClicksPerSecondText();
                UpdateCountText();
            }
        }
    }

    public void IncreaseClickCount(int amt)
    {
        ClickCount += amt;
        UpdateCountText();
    }

    public void DecreaseClickCount(int amt)
    {
        ClickCount -= amt;
        UpdateCountText();
    }
    public int GetClickAmount()
    {
        return (int)(baseClickMultiplier * buffedClickMultiplier);
    }

    public void IncrementManualClicks() => numberOfManualClicks++;
    public void IncreaseBuffClickMultiplierTimed(float amt, float time)
    {
        buffedClickMultiplier *= amt;
        boostTimers.Add(new ClickBoostTimer(time, amt, DecreaseBuffMultiplier));
        UpdateClickMultiplierText();
    }
    public void IncreaseBuffClickMultiplier(float amt)
    {
        buffedClickMultiplier *= amt;
        UpdateClickMultiplierText();
    }
    public void IncreaseBaseClickMultiplier(float amt)
    {
        baseClickMultiplier *= amt;
        UpdateClickMultiplierText();
    }
    public void DecreaseBuffMultiplier(float amt)
    {
        buffedClickMultiplier /= amt;
        if (buffedClickMultiplier < 0)
        {
            buffedClickMultiplier = 0;
            throw new ArgumentOutOfRangeException();
        }
        UpdateClickMultiplierText();
    }
    public ClickerData GenerateClickerData()
    {
        ClickerData data = new ClickerData
        {
            autoClickAmt = autoClickAmt,
            autoClickAmtDecimal = autoClickAmtDecimal,
            baseClickMultiplier = baseClickMultiplier,
            buffedClickMultiplier = buffedClickMultiplier,
            clickCountDecimal = clickCountDecimal,
            clickSliderValue = clickSlider.value,
            timer = timer,
            numberOfManualClicks = numberOfManualClicks,
            timerCounts = new float[boostTimers.Count],
            timerAmounts = new float[boostTimers.Count],
            clickCount = ClickCount,
            sliderMaxed = ClickSliderManager.Maxed
        };

        for (int i = 0; i < boostTimers.Count; i++)
        {
            data.timerCounts[i] = boostTimers[i].Timer;
            data.timerAmounts[i] = boostTimers[i].amt;
        }
        return data;
    }

    private void LoadClickerDataIntoVariables()
    {
        ClickerData data = SaveSystem.Instance.LoadClickerData();
        if (data != null)
        {
            autoClickAmt = data.autoClickAmt;
            autoClickAmtDecimal = data.autoClickAmtDecimal;
            baseClickMultiplier = data.baseClickMultiplier;
            buffedClickMultiplier = data.buffedClickMultiplier;
            clickCountDecimal = data.clickCountDecimal;
            clickSlider.value = data.clickSliderValue;
            timer = data.timer;
            numberOfManualClicks = data.numberOfManualClicks;
            ClickCount = data.clickCount;
            ClickSliderManager.Maxed = data.sliderMaxed;

            for (int i = 0; i < data.timerCounts.Length; i++)
            {
                boostTimers.Add(new ClickBoostTimer(data.timerCounts[i], data.timerAmounts[i], DecreaseBuffMultiplier));
            }
            UpdateCountText();
            UpdateClickMultiplierText();
            UpdateAutoClicksPerSecondText();
        }
    }
    public void AddToAutoClickAmt(float amt) => autoClickAmt += amt;
    private void UpdateCountText() => countText.text = "Followers: " + string.Format("{0:n0}", ClickCount);
    private void UpdateClickMultiplierText() => clickMultiplierText.text = string.Format("{0:n0}", GetClickAmount()) + "x";

    //the value of (autoClickAmt + autoClickAmtDecimal) * 10 is the number of followers gained per second.
    private void UpdateAutoClicksPerSecondText()
    {
        autoClicksPerSecond.text = "per second: " + string.Format("{0:n0}", Math.Round((autoClickAmt + autoClickAmtDecimal) * 10, 1).ToString());
    }
}
