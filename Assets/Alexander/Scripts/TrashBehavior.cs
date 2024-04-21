using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehavior : MonoBehaviour
{
    // called by the incoming object
    public void RecieveObject(Draggable incommingObject)
    {
        AudioManager.PlaySound(SoundKey.DropFailure, transform.position);
        DialougeManager.CreateExplosion(incommingObject.transform.position);
        Destroy(incommingObject.gameObject);
    }
}
