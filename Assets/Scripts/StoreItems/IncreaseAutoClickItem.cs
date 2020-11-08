using UnityEngine;

public class IncreaseAutoClickItem : Item
{
    [SerializeField] private float autoClickIncrease = 0;//autoclick increase of 0.1 means it will increase once per second.

    public override void OnUse()
    {
        if (CanBuy)
        {
            base.OnUse();
            clickManager.AddToAutoClickAmt(autoClickIncrease);
        }
    }
}
