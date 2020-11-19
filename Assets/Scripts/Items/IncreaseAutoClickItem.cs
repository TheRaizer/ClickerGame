using UnityEngine;
using System;
using UnityEngine.UI;

public class IncreaseAutoClickItem : Item
{
    [SerializeField] private float autoClickIncrease = 0;//autoclick increase of 0.1 means it will increase once per second.
    [SerializeField] private Text autoIncreaseText = null;

    protected override void Awake()
    {
        base.Awake();
        UpdateClickIncreaseText();
    }

    private void UpdateClickIncreaseText() => autoIncreaseText.text = string.Format("{0:n0}", Math.Round(autoClickIncrease * 10, 2).ToString());

    public override void OnUse()
    {
        if (CanBuy)
        {
            base.OnUse();
            clickManager.AddToAutoClickAmt(autoClickIncrease);
        }
    }
}
