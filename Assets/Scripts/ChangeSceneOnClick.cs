using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeSceneOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int sceneToChangeToo = 1;
    private FadeUnFadeManager unFadeManager;

    private void Awake()
    {
        unFadeManager = GetComponent<FadeUnFadeManager>();
        unFadeManager.OnUnFaded += LoadScene;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        unFadeManager.UnFade = true;
    }

    private void LoadScene() => SceneManager.LoadScene(sceneToChangeToo);
}
