using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false); 
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // pause game
        pausePanel.SetActive(true); // tampilkan popup
        AudioManager.instance.PauseMusic(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false); // sembunyikan popup
        AudioManager.instance.PauseMusic(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.sceneLoaded += GameManager.Instance.OnSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.instance.RestartMusic();
    }


    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }
    

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lavel 2");

    }
    
}
