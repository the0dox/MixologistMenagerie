using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeakingBehavior : MonoBehaviour
{
    private SpeechBubbleBehavior _currentSpeech;
    [SerializeField] private SoundKey _voiceType;
    [SerializeField] private Vector3 _offset;
    [SerializeField] public DialogueTemplate Template;
    private int TemplateProgress;
        
    void Awake()
    {
        if(Template.Tutroial)
        {
            CustomerManager.Tutorial = true;
        }
    }

    public void OnDestroy()
    {
        if(Template.Tutroial)
        {
            CustomerManager.Tutorial = false;
        }
    }

    public void Say(MessageTypes type)
    {
        switch(type)
        {
            case MessageTypes.Positive:
                Say(Template.positiveMessage, SoundKey.Happy, 2.5f);
                break;
            case MessageTypes.Negative:
                Say(Template.negativeMessage, SoundKey.Sad, 2.5f);
                break;
            default:
                Debug.LogWarning("Invalid message type " + type, gameObject);
            break;
        }
    }
    
    public void Say(string message, SoundKey sound, float time)
    {
        if(_currentSpeech)
        {
            _currentSpeech.Remove();
        }
        AudioManager.PlaySound(sound, transform.position);
        _currentSpeech = DialougeManager.CreateText(message, transform.position + _offset, time);
    }

    public void Say(string message, bool confirmable = false)
    {
        if(_currentSpeech)
        {
            _currentSpeech.Remove();
        }
        _currentSpeech = DialougeManager.CreateText(message, transform.position + _offset, confirmable);
    }

    public void Say(Attributes positiveStats, Attributes negativeStats)
    {
        Say(Template.customRequest + " a potion with at least: " +  positiveStats + " and no more than " + negativeStats + "?");
    }

    public bool Ask()
    {
        if(TemplateProgress < Template.dialogue.Length)
        {
            if(TemplateProgress == 0)
            {
                AudioManager.PlaySound(_voiceType, transform.position);
            }
            else
            {
                AudioManager.PlaySound(SoundKey.MenuHover, transform.position);
            }

            Say(Template.dialogue[TemplateProgress], true);
            TemplateProgress++;
            return false;
        }
        AudioManager.PlaySound(SoundKey.MenuConfirm, transform.position);
        return true;
    }
}

public enum MessageTypes
{
    None,
    Positive,
    Negative,
}
