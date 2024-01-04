using System;
using System.Collections;
using UnityEngine;

public class ShinyFeather : Item
{
    public static event Action<ShinyFeather> onIncreaseDamage;

    public override void DoSomething()
    {
        base.DoSomething();
        onIncreaseDamage?.Invoke(this);
    }

    public float IncreaseDamage()
    {
        return 10f;
    }
}