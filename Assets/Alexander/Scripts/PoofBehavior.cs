using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofBehavior : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
