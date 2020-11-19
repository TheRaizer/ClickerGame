using UnityEngine;

public class BobTransform : MonoBehaviour
{
    [SerializeField] private float amplitude = 0;
    [SerializeField] private float frq = 0;

    private void Update()
    {
        float y = Mathf.Sin(Time.time * frq) * amplitude;
        transform.position = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
    }
}
