using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlenderBehavior : MonoBehaviour
{
    private const int SIZE = 10;
    private IngredientBehavior[] _contents;
    private int _index;
    [SerializeField] private GameObject _potionPrefab;
    [SerializeField] private Transform _depositPoint;
    [SerializeField] private float _radius; 
    private Animator _animationComponent;
    private bool _interactable;
    private AudioSource _sound;
    [SerializeField] private float _blendTime = 2;

    void Start()
    {
        _interactable = true;
        _contents = new IngredientBehavior[SIZE];
        _animationComponent = GetComponent<Animator>();
        _sound = GetComponent<AudioSource>();
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
        AudioManager.PlaySound(SoundKey.DropSuccessful, transform.position);
        _contents[_index] = newItem;
        _index++;
    }

    // called by the incoming object
    public void RecieveObject(Draggable incommingObject)
    {
        if(incommingObject.CompareTag("Ingredient") && _index < SIZE && _interactable)
        {
            incommingObject.enabled = false;
            if(Vector2.Distance(transform.position, incommingObject.transform.position) > _radius)
            {
                incommingObject.transform.position += (transform.position - incommingObject.transform.position).normalized * _radius / 2;
            }
            AddIngredient(incommingObject.GetComponent<IngredientBehavior>());
        }
        else
        {
            incommingObject.Return();
        }
    }

    public void OnMouseDown()
    {
        Blend();
    }

    public void Blend()
    {
        if(_interactable && _index > 0)
        {
            StopAllCoroutines();
            StartCoroutine(BlendDelay());
        }
        else
        {
            Debug.Log("no contents");
            AudioManager.PlaySound(SoundKey.DropFailure, transform.position);
            _animationComponent.SetTrigger("Fail");
        }
    }

    IEnumerator BlendDelay()
    {
        _sound.Play();
        _interactable = false;
        float dir = 1;
        for(float i = 0; i < _blendTime; i += Time.fixedDeltaTime)
        {
            _animationComponent.SetFloat("Speed", 6 * dir, 3, Time.fixedDeltaTime);
            if(i > _blendTime/2)
            {
                dir = 0;
            }
            yield return new WaitForFixedUpdate();
        }
        _animationComponent.SetFloat("Speed", 0);
        _interactable = true;
        PotionBehavior potion = Instantiate(_potionPrefab).GetComponent<PotionBehavior>();
        potion.CombineIngredients(_contents);
        ClearContents();
        potion.transform.position = _depositPoint.position;
    }
}
