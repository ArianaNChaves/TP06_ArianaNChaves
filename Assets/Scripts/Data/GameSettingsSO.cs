using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Game Settings Data", menuName = "ScriptableObjects/Game Settings Data")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Player")]
    [SerializeField] private int maxJumps;

    [Header("Camera")]
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float yOffset = 1.0f;

    [Header("Enemy")] 
    [SerializeField] private Vector2 coinValueRange;

    public int MaxJumps
    {
        get => maxJumps;
        set => maxJumps = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float YOffset
    {
        get => yOffset;
        set => yOffset = value;
    }

    public Vector2 CoinValueRange
    {
        get => coinValueRange;
        set => coinValueRange = value;
    }
}
