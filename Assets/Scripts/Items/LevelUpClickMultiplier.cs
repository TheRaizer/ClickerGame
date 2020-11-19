using UnityEngine;

public class LevelUpClickMultiplier : Item
{
    [SerializeField] private float multiplierIncrease = 0;

    public override void OnUse()
    {
        if (CanBuy)
        {
            base.OnUse();
            RestrictUse = true;
            clickManager.IncreaseBaseClickMultiplier(multiplierIncrease);
            UpdateCostText();
        }
    }
}