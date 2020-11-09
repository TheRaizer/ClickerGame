using UnityEngine;

public class PhoneNumber2 : PhoneNumber
{
    public override void OnPress()
    {
        phoneNumberMini.AddNumberToGuess(2);
    }
}
