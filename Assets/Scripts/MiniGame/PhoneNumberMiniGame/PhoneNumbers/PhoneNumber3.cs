using UnityEngine;

public class PhoneNumber3 : PhoneNumber
{
    public override void OnPress()
    {
        phoneNumberMini.AddNumberToGuess(3);
    }
}
