using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderBehavior : MonoBehaviour
{
    private const int SIZE = 10;
    private GameObject[] _contents;
    private int _index;
    [SerializeField] private GameObject _potionPrefab;

    void Start()
    {
        _contents = new GameObject[SIZE];
        ClearContents();
    }

    void OnDestroy()
    {
        _contents = null;
    }

    private void ClearContents()
    {
        for(int i = 0; i < SIZE; i++)
        {
            if(_contents[i])
            {
                GameObject _instance = _contents[i];
                _contents[i] = null;
                Destroy(_instance);
            }
        }
        _index = 0;
    }

    private void AddIngredient(Draggable newItem)
    {
        _contents[_index] = newItem.gameObject;
        _index++;
    }

    // called by the incoming object
    public void RecieveObject(Draggable incommingObject)
    {
        if(incommingObject.CompareTag("Ingredient") && _index < SIZE)
        {
            incommingObject.transform.position = transform.position + Vector3.up * 3;
            incommingObject.enabled = false;
            AddIngredient(incommingObject);
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("click");
        Blend();
    }

    public void Blend()
    {
        if(_index > 0)
        {
            ClearContents();
            GameObject potion = Instantiate(_potionPrefab);
            potion.transform.position = transform.position + (Vector3.right * 4);
        }
        else
        {
            Debug.Log("no contents");
        }
    }
}
