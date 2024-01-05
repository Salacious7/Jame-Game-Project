using System;
using System.Collections;
using UnityEngine;

public class ShinyFeather : Item
{
    public override void DoSomething()
    {
        base.DoSomething();
    }

    public float IncreaseDamage()
    {
        return 10f;
    }
}