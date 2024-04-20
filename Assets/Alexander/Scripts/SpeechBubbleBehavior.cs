using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// rough placeholder speech bubble
public class SpeechBubbleBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;
    [SerializeField] private bool confirm;
    [SerializeField] private float _lifeTime; 

    public void Start()
    {
        if(_lifeTime > 0)
        {
            Invoke("Remove", _lifeTime);
        }
    }

    public void SetText(string newMessage)
    {
        _textComponent.text = newMessage;
    }

    public void OnMouseDown()
    {
        if(confirm)
        {
            Remove();
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
