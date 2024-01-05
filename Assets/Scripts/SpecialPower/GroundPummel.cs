using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundPummel", menuName = "ScriptableObject/SpecialPower/GroundPummel")]
public class GroundPummel : SpecialPower
{
    public float stunDamage;
    public float stunDuration;

    public override void DoSomething()
    {
        base.DoSomething();

        Debug.Log("Entity has been stunned!");
    }
}
