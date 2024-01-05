using System;
using UnityEngine;

public class CaffeinatedDrink : Item
{
    public override void DoSomething()
    {
        base.DoSomething();
    }

    public float IncreasePassive()
    {
        return 10f;
    }
}