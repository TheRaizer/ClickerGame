using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [field: SerializeField] public int Cost { get; set; } = 0;
    [SerializeField] protected float multiplier = 2.5f;

    [SerializeField] protected Text costText = null;

    private Button Button;
    protected ClickCountManager clickManager = null;
    protected bool CanBuy => clickManager.ClickCount >= Cost;
    public bool RestrictUse { get; set; } = false;
    protected StoreManager storeManager;

    protected virtual void Awake()
    {
        Button = GetComponent<Button>();
        clickManager = FindObjectOfType<ClickCountManager>();
        UpdateCostText();
        storeManager = FindObjectOfType<StoreManager>();
        storeManager.Items.Add(this);
    }

    protected virtual void Start()
    {
        if (!CanBuy)
        {
            Button.interactable = false;
        }
    }

    public void CheckForButtonInteractivity()
    {
        if (RestrictUse)
        {
            Button.interactable = false;
        }
        else if (CanBuy)
        {
            Button.interactable = true;
        }
        else
        {
            Button.interactable = false;
        }
    }

    public void DisableButton() => Button.interactable = false;

    public void UpdateCostText()
    {
        if (RestrictUse)
        {
            costText.text = "Bought";
        }
        else
            costText.text = "Cost: " + string.Format("{0:n0}", Cost);
    }

    public virtual void OnUse()
    {
        Button.interactable = true;
        clickManager.DecreaseClickCount(Cost);
        Cost = (int)(Cost * multiplier);
        UpdateCostText();
        storeManager.CheckAllItemAvailability();
    }
}
