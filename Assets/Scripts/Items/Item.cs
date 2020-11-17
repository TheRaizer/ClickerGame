using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int cost = 0;
    [SerializeField] protected float multiplier = 2.5f;

    [SerializeField] protected TextMeshProUGUI costText = null;

    private Button Button;
    protected ClickCountManager clickManager = null;
    protected bool CanBuy => clickManager.ClickCount >= cost;
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
        if (CanBuy)
        {
            Button.interactable = true;
        }
        else
        {
            Button.interactable = false;
        }
    }

    public void DisableButton() => Button.interactable = false;

    public void UpdateCostText() => costText.text = "Cost: " + cost;

    public virtual void OnUse()
    {
        Button.interactable = true;
        clickManager.DecreaseClickCount(cost);
        cost = (int)(cost * multiplier);
        UpdateCostText();
        storeManager.CheckAllItemAvailability();
    }
}
