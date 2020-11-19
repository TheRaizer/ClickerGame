using UnityEngine;
using UnityEngine.UI;

public class ClickSliderManager
{
    private readonly Slider clickSlider;
    private readonly ClickCountManager clickCountManager;
    private float timer = 0f;
    private const float DRAIN_INTERVAL = 1f;
    private const float MAXED_DRAIN_INTERVAL = 0.01f;
    private const float CLICK_MULTIPLIER_SCALE = 5f;

    public bool Maxed { get; set; }

    public ClickSliderManager(Slider _clickSlider, ClickCountManager _clickCountManager)
    {
        clickSlider = _clickSlider;
        clickCountManager = _clickCountManager;
    }

    public void OnUpdate()
    {
        DrainSlider();
    }

    public void IncrementSlider()
    {
        clickSlider.value += 1;
        if(clickSlider.value >= clickSlider.maxValue && !Maxed)
        {
            OnMaxValue();
        }
    }

    private void DecrementSlider()
    {
        clickSlider.value -= 1;
        if(clickSlider.value < 0)
        {
            clickSlider.value = 0;
        }
    }

    private void DrainSlider()
    {
        if (!Maxed)
        {
            timer += Time.deltaTime;
            if (timer >= DRAIN_INTERVAL)
            {
                timer = 0f;
                DecrementSlider();
            }
        }
        else
        {
            if(clickSlider.value <= 0)
            {
                Maxed = false;
                timer = 0;
                clickCountManager.DecreaseBuffMultiplier(CLICK_MULTIPLIER_SCALE);
                return;
            }
            timer += Time.deltaTime;
            if(timer >= MAXED_DRAIN_INTERVAL)
            {
                timer = 0f;
                DecrementSlider();
            }
        }
    }

    private void OnMaxValue()
    {
        Maxed = true;
        clickCountManager.IncreaseBuffClickMultiplier(CLICK_MULTIPLIER_SCALE);
    }
}
