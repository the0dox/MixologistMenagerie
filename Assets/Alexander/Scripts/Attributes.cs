using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Burst.Intrinsics;
using UnityEngine;

// bag of stats, designed to be modifiable if we go a different direction
[System.Serializable]
public struct Attributes
{
    // wip stats, subject to change
    public int AttackUp;
    public int Sweetness;
    public int Bitter;
    public int FireDefense;
    public int Saltiness;
    public int Speed;

    // allows attributes to be combined with the + opertator
    public static Attributes operator + (Attributes lhs, Attributes rhs)
    {
        Attributes output = new Attributes();
        output.AttackUp = lhs.AttackUp + rhs.AttackUp;
        output.Sweetness = lhs.Sweetness + rhs.Sweetness;
        output.Bitter = lhs.Bitter + rhs.Bitter;
        output.FireDefense = lhs.FireDefense + rhs.FireDefense;
        output.Saltiness = lhs.Saltiness + rhs.Saltiness;
        output.Speed = lhs.Speed = rhs.Speed;
        return output;
    }


    // allows attributes to be combined with the + opertator
    public static Attributes operator - (Attributes lhs, Attributes rhs)
    {
        Attributes output = new Attributes();
        output.AttackUp = lhs.AttackUp - rhs.AttackUp;
        output.Sweetness = lhs.Sweetness - rhs.Sweetness;
        output.Bitter = lhs.Bitter - rhs.Bitter;
        output.FireDefense = lhs.FireDefense - rhs.FireDefense;
        output.Speed = lhs.Saltiness - rhs.Saltiness;
        output.Speed =lhs.Speed - rhs.Speed;
        return output;
    }



    // this is third one 'Equals'
    public static bool operator == (Attributes lhs, Attributes rhs)
    {
        return lhs.AttackUp == rhs.AttackUp && lhs.Sweetness == rhs.Sweetness && lhs.Bitter == rhs.Bitter && lhs.FireDefense == rhs.FireDefense && lhs.Saltiness == rhs.Saltiness && lhs.Speed == rhs.Speed;
    }
    
    // this is third one 'Equals'
    public static bool operator != (Attributes lhs, Attributes rhs)
    {
        return lhs.AttackUp != rhs.AttackUp && lhs.Sweetness != rhs.Sweetness && lhs.Bitter != rhs.Bitter && lhs.FireDefense != rhs.FireDefense && lhs.Saltiness != rhs.Saltiness && lhs.Speed != rhs.Speed;
    }

    public override bool Equals(object obj)
    {
        if((Attributes)obj != null)
        {
            return this == (Attributes)obj;
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }


    // returns a safe string of thiss structs contents
    public override string ToString()
    {
        StringBuilder output = new StringBuilder();
        if(AttackUp != 0)
        {
            output.Append("Attack Up: " + AttackUp);
        }
        if(Sweetness != 0)
        {
            output.Append(" Sweetness: " + Sweetness);
        }
        if(Bitter != 0)
        {
            output.Append(" Bitterness: " + Bitter);
        }
        if(FireDefense != 0)
        {
            output.Append(" Fire Defense: " + FireDefense);
        }
        if(Saltiness != 0)
        {
            output.Append(" Saltiness: " + Saltiness);
        }
        if(Speed != 0)
        {
            output.Append(" Speed: " + Speed);
        }
        return output.ToString();
    }

}
