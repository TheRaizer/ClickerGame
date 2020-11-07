using UnityEngine;
using UnityEngine.EventSystems;

public class StoreButton : MonoBehaviour
{
    [SerializeField] private Animator storePageAnimator = null;

    public void OnPress()
    {
        storePageAnimator.SetTrigger("SlideInFront");
    }
}
