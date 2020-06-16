using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int startSceneIndex;

    private void Awake()
    {
        if (FindObjectsOfType<ScenePersist>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }

    private void Start()
    {
        startSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    void OnSceneChange(Scene currentScene, Scene newScene)
    {
        if (startSceneIndex != newScene.buildIndex)
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
            Destroy(this.gameObject);
        }
    }
}
