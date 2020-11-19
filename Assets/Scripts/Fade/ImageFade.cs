using UnityEngine;
using UnityEngine.UI;

public class ImageFade : FadeUnFadeManager
{
    private Image image;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        baseColor = image.color;
        baseRaycastability = image.raycastTarget;
    }

    private void Update()
    {
        if (UnFade)
        {
            Color color = ManageUnFade(image.color);
            image.color = color;
        }
        if (Fade)
        {
            Color color = ManageFade(image.color);
            image.color = color;
        }
    }

    public override void ResetColor()
    {
        base.ResetColor();
        image.color = baseColor;
    }

    public override void DisableInteractivity()
    {
        base.DisableInteractivity();
        image.raycastTarget = false;
    }

    public override void EnableInteractivity()
    {
        base.EnableInteractivity();
        image.raycastTarget = baseRaycastability;
    }
}
