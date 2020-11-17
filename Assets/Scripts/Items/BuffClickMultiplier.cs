using UnityEngine;

public class BuffClickMultiplier : Item
{
    [SerializeField] private float multiplierBuff = 0;
    [SerializeField] private float timeToLast = 0;

    public override void OnUse()
    {
        if (CanBuy)
        {
            base.OnUse();
            clickManager.IncreaseBuffClickMultiplierTimed(multiplierBuff, timeToLast);
        }
    }
}