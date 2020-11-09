using UnityEngine;

public class PhoneNumber5 : PhoneNumber
{
    public override void OnPress()
    {
        phoneNumberMini.AddNumberToGuess(5);
    }
}
