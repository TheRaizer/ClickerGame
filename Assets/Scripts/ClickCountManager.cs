using TMPro;
using UnityEngine;

public class ClickCountManager : MonoBehaviour
{
    [field: SerializeField] public long ClickCount { get; private set; }
    [SerializeField] private float timeBetweenAutoClick = 0.5f;
    [SerializeField] private TextMeshProUGUI countText = null;

    private int autoClickAmt = 0;
    private float timer = 0;

    private void Update()
    {
        if (autoClickAmt > 0)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAutoClick)
            {
                timer = 0;
                ClickCount += autoClickAmt;
                countText.text = ClickCount.ToString();
            }
        }

    }

    public void IncreaseClickCount(int amt)
    {
        ClickCount += amt;
        countText.text = ClickCount.ToString();
    }

    public void DecreaseClickCount(int amt)
    {
        ClickCount -= amt;
        countText.text = ClickCount.ToString();
    }

    public void AddToAutoClickAmt(int amt)
    {
        autoClickAmt += amt;
    }
}
