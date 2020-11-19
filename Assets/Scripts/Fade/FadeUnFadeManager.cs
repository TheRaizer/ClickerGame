using UnityEngine;

public class FadeUnFadeManager : MonoBehaviour
{
    [SerializeField] private bool autoFade = false;
    [SerializeField] private float maxAlpha = 255;//alpha is seen in values between 0 and 1 instead of 0 to 255
    [SerializeField] private float minAlpha = 0;
    [SerializeField] private float unfadeSpeed = 0;
    [SerializeField] private float fadeSpeed = 0;

    protected Color baseColor;
    protected bool baseRaycastability = false;

    public bool UnFade { get; set; } = false;
    public bool Fade { get; set; } = false;

    protected virtual void Awake()
    {
        maxAlpha /= 255;//divide to average value between 0 and 1
        minAlpha /= 255;
    }

    public virtual void ResetColor()
    {
        UnFade = false;
        Fade = false;
    }

    public virtual void DisableInteractivity() { }
    public virtual void EnableInteractivity() { }

    protected Color ManageUnFade(Color colorToChange)
    {
        Color color = colorToChange;
        color.a += unfadeSpeed * Time.deltaTime;

        if (color.a >= maxAlpha)
        {
            UnFade = false;
            if (autoFade)
            {
                Fade = true;
            }
        }

        return color;
    }
    protected Color ManageFade(Color colorToChange)
    {
        Color color = colorToChange;
        color.a -= fadeSpeed * Time.deltaTime;

        if (color.a <= minAlpha)
        {
            Fade = false;
        }

        return color;
    }
}
