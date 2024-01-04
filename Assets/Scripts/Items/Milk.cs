using System;
using UnityEngine;

public class Milk : Item
{
    public static event Action onEntityRemoveNegativeStatus;

    public override void DoSomething()
    {
        base.DoSomething();

        onEntityRemoveNegativeStatus?.Invoke();
    }
}