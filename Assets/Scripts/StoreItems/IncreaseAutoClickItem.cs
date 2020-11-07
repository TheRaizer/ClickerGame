using UnityEngine;

public class IncreaseAutoClickItem : Item
{
    [SerializeField] private float autoClickIncrease = 0;

    public override void OnUse()
    {
        if (CanBuy)
        {
            base.OnUse();
            clickManager.AddToAutoClickAmt(autoClickIncrease);
        }
    }
}
