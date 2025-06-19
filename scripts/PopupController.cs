using UnityEngine;
using UnityEngine.SceneManagement;


public class PopupController : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject exitPopup;
    public GameObject shopPopup;
    public GameObject aboutPanel;

    public void GoToInGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void OpenSetting() => settingPanel.SetActive(true);
    public void CloseSetting() => settingPanel.SetActive(false);

    public void ShowExitPopup() => exitPopup.SetActive(true);
    public void HideExitPopup() => exitPopup.SetActive(false);

    public void OpenShop() => shopPopup.SetActive(true);
    public void CloseShop() => shopPopup.SetActive(false);

    public void ExitGame()
    {
        Debug.Log("Keluar dari game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void OpenAbout()
    {
        aboutPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        aboutPanel.SetActive(false);
    }

}
