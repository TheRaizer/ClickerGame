using UnityEngine;

public class EnterPhoneNumber : MonoBehaviour
{
    private PhoneNumberMini phoneNumberMini;

    private void Awake()
    {
        phoneNumberMini = FindObjectOfType<PhoneNumberMini>();
    }

    public void OnPress()
    {
        phoneNumberMini.CheckIfCorrect();
    }
}
