using System;
using UnityEngine;

public class CaffeinatedDrink : Item
{
    public static event Action<CaffeinatedDrink> onEntityPassive;

    public override void DoSomething()
    {
        base.DoSomething();
        onEntityPassive?.Invoke(this);
    }

    public float IncreasePassive()
    {
        return 10f;
    }
}