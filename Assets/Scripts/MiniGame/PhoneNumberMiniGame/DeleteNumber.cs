using UnityEngine;

public class DeleteNumber : MonoBehaviour
{
    private PhoneNumberMini phoneNumberMini;

    private void Awake()
    {
        phoneNumberMini = FindObjectOfType<PhoneNumberMini>();
    }

    public void OnPress()
    {
        phoneNumberMini.DeleteNumber();
    }
}
