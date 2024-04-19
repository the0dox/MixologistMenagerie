using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// created by alexander
// add to classes that can recieve draggable objects
public class DragReciever : MonoBehaviour
{
    // event raised when dragged
    [SerializeField] private UnityEvent<Draggable> _recievedObject;

    // called by the incoming object
    public void RecieveObject(Draggable _incommingObject)
    {
        _recievedObject.Invoke(_incommingObject);
    }
} 