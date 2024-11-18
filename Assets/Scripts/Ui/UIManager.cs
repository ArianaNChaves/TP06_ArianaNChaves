using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Buttons")] 
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button settingsBackButton;
    [SerializeField] private Button creditsBackButton;
    [SerializeField] private Button sfxButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button infoBackButton;

    [Header("Panels")] 
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject infoPanel;
    
    [Header("Sliders")] 
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;
    [SerializeField] private Slider globalVolume;
    
    [Header("Info")]
    [SerializeField] private TextMeshProUGUI totalCoins;
    [SerializeField] private TextMeshProUGUI totalTime;
    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        settingsBackButton.onClick.AddListener(OnSettingsBackButtonClicked);
        creditsBackButton.onClick.AddListener(OnCreditsBackButtonClicked);
        sfxButton.onClick.AddListener(OnSoundEffectsButtonClicked);
        infoButton.onClick.AddListener(OnInfoButtonClicked);
        
        musicVolume.onValueChanged.AddListener(SetMusicVolume);
        sfxVolume.onValueChanged.AddListener(SetSFXVolume);
        globalVolume.onValueChanged.AddListener(SetGlobalVolume);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Menu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        LoadInfoPanel();
        AudioManager.Instance.MusicVolume(musicVolume.value);
        AudioManager.Instance.SfxVolume(sfxVolume.value);
        AudioManager.Instance.GlobalVolume(globalVolume.value);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        creditsButton.onClick.RemoveListener(OnCreditsButtonClicked);
        exitButton.onClick.RemoveListener(OnExitButtonClicked);
        settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        settingsBackButton.onClick.RemoveListener(OnSettingsBackButtonClicked);
        creditsBackButton.onClick.RemoveListener(OnSoundEffectsButtonClicked);
        sfxButton.onClick.RemoveListener(OnSoundEffectsButtonClicked);
        infoButton.onClick.RemoveListener(OnInfoButtonClicked);
        
        musicVolume.onValueChanged.RemoveListener(SetMusicVolume);
        sfxVolume.onValueChanged.RemoveListener(SetSFXVolume);
        globalVolume.onValueChanged.RemoveListener(SetSFXVolume);
    }

    private void LoadInfoPanel()
    {
        totalCoins.SetText(GameData.Instance.GetCoins().ToString());
        totalTime.SetText(Mathf.Round(GameData.Instance.GetTime()).ToString() + " S");
    }
    private void OnInfoButtonClicked()
    {
        infoPanel.SetActive(!infoPanel.activeInHierarchy);
        buttonsPanel.SetActive(!buttonsPanel.activeInHierarchy);
    }
    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Gameplay");
    }
    private void OnCreditsButtonClicked()
    {
        creditsPanel.SetActive(!creditsPanel.activeInHierarchy);
        buttonsPanel.SetActive(false);
    }
    private void OnExitButtonClicked()
    {
        Debug.Log("exit");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    private void OnSettingsButtonClicked()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
        buttonsPanel.SetActive(false);
    }
    private void OnSettingsBackButtonClicked()
    {
        buttonsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    private void OnCreditsBackButtonClicked()
    {
        buttonsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    private void OnSoundEffectsButtonClicked()
    {
        AudioManager.Instance.PlayEffect("Enemy Die");
    }
    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.MusicVolume(value);
    }
    public void SetSFXVolume(float value)
    {
        AudioManager.Instance.SfxVolume(value);
    }
    public void SetGlobalVolume(float value)
    {
        AudioManager.Instance.GlobalVolume(value);
    }

    public void OnMouseHover()
    {
        AudioManager.Instance.PlayEffect("Ui");
    }
}
