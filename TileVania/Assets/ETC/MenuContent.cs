using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class MenuContent : MonoBehaviour
{
    [SerializeField]UnityEvent OnPlayerCollide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPlayerCollide.Invoke();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToogleOptionsPanel()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
