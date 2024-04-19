using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

// created by alexander
// add to a physics object to pick it up and drop it
[RequireComponent(typeof(Rigidbody2D))]
public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    // the offset between the mouse and the object, if set to zero the object will be perfectly centered when held
    [SerializeField] private Vector2 _offset;
    // reference to the physics of the object
    private Rigidbody2D _body;
    // if set to true, move towards mouse position
    private bool _dragged;
    // the current mouse position
    private Vector2 _targetPosition;
    // speed at which object travels
    private const float _speed = 15;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();    
    }

    // called whenever this object is first disabled
    public void OnDisable()
    {
        if(_dragged)
        {
            OnDrop();
            _dragged = false;
        }
    }

    // called every frame
    public void FixedUpdate()
    {
        if(_dragged)
        {
            transform.position = Vector2.Lerp(transform.position, _targetPosition, Time.fixedDeltaTime * _speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * _speed); 
        }
    }

    // called whenever the object no longer dragged
    public void OnDrop()
    {
        _body.simulated = true;
    }

    // called when drag first starts
    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragged = true;
        _body.simulated = false;
        _body.totalTorque = 0;
        _body.totalForce = Vector2.zero;
    }

    // called when the object is let go
    public void OnEndDrag(PointerEventData eventData)
    {
        _dragged = false;
        OnDrop();
        if(eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out DragReciever _dragTarget))
        {
            _dragTarget.RecieveObject(this);
        }
    }
    
    // called every frame this object is grabbed, updates its position drag first starts
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) ;
        _targetPosition = mousePosition - _offset;
    }
}
