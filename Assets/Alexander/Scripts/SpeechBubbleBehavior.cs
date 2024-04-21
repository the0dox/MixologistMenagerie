using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// rough placeholder speech bubble
public class SpeechBubbleBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;
    [SerializeField] private GameObject _confirmablePiece;

    public void SetText(string newMessage, float time)
    {
        _textComponent.text = newMessage;
        _confirmablePiece.gameObject.SetActive(false);
        Invoke("Remove", time);
    }
    
    public void SetText(string newMessage, bool confirmable = true)
    {
        _textComponent.text = newMessage;
        _confirmablePiece.gameObject.SetActive(confirmable);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
