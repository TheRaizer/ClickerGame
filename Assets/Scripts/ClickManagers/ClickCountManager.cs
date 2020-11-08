using System;
using TMPro;
using UnityEngine;

public class ClickCountManager : MonoBehaviour
{
    [SerializeField] private float numberOfManualClicks = 0;
    [field: SerializeField] public long ClickCount { get; private set; }
    [SerializeField] private TextMeshProUGUI countText = null;
    [SerializeField] private TextMeshProUGUI autoClicksPerSecond = null;

    private readonly float clickMultiplier = 1;

    private float autoClickAmt = 0;
    private float timer = 0;
    private float buffedClickMultiplier = 0;
    private float lengthToBuffMultiplier = 0;

    private float autoClickAmtDecimal = 0.0f;
    private float clickCountDecimal = 0.0f;

    private const float AUTO_CLICK_INTERVAL = 0.1f;

    private void Update()
    {
        AutoClick();

        if (lengthToBuffMultiplier > 0)
        {
            lengthToBuffMultiplier -= Time.deltaTime;
        }
        else if(lengthToBuffMultiplier <= 0 && clickMultiplier != 1)
        {
            buffedClickMultiplier = 0;
        }

    }

    private void UpdateAutoClicksPerSecondText()
    {
        //the value of (autoClickAmt + autoClickAmtDecimal) * 10 is the number of followers gained per second.
        autoClicksPerSecond.text = "per second: " + Math.Round((autoClickAmt + autoClickAmtDecimal) * 10, 1);
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

    public void AddToAutoClickAmt(float amt)
    {
        autoClickAmt += amt;
    }

    private void UpdateCountText()
    {
        countText.text = "Followers: " + ClickCount;
    }

    public int GetClickAmount()
    {
        return (int)(clickMultiplier + buffedClickMultiplier);
    }

    public void SetBuffClickMultiplier(float amt, float time)
    {
        buffedClickMultiplier += amt;
        lengthToBuffMultiplier = time;
    }
}
