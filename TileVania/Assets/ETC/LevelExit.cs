using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float delayToNextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadSceneWithDelay(delayToNextLevel));
    }

    IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        var nextLevelToLoad = (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1) ?
                                 0 : SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(nextLevelToLoad);
    }
}
