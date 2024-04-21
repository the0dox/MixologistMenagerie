using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

// story queue that doesn't randomly spit character
public class CustomerManager : MonoBehaviour
{
    // reference to a customer prefab
    [SerializeField] private GameObject _customerPrefab;
    // reference to the characters that will appear in order!
    [SerializeField] private GameObject[] _characters;
    // reference to the positions on the screen customers can fill
    [SerializeField] private CustomerNode[] _nodes;
    // remaining customers that need to be served
    private int _customerIndex; 
    // ammount of time between customers entering
    [SerializeField] private float intervalCheck = 10; 

    // called before first frame
    void Start()
    {
        foreach(GameObject character in _characters)
        {
            character.gameObject.SetActive(false);
        }
        StartDay();
    }

    // begins the day
    void StartDay()
    {
        InvokeRepeating("CustomerCheck",0, intervalCheck);
    }

    // checks to see if any slots are available and attempts to add a customer to that slot
    void CustomerCheck()
    {
        if(_customerIndex < _characters.Length)
        {
            for(int i = 0; i < _nodes.Length; i++)
            {
                if(_nodes[i].Available)
                {
                    GameObject currentCharacter = _characters[i];
                    CustomerBehavior newCustomer = Instantiate(_customerPrefab).GetComponent<CustomerBehavior>();
                    currentCharacter.transform.SetParent(newCustomer.transform, false);
                    currentCharacter.transform.localPosition = Vector3.zero;    
                    currentCharacter.SetActive(true);
                    newCustomer.gameObject.SetActive(true);
                    _nodes[i].AddCustomer(newCustomer);
                    _customerIndex++;
                    return;
                }
            }
        }
        else
        {
            for(int i = 0; i < _nodes.Length; i++)
            {
                if(!_nodes[i].Available)
                {
                    return;
                }
            }
            Debug.Log("all customers satsified");
            GameManager.WinGame();
            StopAllCoroutines();
        }
    }
}