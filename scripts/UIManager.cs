using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI heartText;
    public TextMeshProUGUI potionText;

    public GameObject gameOverPanel;
    public GameObject gameWinPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdateHeartUI(int value)
    {
        heartText.text = value.ToString();
    }

    public void UpdatePotionUI(int value)
    {
        potionText.text = value.ToString();
    }
    
    
}
