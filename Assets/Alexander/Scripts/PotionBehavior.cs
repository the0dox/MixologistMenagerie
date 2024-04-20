using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// behavior of potions
public class PotionBehavior : MonoBehaviour
{
    // potions contain a bucket of stats from the ingredients its made out of
    [field: SerializeField] public Attributes Stats {get; private set;} 
    private TooltipTarget _tooltip;

    private void Awake()
    {
        _tooltip = GetComponent<TooltipTarget>();
    }


    // takes a set of ingredients and combines their stats into this potion
    public void CombineIngredients(IngredientBehavior[] contents)
    {
        Stats = new Attributes();
        foreach(IngredientBehavior ingredient in contents)
        {  
            if(ingredient)
            {  
                Stats += ingredient.Stats;
            }
        }
        _tooltip.Tooltip = this.ToString();
    }

    public override string ToString()
    {
        return name + " " + Stats;
    }
}
