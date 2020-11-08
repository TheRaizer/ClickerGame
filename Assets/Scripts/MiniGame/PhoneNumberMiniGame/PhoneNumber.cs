using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneNumber : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int number = 0;
    private PhoneNumberMini phoneNumberMini;

    private void Awake()
    {
        phoneNumberMini = FindObjectOfType<PhoneNumberMini>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        phoneNumberMini.AddNumberToGuess(number);
    }
}
