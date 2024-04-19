using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// behavior of potions
public class PotionBehavior : MonoBehaviour
{
    // potions contain a bucket of stats from the ingredients its made out of
    [field: SerializeField] public Attributes Stats {get; private set;} 

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
            
    }
}
