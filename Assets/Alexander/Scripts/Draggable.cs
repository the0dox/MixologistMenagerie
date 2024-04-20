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
    // public Accessor
    public bool Dragged => _dragged;
    // the offset between the mouse and the object, if set to zero the object will be perfectly centered when held
    [SerializeField] private Vector2 _offset;
    // reference to the physics of the object
    private Rigidbody2D _body;
    // if set to true, move towards mouse position
    [SerializeField] private bool _dragged;
    // the current mouse position
    private Vector2 _targetPosition;
    // speed at which object travels
    private const float _speed = 15;
    // reference to the position the object was last held it can return to
    private Vector2 originalPosition;
    [SerializeField] private float _returnTime;
    [SerializeField] private SoundKey _pickUpSound;

    // Start is called before the first frame update
    void Awake()
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
        _dragged = false;
        _body.simulated = true;
    }

    public void OnPickup()
    {
        StopAllCoroutines();
        AudioManager.PlaySound(_pickUpSound, transform.position);
        _dragged = true;
        _body.simulated = false;
        _body.totalTorque = 0;
        _body.totalForce = Vector2.zero;
        originalPosition = transform.position;
    }

    // called when drag first starts
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnPickup();
    }

    // called when the object is let go
    public void OnEndDrag(PointerEventData eventData)
    {
        OnDrop();
        if(eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out DragReciever _dragTarget))
        {
            _dragTarget.RecieveObject(this);
        }
        else
        {
            Return();
        }
        Debug.Log($"dragged onto {eventData.pointerCurrentRaycast.gameObject}", eventData.pointerCurrentRaycast.gameObject);
    }

    public void Return()
    {
        StopAllCoroutines();
        AudioManager.PlaySound(SoundKey.DropFailure, transform.position);
        StartCoroutine(ReturnToPositionDelay());
    }
    
    // called every frame this object is grabbed, updates its position drag first starts
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.Rotate(new Vector3(0, 0, 3  * Mathf.Clamp(eventData.delta.x, -1, 1)));
        _targetPosition = mousePosition - _offset;
    }

    IEnumerator ReturnToPositionDelay()
    {
        _dragged = true;
        _body.simulated = false;
        _targetPosition = originalPosition;
        while(Vector3.Distance(transform.position, originalPosition) > 0.1)
        {
            yield return new WaitForFixedUpdate();
        }
        transform.position = originalPosition;
        OnDrop();
    }
}
