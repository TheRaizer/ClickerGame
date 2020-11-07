using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreEnterExit : MonoBehaviour
{
    [SerializeField] private Image phone = null;
    [SerializeField] private Image store = null;

    [field: SerializeField] public List<Button> StoreButtonsToManageRayCast { get; set; } = new List<Button>();
    

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
        store.raycastTarget = false;
    }

    private void TurnOnStoreIconRaycast()
    {
        store.raycastTarget = true;
    }

    private void TurnOnStoreButtons()
    {
        for (int i = 0; i < StoreButtonsToManageRayCast.Count; i++)
        {
            StoreButtonsToManageRayCast[i].interactable = true;
        }
    }

    private void TurnOffStoreButtons()
    {
        for (int i = 0; i < StoreButtonsToManageRayCast.Count; i++)
        {
            StoreButtonsToManageRayCast[i].interactable = false;
        }
    }
}
