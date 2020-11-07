using UnityEngine;
using UnityEngine.EventSystems;

public class ClickChecker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ClickCountManager clickManager = null;
    [SerializeField] private SpawnIcons spawnIcons = null;
    [SerializeField] private float sizeToShrinkToo = 0f;
    [SerializeField] private float shrinkSpeed = 0;

    private Vector3 shrunkenSize;
    private Vector3 normalSize;

    private bool shrink = false;
    private bool stopChangingScale = false;

    private void Awake()
    {
        normalSize = transform.localScale;
        shrunkenSize = new Vector3(sizeToShrinkToo, sizeToShrinkToo, sizeToShrinkToo);
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
        clickManager.IncreaseClickCount(1);
        stopChangingScale = false;
        shrink = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        shrink = false;
    }
}
