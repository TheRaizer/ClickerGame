using UnityEngine;
using UnityEngine.EventSystems;

public class StoreIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator storePageAnimator = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        storePageAnimator.SetTrigger("SlideInFront");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            storePageAnimator.SetTrigger("SlideBack");
        }
    }
}
