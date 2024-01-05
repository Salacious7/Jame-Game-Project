using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwanLeap", menuName = "ScriptableObject/SpecialPower/SwanLeap")]
public class SwanLeap: SpecialPower
{
    public float slowDuration;

    public override void DoSomething()
    {
        base.DoSomething();

        Debug.Log("Entity has been slowed!");
    }
}
