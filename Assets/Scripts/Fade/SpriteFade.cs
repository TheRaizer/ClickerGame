using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    [field: SerializeField] public bool Fade { get; set; } = false;
    [SerializeField] private int baseAlpha = 1;
    [SerializeField] private float fadeSpeed = 0.5f;

    private SpriteRenderer sprite;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Fade)
        {
            if (sprite.color.a > 0)
            {
                Color color = sprite.color;
                color.a -= fadeSpeed * Time.deltaTime;
                sprite.color = color;
            }
            else
            {
                Color color = sprite.color;
                color.a = baseAlpha;
                sprite.color = color;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Color color = sprite.color;
        color.a = baseAlpha;
        sprite.color = color;
        gameObject.SetActive(false);
    }
}
