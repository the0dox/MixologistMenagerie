using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

// created by alexander
// code for customer behavior
public class CustomerBehavior : MonoBehaviour
{
    // reference to the animator attached to this charater
    private Animator _animationComponent;
    public SpeakingBehavior _speakingComponent;
    // library of potential looks
    [SerializeField] private GameObject[] _sprites;
    [SerializeField] private Attributes _desiredAttributes;
    [SerializeField] private Attributes _unwantedAttributes;
    private SpriteResolver _resolver;

    [SerializeField] private Attributes[] _potentialAttributes;
    private bool _asked;

    // generate random look on creation
    public void Awake()
    {
        // create a random character if I have one
        if(_sprites.Length > 0)
        {
            GameObject visual = Instantiate(_sprites[Random.Range(0, _sprites.Length)], transform);
            GeneratePersonality();
        }
        _animationComponent = GetComponentInChildren<Animator>();
        _speakingComponent = GetComponentInChildren<SpeakingBehavior>();
        Preference current = GetComponentInChildren<Preference>();
        _desiredAttributes = current.perfered;
        _unwantedAttributes = current.disliked;
        _resolver = GetComponentInChildren<SpriteResolver>();
        AudioManager.PlaySound(SoundKey.WalkUp, transform.position);
    }

    void GeneratePersonality()
    {
        _desiredAttributes = _potentialAttributes[Random.Range(0,_potentialAttributes.Length)];
        _unwantedAttributes = _potentialAttributes[Random.Range(0,_potentialAttributes.Length)];
        while(_unwantedAttributes == _desiredAttributes && _potentialAttributes.Length > 1)
        {   
            _unwantedAttributes = _potentialAttributes[Random.Range(0,_potentialAttributes.Length)];
        }
    }
    
    // called by the incoming object
    public void RecieveObject(Draggable _incommingObject)
    {
        if(_asked && _incommingObject.CompareTag("Potion"))
        {
            EvaluatePotion(_incommingObject.GetComponent<PotionBehavior>());
        }
        else
        {
            _incommingObject.Return();
        }
    }

    public void OnMouseDown()
    {
        Ask();
    }

    public void Ask()
    {
        if(!_asked)
        {
            _resolver.SetCategoryAndLabel(_resolver.GetCategory(), "Talking");
            _animationComponent.SetTrigger("Asking");
            if(_speakingComponent.Ask())
            {
                _asked = true;   
                _speakingComponent.Say(_desiredAttributes, _unwantedAttributes);
            }
        }
    }

    // determines how much a customer likes the potione they recieved
    public void EvaluatePotion(PotionBehavior incomingPotion)
    {
        Attributes preferenceDiff = incomingPotion.Stats - _desiredAttributes;
        int score = 0;
        Debug.Log("positive pref: " + preferenceDiff);
        // wip get higher than wanted attributes
        if(_desiredAttributes.AttackUp != 0)
        {
            score += preferenceDiff.AttackUp;
        }
        if(_desiredAttributes.Sweetness != 0)
        {
            score += preferenceDiff.Sweetness;
        }
        if(_desiredAttributes.Bitter != 0)
        {
            score += preferenceDiff.Bitter;
        }
        if(_desiredAttributes.FireDefense != 0)
        {
            score += preferenceDiff.FireDefense;
        }

        // wip don't get higher than unwanted attributes
        preferenceDiff = incomingPotion.Stats - _unwantedAttributes;
        
        if(_unwantedAttributes.AttackUp != 0)
        {
            score -= preferenceDiff.AttackUp;
        }
        if(_unwantedAttributes.Sweetness != 0) 
        {
            score -= preferenceDiff.Sweetness;
        }
        if(_unwantedAttributes.Bitter != 0) 
        {
            score -= preferenceDiff.Bitter;
        }
        if(_unwantedAttributes.FireDefense != 0) 
        {
            score -= preferenceDiff.FireDefense;
        }
        Debug.Log("score: " + score);
        DialougeManager.CreateExplosion(incomingPotion.transform.position);
        Destroy(incomingPotion.gameObject);
        if(score >= 0)
        {
            _resolver.SetCategoryAndLabel(_resolver.GetCategory(), "Positive");
            AudioManager.PlaySound(SoundKey.Happy, transform.position);
            _animationComponent.SetTrigger("Happy");
            _speakingComponent.Say(MessageTypes.Positive);
        }
        else
        {
            _resolver.SetCategoryAndLabel(_resolver.GetCategory(), "Negative");
            AudioManager.PlaySound(SoundKey.Sad, transform.position);
            _animationComponent.SetTrigger("Angry");
            _speakingComponent.Say(MessageTypes.Negative);
            if(_speakingComponent.Template.Tutroial)
            {
                Invoke("Reask", 2f);
                return;
            }
        }
        {
            StartCoroutine(LeaveDelay(3, Mathf.Clamp(score + 15, 5, 100)));
        }
    }

    public void Reask()
    {
        _resolver.SetCategoryAndLabel(_resolver.GetCategory(), "Talking");
        _animationComponent.SetTrigger("Asking");
        _speakingComponent.Say(_desiredAttributes, _unwantedAttributes);
    }

    IEnumerator LeaveDelay(float seconds, int score)
    {
        yield return new WaitForSeconds(seconds/2);
        ScoreManager.AddGold(score + 10);
        yield return new WaitForSeconds(seconds/2);
        _animationComponent.SetTrigger("Exit");
        AudioManager.PlaySound(SoundKey.WalkAway, transform.position);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
