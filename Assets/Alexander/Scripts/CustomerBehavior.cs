using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// created by alexander
// code for customer behavior
public class CustomerBehavior : MonoBehaviour
{
    
    // called by the incoming object
    public void RecieveObject(Draggable _incommingObject)
    {
        if(_incommingObject.CompareTag("Potion"))
        {
            Destroy(gameObject);
            Destroy(_incommingObject.gameObject);
        }
    }
}
