using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    public static GameSession instance { get; private set; }

    [SerializeField] int playerLives = 3;
    int coinsCount = 0;

    [SerializeField] TextMeshProUGUI coinsCountText;
    [SerializeField] TextMeshProUGUI livesCountText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        UpdateCoinsUI();
        UpdateLivesUI();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddCoin()
    {
        coinsCount++;
        UpdateCoinsUI();
    }

    private void TakeLife()
    {
        playerLives--;
        UpdateLivesUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void UpdateCoinsUI()
    {
        coinsCountText.text = "x " + coinsCount.ToString();
    }

    void UpdateLivesUI()
    {
        livesCountText.text = "x " + playerLives.ToString();
    }
}
