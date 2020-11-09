using UnityEngine;

public class PhoneNumber1 : PhoneNumber
{
    public override void OnPress()
    {
        phoneNumberMini.AddNumberToGuess(1);
    }
}
