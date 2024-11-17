using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDamage : MonoBehaviour
{
    private const int KillDamage = -99999;
    
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     IHealthHandler healthHandler = other.gameObject.GetComponent<IHealthHandler>();
    //     healthHandler.UpdateHealth(KillDamage);
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        AudioManager.Instance.PlayEffect("Lose");
        GameData.Instance.AddCoins(CoinsManager.Instance.GetMaxCoinsAmount());
        SceneManager.LoadScene("LoseScreen");
    }
}
