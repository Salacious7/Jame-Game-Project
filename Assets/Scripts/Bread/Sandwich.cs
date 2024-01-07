using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandwich : Bread
{
    public override void OnAttack(FightType fightType)
    {
        switch (fightType)
        {
            case FightType.BasicState:
                SoundManager.Instance.OnPlaySandwichMainAttack();
                break;
            case FightType.HeavyState:
                SoundManager.Instance.OnPlaySandwichHeavyAttack();
                break;
        }
    }

    public override void OnHeal()
    {
        SoundManager.Instance.OnPlaySandwichHeal();
    }

    public override void OnDeath()
    {
        SoundManager.Instance.OnPlaySandwichDeath();
    }

    public override void SpecialPower()
    {
        if(Random.Range(0, 2) == 0)
        {
            if(breadMana <= special1Cost)
            {
                SoundManager.Instance.OnPlaySandwichSkillOne();
                Fight();
                return;
            }

            Debug.Log(name + " used hot sauce");
            DamageFromCurrentAttack = specialAttack1Damage;
            if(GameObject.FindWithTag("Swan").TryGetComponent(out Swan swan))
                swan.IncomingDamage = DamageFromCurrentAttack;
            //trigger animation
        }
        else
        {
            if(breadMana <= special2Cost)
            {
                SoundManager.Instance.OnPlaySandwichSkillTwo();
                Fight();
                return;
            }

            Debug.Log(name + " used mayo");
            DamageFromCurrentAttack = specialAttack2Damage;
            if(GameObject.FindWithTag("Swan").TryGetComponent(out Swan swan))
                swan.IncomingDamage = DamageFromCurrentAttack;
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
