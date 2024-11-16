using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private Button nextButton;

    [Header("Scene")] 
    [SerializeField] private string scene;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }
    
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDestroy()
    {
        nextButton.onClick.RemoveListener(OnNextButtonClicked);
    }

    private void OnNextButtonClicked()
    {
        SceneManager.LoadScene(scene);
    }
}
