using System;
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

    [field: SerializeField] public bool UnFade { get; set; } = false;
    [field: SerializeField] public bool Fade { get; set; } = false;

    public Action OnUnFaded { get; set; } = null;
    public Action OnFaded { get; set; } = null;

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
            OnUnFaded?.Invoke();
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
            OnFaded?.Invoke();
        }

        return color;
    }
}
