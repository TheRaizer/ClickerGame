using TMPro;
using UnityEngine;

public class TextRiseFade : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 0;
    [SerializeField] private float fadeSpeed = 0;

    private RectTransform rect;
    private TextMeshProUGUI text;
    private float baseAlpha = 255f;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
        baseAlpha = text.color.a;
    }

    private void Update()
    {
        rect.Translate(Vector3.up * riseSpeed * Time.deltaTime, Space.Self);
        Color color = text.color;
        color.a -= fadeSpeed * Time.deltaTime;
        text.color = color;

        if(text.color.a <= 0)
        {
            color.a = baseAlpha;
            text.color = color;
            gameObject.SetActive(false);
        }
    }
}
