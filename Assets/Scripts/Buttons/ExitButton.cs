using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private Animator storePageAnimator = null;

    private void Awake()
    {
        StoreEnterExit storeManager = FindObjectOfType<StoreEnterExit>();
        storeManager.StoreButtonsToManageRayCast.Add(GetComponent<Button>());
    }

    public void OnPress()
    {
        storePageAnimator.SetTrigger("SlideBack");
    }
}
