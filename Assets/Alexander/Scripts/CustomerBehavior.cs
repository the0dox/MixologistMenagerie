using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// created by alexander
// code for customer behavior
public class CustomerBehavior : MonoBehaviour
{
    // reference to the animator attached to this charater
    private Animator _animationComponent;
    private SpeakingBehavior _speakingComponent;
    // library of potential looks
    [SerializeField] private GameObject[] _sprites;
    [SerializeField] private Attributes _desiredAttributes;
    [SerializeField] private Attributes _unwantedAttributes;
    [SerializeField] private Vector3 _textOffset;
    private bool _asked;

    // generate random look on creation
    public void Awake()
    {
        GameObject visual = Instantiate(_sprites[Random.Range(0, _sprites.Length)], transform);
        _animationComponent = GetComponentInChildren<Animator>();
        _speakingComponent = GetComponentInChildren<SpeakingBehavior>();
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
        if(!_asked && _animationComponent.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            _animationComponent.SetTrigger("Asking");
            _speakingComponent.Say("Can I get a potion with at least: " +  _desiredAttributes + " and no more than " + _unwantedAttributes + "?");
            _asked = true;
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
        Destroy(incomingPotion.gameObject);
        if(score >= 0)
        {
            _animationComponent.SetTrigger("Happy");
            _speakingComponent.Say(MessageTypes.Positive);
        }
        else
        {
            _animationComponent.SetTrigger("Angry");
            _speakingComponent.Say(MessageTypes.Negative);
        }
        ScoreManager.AddGold(score + 10);
        StartCoroutine(LeaveDelay(3));
    }

    IEnumerator LeaveDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _animationComponent.SetTrigger("Exit");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
