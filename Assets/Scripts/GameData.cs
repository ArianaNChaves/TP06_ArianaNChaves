using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    
    private float _timePlayed;
    private int _totalCoins;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    private void Update()
    {
        _timePlayed += Time.deltaTime;
    }

    private void OnApplicationQuit()
    {
        SaveData(); // Guarda los datos cuando el juego se cierra.
    }

    private void LoadData()
    {
        // Cargar el tiempo jugado y las monedas totales desde PlayerPrefs.
        _timePlayed = PlayerPrefs.GetFloat("TimePlayed", 0f); // Por defecto 0 si no hay dato guardado.
        _totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);    // Por defecto 0 si no hay dato guardado.

        Debug.Log($"Datos cargados: Tiempo jugado = {_timePlayed} segundos, Monedas totales = {_totalCoins}");
    }

    private void SaveData()
    {
        // Guardar el tiempo jugado y las monedas totales en PlayerPrefs.
        PlayerPrefs.SetFloat("TimePlayed", _timePlayed);
        PlayerPrefs.SetInt("TotalCoins", _totalCoins);
        PlayerPrefs.Save(); // Forzar el guardado inmediato.

        Debug.Log($"Datos guardados: Tiempo jugado = {_timePlayed} segundos, Monedas totales = {_totalCoins}");
    }

    public float GetTime()
    {
        return _timePlayed;
    }
    public float GetCoins()
    {
        return _totalCoins;
    }
    public void AddCoins(int amount)
    {
        _totalCoins += amount;
    }
}