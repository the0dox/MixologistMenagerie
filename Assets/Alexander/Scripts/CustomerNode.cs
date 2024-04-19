using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// simple holder node for a customer at the table
public class CustomerNode : MonoBehaviour
{
    // returns true if there is no customer at this point
    public bool Available => !_activeCustomer;
    // reference to the customer at this point
    private CustomerBehavior _activeCustomer;

    // adds a new customer at this node location
    public void AddCustomer(CustomerBehavior newCustomer)
    {
        newCustomer.transform.SetParent(transform);
        newCustomer.transform.localPosition = Vector3.zero;
        _activeCustomer = newCustomer;
    }
}
