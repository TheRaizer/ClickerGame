using UnityEngine;

public class DespawnOnTimer : MonoBehaviour
{
    [SerializeField] private float timeToLiveToo = 0;
    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToLiveToo)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }
}
