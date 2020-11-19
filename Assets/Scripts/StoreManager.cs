using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private Image phone = null;
    [SerializeField] private Button store = null;
    [SerializeField] private Button storeExit = null;

    public List<Item> Items { get; private set; } = new List<Item>();

    private void Start()
    {
        LoadStoreDataIntoVariables();
        CheckAllItemAvailability();
        UpdateAllItemCostTexts();
    }

    public void CheckAllItemAvailability()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].CheckForButtonInteractivity();
        }
    }

    private void UpdateAllItemCostTexts()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].UpdateCostText();
        }
    }

    public StoreItemData GenerateStoreItemData()
    {
        StoreItemData data = new StoreItemData()
        {
            costs = new int[Items.Count],
            restrictedUses = new bool[Items.Count]
        };

        for(int i = 0; i < data.costs.Length; i++)
        {
            data.costs[i] = Items[i].Cost;
            data.restrictedUses[i] = Items[i].RestrictUse;
        }

        return data;
    }

    private void LoadStoreDataIntoVariables()
    {
        StoreItemData data = SaveSystem.Instance.LoadStoreItemData();
        if (data != null)
        {
            for (int i = 0; i < data.costs.Length; i++)
            {
                Items[i].Cost = data.costs[i];
                Items[i].RestrictUse = data.restrictedUses[i];
            }
        }
    }

    //Animation Event Methods
    private void TurnOffPhoneRaycast()
    {
        phone.raycastTarget = false;
    }

    private void TurnOnPhoneRaycast()
    {
        phone.raycastTarget = true;
    }

    private void TurnOffStoreIconRaycast()
    {
        store.interactable = false;
    }

    private void TurnOnStoreIconRaycast()
    {
        store.interactable = true;
    }

    private void TurnOnStoreButtons()
    {
        for(int i = 0; i < Items.Count; i++)
        {
            Items[i].CheckForButtonInteractivity();
        }
        storeExit.interactable = true;
    }

    private void TurnOffStoreButtons()
    {
        storeExit.interactable = false;
        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].DisableButton();
        }
    }
}
