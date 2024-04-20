using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// rough manager for creating text
public class DialougeManager : MonoBehaviour
{
    private static DialougeManager s_instance;
    [SerializeField] private GameObject _prefab;

    public void Awake()
    {
        s_instance = this;  
    }

    public static SpeechBubbleBehavior CreateText(string text, Vector2 position)
    {
        SpeechBubbleBehavior newText = Instantiate(s_instance._prefab).GetComponent<SpeechBubbleBehavior>();
        newText.SetText(text);
        newText.transform.position = position;
        return newText;
    }
}
