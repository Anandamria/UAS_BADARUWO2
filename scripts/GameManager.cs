using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public BossSObj bossData;
    public int maxHearts = 3;
    public int currentHearts { get; private set; }
    public int potionCount { get; private set; }

    public bool isGameOver { get; private set; }
    public bool isGameWin { get; private set; }

    private GameObject gameOverPanel;
    private GameObject gameWinPanel;
    private void Start()
    {
        gameOverPanel = UIManager.Instance.gameOverPanel;
        gameWinPanel = UIManager.Instance.gameWinPanel;
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        bossData.InitializeData();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameOverPanel = UIManager.Instance.gameOverPanel;
        gameWinPanel = UIManager.Instance.gameWinPanel;

        StartCoroutine(DelayedReset());
         bossData.InitializeData();
    }

    private IEnumerator DelayedReset()
    {
        yield return new WaitForSecondsRealtime(0.1f); // beri waktu UIManager siap
        ResetGame();
        Time.timeScale = 1f;
    }

    public void ResetGame()
    {
        currentHearts = maxHearts;
        potionCount = 0;
        isGameOver = false;
        isGameWin = false;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHeartUI(currentHearts);
            UIManager.Instance.UpdatePotionUI(potionCount);
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (gameWinPanel != null)
            gameWinPanel.SetActive(false);

        AudioManager.instance.RestartMusic();
        bossData.InitializeData();
    }

    public void TakeDamage(int amount)
    {
        if (isGameOver || isGameWin) return;

        currentHearts -= amount;
        currentHearts = Mathf.Max(0, currentHearts);
        UIManager.Instance.UpdateHeartUI(currentHearts);

        if (currentHearts <= 0)
        {
            TriggerGameOver();
        }
    }

    public void AddPotion()
    {
        potionCount++;
        UIManager.Instance.UpdatePotionUI(potionCount);
    }

    public void UsePotion()
    {
        if (potionCount > 0)
        {
            potionCount--;
            UIManager.Instance.UpdatePotionUI(potionCount);
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOver || isGameWin) return;

        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            AudioManager.instance.PlayGameOverSFX(); 
            gameOverPanel.SetActive(true);
            AudioManager.instance.PauseMusic(true);
        }
        else
        {
            Debug.LogWarning("GameOverPanel is null!");
        }
    }

    public void TriggerGameWin()
    {
        if (isGameWin || isGameOver) return;

        isGameWin = true;
        Time.timeScale = 0f;

        if (gameWinPanel != null)
        {
            AudioManager.instance.PlayGameWinSFX(); 
            gameWinPanel.SetActive(true);
            AudioManager.instance.PauseMusic(true);
        }
        else
        {
            Debug.LogWarning("GameWinPanel is null!");
        }
    }
}
