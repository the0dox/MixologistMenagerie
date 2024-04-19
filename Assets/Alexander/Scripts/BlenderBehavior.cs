using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderBehavior : MonoBehaviour
{
    private const int SIZE = 10;
    private IngredientBehavior[] _contents;
    private int _index;
    [SerializeField] private GameObject _potionPrefab;
    [SerializeField] private Transform _depositPoint;
    void Start()
    {
        _contents = new IngredientBehavior[SIZE];
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
                IngredientBehavior _instance = _contents[i];
                _contents[i] = null;
                Destroy(_instance.gameObject);
            }
        }
        _index = 0;
    }

    private void AddIngredient(IngredientBehavior newItem)
    {
        _contents[_index] = newItem;
        _index++;
    }

    // called by the incoming object
    public void RecieveObject(Draggable incommingObject)
    {
        if(incommingObject.CompareTag("Ingredient") && _index < SIZE)
        {
            incommingObject.transform.position = transform.position + Vector3.up * 3;
            incommingObject.enabled = false;
            AddIngredient(incommingObject.GetComponent<IngredientBehavior>());
        }
        else
        {
            incommingObject.Return();
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
            PotionBehavior potion = Instantiate(_potionPrefab).GetComponent<PotionBehavior>();
            potion.CombineIngredients(_contents);
            ClearContents();
            potion.transform.position = _depositPoint.position;
        }
        else
        {
            Debug.Log("no contents");
        }
    }
}
