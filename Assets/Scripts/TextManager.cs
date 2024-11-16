using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private Coroutine currentRoutine;

    private void Start()
    {
        Interactable.ItemCollected += OnItemCollected;
    }

    private void OnItemCollected(string textToWrite)
    {
        if (text == null) return;
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(EnableAndDisableRoutine(text, textToWrite));
    }

    private IEnumerator EnableAndDisableRoutine(TextMeshProUGUI target, string textToWrite)
    {
        target.SetText(textToWrite);
        yield return new WaitForSeconds(5);
        target.SetText("");
        currentRoutine = null;
    }
}
