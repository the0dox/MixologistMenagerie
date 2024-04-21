using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// behavior for boxes containing ingredients
public class BoxBehavior : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    // reference to the ingredient this box produces
    [SerializeField] private GameObject _prefab;
    // reference to the current ingredient being dragged
    private Draggable _active;

    void Awake()
    {
        TooltipTarget myTarget = GetComponent<TooltipTarget>();
        TooltipTarget prefabTaget = _prefab.GetComponent<TooltipTarget>();
        myTarget.Tooltip = prefabTaget.Tooltip;
        myTarget.displayName = prefabTaget.displayName;
    }


    // when an ingredient is pulled back to this box because it was dragged to an invalid point delete it
    void OnTriggerStay2D(Collider2D other)
    {
        if(_active && other.gameObject.Equals(_active.gameObject) && !_active.Dragged)
        {
            Destroy(_active.gameObject);
            _active = null;
        }
    }

    // create an instance and begin dragging it
    public void OnBeginDrag(PointerEventData eventData)
    {
        DialougeManager.CreateExplosion(transform.position);
        _active = Instantiate(_prefab).GetComponent<Draggable>();
        _active.transform.position = transform.position;
        _active.OnBeginDrag(eventData);
    }

    // pass pointer data to instance
    public void OnEndDrag(PointerEventData eventData)
    {
        _active.OnEndDrag(eventData);
    }

    // pass pointer data to instance
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _active.OnDrag(eventData);
    }
}



