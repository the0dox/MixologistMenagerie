using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// rough manager for creating text
public class DialougeManager : MonoBehaviour
{
    private static DialougeManager s_instance;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _explosionPrefab;

    public void Awake()
    {
        s_instance = this;  
    }

    public static SpeechBubbleBehavior CreateText(string text, Vector2 position, bool confirmable)
    {
        SpeechBubbleBehavior newText = Instantiate(s_instance._prefab).GetComponent<SpeechBubbleBehavior>();
        newText.SetText(text, confirmable);
        newText.transform.position = position;
        return newText;
    }

    public static SpeechBubbleBehavior CreateText(string text, Vector2 position, float timer)
    {
        SpeechBubbleBehavior newText = Instantiate(s_instance._prefab).GetComponent<SpeechBubbleBehavior>();
        newText.SetText(text, timer);
        newText.transform.position = position;
        return newText;
    }

    public static void CreateExplosion(Vector2 position)
    {
        GameObject newFXs = Instantiate(s_instance._explosionPrefab);
        newFXs.transform.position = position;
    }
}
