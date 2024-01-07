using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baguette : Bread
{
    public override void SpecialPower()
    {
        if(Random.Range(0, 2) == 0)
        {
            if(breadMana <= special1Cost)
            {
                Fight();
                return;
            }

            Debug.Log(name + " used roll");
            DamageFromCurrentAttack = specialAttack1Damage;
            //trigger animation
        }
        else
        {
            if(breadMana <= special1Cost)
            {
                Fight();
                return;
            }

            Debug.Log(name + " used barrage");
            DamageFromCurrentAttack = specialAttack2Damage;
            //trigger animation
        }

        Attack();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
