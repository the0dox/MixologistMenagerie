using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    public static int _gold;

    public static void AddGold(int value)
    {
        _gold += value;
    }
}
