using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioClip menuMusic;
    public AudioClip inGameMusic;
    public AudioSource sfxSource;
    public AudioClip gameOverSFX;
    public AudioClip gameWinSFX;


    public bool isMusicMuted = false;
    public bool isSFXMuted = false;

    public Image musicButtonImage;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    public Image sfxButtonImage;
    public Sprite sfxOnSprite;
    public Sprite sfxOffSprite;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource.mute = isMusicMuted;
        sfxSource.mute = isSFXMuted;

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Start()
    {
        UpdateMusicIcon();
        UpdateSFXIcon();

        // Atur musik saat pertama kali mulai, misal di scene awal
        PlayMusicForScene(SceneManager.GetActiveScene().name);

         StartCoroutine(DelayedUIBinding());

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
        if (scene.name == "MainMenu")
        {
            StartCoroutine(DelayedUIBinding());
        }

    }

    private void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            if (musicSource.clip != menuMusic)
            {
                musicSource.clip = menuMusic;
                musicSource.loop = true;
                musicSource.volume = 0.2f; 
                musicSource.Play();
            }
        }
        else if (sceneName == "InGame")
        {
            if (musicSource.clip != inGameMusic)
            {
                musicSource.clip = inGameMusic;
                musicSource.loop = true;
                musicSource.volume = 0.2f; 
                musicSource.Play();
            }
        }
        else
        {
            musicSource.Stop();
        }
    }


    public void ToggleMusic()
    {
        isMusicMuted = !isMusicMuted;
        musicSource.mute = isMusicMuted;
        UpdateMusicIcon();
    }

    public void ToggleSFX()
    {
        isSFXMuted = !isSFXMuted;
        sfxSource.mute = isSFXMuted;
        UpdateSFXIcon();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!isSFXMuted && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    private void UpdateMusicIcon()
    {
        if (musicButtonImage != null)
            musicButtonImage.sprite = isMusicMuted ? musicOffSprite : musicOnSprite;
    }

    private void UpdateSFXIcon()
    {
        if (sfxButtonImage != null)
            sfxButtonImage.sprite = isSFXMuted ? sfxOffSprite : sfxOnSprite;
    }

    public void PauseMusic(bool pause)
    {
        if (pause)
            musicSource.Pause();
        else
            musicSource.UnPause();
    }

    public void RestartMusic()
    {
        musicSource.Stop();         
        musicSource.time = 0f;      
        musicSource.Play();         
    }


   private IEnumerator DelayedUIBinding()
    {
        yield return new WaitForSeconds(0.2f); // jeda ui aktif

        while (GameObject.FindWithTag("MusicButton") == null)
             {
                  yield return null;
             }        
        GameObject musicBtn = GameObject.FindWithTag("MusicButton");

        while (GameObject.FindWithTag("SFXButton") == null)
              {
                     yield return null;
              }
        GameObject sfxBtn = GameObject.FindWithTag("SFXButton");

         Debug.Log(musicBtn + "M1");
         Debug.Log(sfxBtn + "S1");

        if (musicBtn != null)
        {
            musicButtonImage = musicBtn.GetComponent<Image>();

            // Re-bind tombol klik-nya ke fungsi ToggleMusic
            Button musicBtnComp = musicBtn.GetComponent<Button>();
            if (musicBtnComp != null)
            {
                musicBtnComp.onClick.RemoveAllListeners(); 
                musicBtnComp.onClick.AddListener(ToggleMusic);
            }
        }

        if (sfxBtn != null)
        {
            sfxButtonImage = sfxBtn.GetComponent<Image>();

            Button sfxBtnComp = sfxBtn.GetComponent<Button>();
            if (sfxBtnComp != null)
            {
                sfxBtnComp.onClick.RemoveAllListeners();
                sfxBtnComp.onClick.AddListener(ToggleSFX);
            }
        }

        UpdateMusicIcon();
        UpdateSFXIcon();
    }

    public void PlayGameOverSFX()
    {
        PlaySFX(gameOverSFX);
    }

    public void PlayGameWinSFX()
    {
        PlaySFX(gameWinSFX);
    }

}
