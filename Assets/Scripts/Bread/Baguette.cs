using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baguette : Bread
{
    public override void OnAttack(FightType fightType)
    {
        switch(fightType)
        {
            case FightType.BasicState:
                SoundManager.Instance.OnPlayBaguetteMainAttack();
                break;
            case FightType.HeavyState:
                SoundManager.Instance.OnPlayBaguetteHeavyAttack();
                break;
        }
    }

    public override void OnHeal()
    {
        SoundManager.Instance.OnPlayBaguetteHeal();
    }

    public override void OnDeath()
    {
        SoundManager.Instance.OnPlayBaguetteDeath();
    }

    public override void SpecialPower()
    {
        if(Random.Range(0, 2) == 0)
        {
            if(breadMana <= special1Cost)
            {
                SoundManager.Instance.OnPlayBaguetteSkillOne();
                Fight();
                return;
            }

            Debug.Log(name + " used roll");
            DamageFromCurrentAttack = specialAttack1Damage;
            if(GameObject.FindWithTag("Swan").TryGetComponent(out Swan swan))
                swan.IncomingDamage = DamageFromCurrentAttack;
            //trigger animation
        }
        else
        {
            if(breadMana <= special2Cost)
            {
                SoundManager.Instance.OnPlayBaguetteSkillTwo();
                Fight();
                return;
            }

            Debug.Log(name + " used barrage");
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
