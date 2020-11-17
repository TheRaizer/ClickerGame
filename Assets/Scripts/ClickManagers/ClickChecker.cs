using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ClickChecker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private ClickCountManager clickManager = null;
    [SerializeField] private SpawnPopups spawnIcons = null;
    [SerializeField] private float sizeToShrinkToo = 0f;
    [SerializeField] private float shrinkSpeed = 0;

    private Vector3 shrunkenSize;
    private Vector3 normalSize;

    private ObjectPooler objectPooler;
    private bool shrink = false;
    private bool stopChangingScale = false;

    private void Awake()
    {
        normalSize = transform.localScale;
        shrunkenSize = new Vector3(sizeToShrinkToo, sizeToShrinkToo, sizeToShrinkToo);
        objectPooler = FindObjectOfType<ObjectPooler>();
    }

    private void Update()
    {
        if (shrink)
        {
            ShrinkPhone();
        }
        else if(!stopChangingScale)
        {
            UnShrinkPhone();
        }
    }

    private void ShrinkPhone()
    {
        transform.localScale = new Vector3
            (
                transform.localScale.x - Time.deltaTime * shrinkSpeed,
                transform.localScale.y - Time.deltaTime * shrinkSpeed,
                transform.localScale.z - Time.deltaTime * shrinkSpeed
            );
        
        if (transform.localScale.x <= shrunkenSize.x && transform.localScale != shrunkenSize)
        {
            transform.localScale = shrunkenSize;
        }
    }

    private void UnShrinkPhone()
    {
        transform.localScale = new Vector3
            (
                transform.localScale.x + Time.deltaTime * shrinkSpeed,
                transform.localScale.y + Time.deltaTime * shrinkSpeed,
                transform.localScale.z + Time.deltaTime * shrinkSpeed
            );
        if(transform.localScale.x >= normalSize.x && transform.localScale != normalSize)
        {
            transform.localScale = normalSize;
            stopChangingScale = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        spawnIcons.Spawn();
        clickManager.IncreaseClickCount(clickManager.GetClickAmount());
        clickManager.ClickSliderManager.IncrementSlider();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos);

        GameObject g = objectPooler.SpawnUIObject(pos, Quaternion.identity, "ClickNumber");
        g.GetComponent<RectTransform>().ZeroOutZ();
        g.GetComponent<TextMeshProUGUI>().text = clickManager.GetClickAmount().ToString();

        stopChangingScale = false;
        shrink = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        shrink = false;
    }
}
