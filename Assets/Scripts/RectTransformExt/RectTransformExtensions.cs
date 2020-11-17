using UnityEngine;

public static class RectTransformExtensions
{
    public static void ZeroOutZ(this RectTransform rect)
    {
        rect.anchoredPosition3D = new Vector3(rect.anchoredPosition.x, rect.anchoredPosition.y, 0);
    }
}
