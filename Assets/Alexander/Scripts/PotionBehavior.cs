using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// behavior of potions
public class PotionBehavior : MonoBehaviour
{
    // potions contain a bucket of stats from the ingredients its made out of
    [field: SerializeField] public Attributes Stats {get; private set;} 
    [SerializeField] private Sprite[] _variants;
    private TooltipTarget _tooltip;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = _variants[Random.Range(0, _variants.Length)];
        _tooltip = GetComponent<TooltipTarget>();
        if(Random.Range(0,2) < 1)
        {
            AudioManager.PlaySound(SoundKey.PotionReady1,transform.position);
        }   
        else
        {
            AudioManager.PlaySound(SoundKey.PotionReady2,transform.position);
        }
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
        _tooltip.Tooltip = Stats.ToStringLine();
    }
}
