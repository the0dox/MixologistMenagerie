using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeakingBehavior : MonoBehaviour
{
    private SpeechBubbleBehavior _currentSpeech;
    [SerializeField, TextArea(1,5)] private string _positiveMessage;
    [SerializeField, TextArea(1,5)] private string _negativeMessage;
    [SerializeField] private SoundKey _voiceType;
    [SerializeField] private Vector3 _offset;

    public void Say(MessageTypes type)
    {
        switch(type)
        {
            case MessageTypes.Positive:
                Say(_positiveMessage, SoundKey.Happy);
                break;
            case MessageTypes.Negative:
                Say(_negativeMessage, SoundKey.Sad);
                break;
            default:
                Debug.LogWarning("Invalid message type " + type, gameObject);
            break;
        }
    }

    public void Say(string message, SoundKey sound = SoundKey.None)
    {
        if(_currentSpeech)
        {
            _currentSpeech.Remove();
        }
        if(sound == SoundKey.None)
        {
            sound = _voiceType;
        }
        AudioManager.PlaySound(sound, transform.position);
        _currentSpeech = DialougeManager.CreateText(message, transform.position + _offset);
    }

    void OnDestroy()
    {
        if(_currentSpeech)
        {
            _currentSpeech.Remove();
        }
    }
}

public enum MessageTypes
{
    None,
    Positive,
    Negative,
}
