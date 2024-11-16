using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Panels")] 
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject buttonsPanel;
    
    [Header("Sliders")] 
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;
    [SerializeField] private Slider globalVolume;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        settingsBackButton.onClick.AddListener(OnSettingsBackButtonClicked);
        creditsBackButton.onClick.AddListener(OnCreditsBackButtonClicked);
        sfxButton.onClick.AddListener(OnSoundEffectsButtonClicked);

        
        musicVolume.onValueChanged.AddListener(SetMusicVolume);
        sfxVolume.onValueChanged.AddListener(SetSFXVolume);
        globalVolume.onValueChanged.AddListener(SetGlobalVolume);
    }

    private void Start()
    {
        //AudioManager.Instance.PlayMusic("Main Theme");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        //AudioManager.Instance.MusicVolume(musicVolume.value);
        //AudioManager.Instance.SfxVolume(sfxVolume.value);
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

        
        musicVolume.onValueChanged.RemoveListener(SetMusicVolume);
        sfxVolume.onValueChanged.RemoveListener(SetSFXVolume);
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
        //AudioManager.Instance.PlayEffect("Gunshot");
    }
    public void SetMusicVolume(float value)
    {
        //AudioManager.Instance.MusicVolume(value);
    }
    public void SetSFXVolume(float value)
    {
        //AudioManager.Instance.SfxVolume(value);
    }
    public void SetGlobalVolume(float value)
    {
        //AudioManager.Instance.SfxVolume(value);
    }
}
