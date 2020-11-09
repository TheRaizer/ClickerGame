using UnityEngine;

public class PhoneNumber : MonoBehaviour
{
    ///<summary>
    /// The reason we must make a new phone number class for each number is because
    /// the button unity event does not use serialized fields and therefore a new script must be
    /// placed on each phonenumber gameObject with a different number hard coded in the OnPress func.
    ///</summary>
    
    protected PhoneNumberMini phoneNumberMini;

    private void Awake()
    {
        phoneNumberMini = FindObjectOfType<PhoneNumberMini>();
    }

    public virtual void OnPress() { }
}
