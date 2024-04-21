using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hoverable : MonoBehaviour, IPointerEnterHandler
{
    public void OnMouseOver()
    {
        Debug.Log("hovered");
        AudioManager.PlaySound(SoundKey.MenuHover, Vector2.zero);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("hovered");
        AudioManager.PlaySound(SoundKey.MenuHover, Vector2.zero);
    }
}
