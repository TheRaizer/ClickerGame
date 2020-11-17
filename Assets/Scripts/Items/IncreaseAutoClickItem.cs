using UnityEngine;
using TMPro;
using System;

public class IncreaseAutoClickItem : Item
{
    [SerializeField] private float autoClickIncrease = 0;//autoclick increase of 0.1 means it will increase once per second.
    [SerializeField] private TextMeshProUGUI clickIncreaseText = null;

    protected override void Awake()
    {
        base.Awake();
        UpdateClickIncreaseText();
    }

    private void UpdateClickIncreaseText() => clickIncreaseText.text = Math.Round(autoClickIncrease * 10, 2).ToString();

    public override void OnUse()
    {
        if (CanBuy)
        {
            base.OnUse();
            clickManager.AddToAutoClickAmt(autoClickIncrease);
        }
    }
}
