using UnityEngine;

public class IncreaseAutoClickItem : MonoBehaviour
{
    [SerializeField] private string id = "";
    [SerializeField] private int autoClickIncrease = 0;
    [SerializeField] private ClickCountManager clickManager = null;
    [SerializeField] private int cost = 0;

    public virtual void OnUse()
    {
        clickManager.DecreaseClickCount(cost);
        clickManager.AddToAutoClickAmt(autoClickIncrease);
    }
}
