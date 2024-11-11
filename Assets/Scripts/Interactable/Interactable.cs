using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private enum Type
    {
        Heal,
        Boost,
        Invulnerable,
        Coin,
    }
    
    [SerializeField] private Type type;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
