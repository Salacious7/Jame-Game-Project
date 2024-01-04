using System;
using UnityEngine;

public class ClearBlueCrystal : Item
{
    public static event Action<ClearBlueCrystal> onEntityMana;

    public override void DoSomething()
    {
        base.DoSomething();
        onEntityMana?.Invoke(this);
    }

    public float IncreaseMana()
    {
        return 10f;
    }
}