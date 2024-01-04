using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadCrumbs : Item
{
    public static event Action<BreadCrumbs> onEntityHeal;

    public override void DoSomething()
    {
        base.DoSomething();
        onEntityHeal?.Invoke(this);
    }

    public float IncreaseHealth()
    {
        return 10f;
    }
}
