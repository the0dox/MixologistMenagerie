using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// created by alexander
// code for customer behavior
public class CustomerBehavior : MonoBehaviour
{
    // reference to the renderer
    private SpriteRenderer _renderer;
    // library of potential looks
    [SerializeField] private Sprite[] _sprites;

    // generate random look on creation
    public void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }
    
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
