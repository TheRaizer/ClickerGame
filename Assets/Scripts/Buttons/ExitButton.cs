using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private Animator storePageAnimator = null;

    public void OnPress()
    {
        storePageAnimator.SetTrigger("SlideBack");
    }
}
