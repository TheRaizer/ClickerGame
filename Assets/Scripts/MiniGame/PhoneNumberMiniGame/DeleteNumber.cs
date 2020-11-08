using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteNumber : MonoBehaviour, IPointerClickHandler
{
    private PhoneNumberMini phoneNumberMini;

    private void Awake()
    {
        phoneNumberMini = FindObjectOfType<PhoneNumberMini>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        phoneNumberMini.DeleteNumber();
    }
}
