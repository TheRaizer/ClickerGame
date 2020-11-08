using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] protected int numberBought = 0;
    [SerializeField] protected int cost = 0;
    [SerializeField] protected float multiplier = 2.5f;

    [SerializeField] protected TextMeshProUGUI costText = null;
    [SerializeField] protected TextMeshProUGUI amountBoughtText = null;

    protected ClickCountManager clickManager = null;
    protected bool CanBuy => clickManager.ClickCount >= cost;

    private void Awake()
    {
        StoreEnterExit storeManager = FindObjectOfType<StoreEnterExit>();
        clickManager = FindObjectOfType<ClickCountManager>();

        storeManager.StoreButtonsToManageRayCast.Add(GetComponent<Button>());
        UpdateCostText();
        UpdateBoughtText();
    }

    protected void UpdateBoughtText()
    {
        amountBoughtText.text = "Bought: " + numberBought;
    }

    protected void UpdateCostText()
    {
        costText.text = "Cost: " + cost;
    }


    public virtual void OnUse()
    {
        clickManager.DecreaseClickCount(cost);
        cost = (int)(cost * multiplier);
        numberBought++;
        UpdateBoughtText();
        UpdateCostText();
    }
}
