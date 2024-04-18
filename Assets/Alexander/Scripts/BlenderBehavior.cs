using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderBehavior : MonoBehaviour
{
    private Stack<Draggable> _contents = new Stack<Draggable>();
    [SerializeField] private GameObject _potionPrefab;

    // called by the incoming object
    public void RecieveObject(Draggable _incommingObject)
    {
        _contents.Push(_incommingObject);
    }

    public void Blend()
    {
        if(_contents.Count > 0)
        {
            while(_contents.Count > 0)
            {
                Destroy(_contents.Pop().gameObject);
            }
            _contents.Clear();
            GameObject potion = Instantiate(_potionPrefab);
            potion.transform.position = transform.position + (Vector3.right * 4);
        }
    }
}
