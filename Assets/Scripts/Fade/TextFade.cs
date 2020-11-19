using UnityEngine;
using UnityEngine.UI;

public class TextFade : FadeUnFadeManager
{
    private Text text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponent<Text>();
        baseColor = text.color;
        baseRaycastability = text.raycastTarget;
    }

    private void Update()
    {
        if (UnFade)
        {
            Color color = ManageUnFade(text.color);
            text.color = color;
        }
        if (Fade)
        {
            Color color = ManageFade(text.color);
            text.color = color;
        }
    }

    public override void ResetColor()
    {
        base.ResetColor();
        text.color = baseColor;
    }
    public override void EnableInteractivity()
    {
        base.EnableInteractivity();
        text.raycastTarget = baseRaycastability;
    }
}
