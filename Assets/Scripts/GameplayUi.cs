using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUi : MonoBehaviour
{
    public static GameplayUi Instance;
    
    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI coinsText;
    
    [Header("Player Stats")]
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI jumpsText;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    
    
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateDamageText(int damage)
    {
        damageText.text = damage.ToString();
    }

    public void UpdateJumpsText(int jumps)
    {
        jumpsText.text = jumps.ToString();
    }

    public void UpdateMaxHealthText(int maxHealth)
    {
        maxHealthText.text = maxHealth.ToString();
    }

    public void UpdateCoinsText(int coins)
    {
        coinsText.text = coins.ToString();
    }
    
}
