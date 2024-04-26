using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// behavior for an ingredient
public class IngredientBehavior : MonoBehaviour
{
    // currently only contains a bucket of stats
    [field: SerializeField] public Attributes Stats {get; private set;}
    [SerializeField] private TooltipTarget _tooltip;

    public override string ToString()
    {
        return Stats.ToStringLine();
    }

    public void OnValidate()
    {
        if(_tooltip)
        {
            _tooltip.Tooltip = ToString();
        }
    }
}
