using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameData.Instance.AddCoins(CoinsManager.Instance.GetMaxCoinsAmount());
        AudioManager.Instance.PlayEffect("Win");
        SceneManager.LoadScene("WinScreen");
    }
}
