using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreEnterExit : MonoBehaviour
{
    [SerializeField] private Image phone = null;
    [SerializeField] private Image store = null;
    

    //Animation Event Methods
    private void ReversePhoneRaycastable()
    {
        phone.raycastTarget = !phone.raycastTarget;
    }
    
    private void ReverseStoreRaycastable()
    {
        store.raycastTarget = !store.raycastTarget;
    }

}
