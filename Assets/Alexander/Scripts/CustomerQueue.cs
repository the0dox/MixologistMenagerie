using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

// manages the customers over the course of a day
public class CustomerQueue : MonoBehaviour
{
    // total number of customers that needs to be served before the day ends
    [SerializeField] private int _totalCostumers;
    // reference to a customer prefab
    [SerializeField] private GameObject _customerPrefab;
    // reference to the positions on the screen customers can fill
    [SerializeField] private CustomerNode[] _nodes;
    // remaining customers that need to be served
    private int _remainingCustomers; 
    // ammount of time between customers entering
    [SerializeField] private float intervalCheck = 10; 

    // called before first frame
    void Start()
    {
        StartDay();
    }

    // begins the day
    void StartDay()
    {
        _remainingCustomers = _totalCostumers;
        InvokeRepeating("CustomerCheck",0, intervalCheck);
    }

    // checks to see if any slots are available and attempts to add a customer to that slot
    void CustomerCheck()
    {
        if(_remainingCustomers > 0)
        {
            for(int i = 0; i < _nodes.Length; i++)
            {
                if(_nodes[i].Available)
                {
                    CustomerBehavior newCustomer = Instantiate(_customerPrefab).GetComponent<CustomerBehavior>();
                    _nodes[i].AddCustomer(newCustomer);
                    _remainingCustomers--;
                    return;
                }
            }
        }
    }
}