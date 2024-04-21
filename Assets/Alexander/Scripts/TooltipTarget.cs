using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to an object to read out text on UI tooltip when it is scrolled over
public class TooltipTarget : MonoBehaviour
{
    // viewable text sent to the tooltip
    [field: SerializeField, TextArea(1,5)] public string Tooltip {get; set;}
    [SerializeField] public string displayName;

    // enable text when moused over
    public void OnMouseOver()
    {
        if(GameManager.GameActive)
        {
            UITooltip.SetText(Tooltip, displayName);
        }
    }
    
    // close text when moused off
    public void OnMouseExit()
    {
        UITooltip.SetText(null);
    }
}
