using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baguette : Bread
{
    [SerializeField] private Transform throwPos;
    [SerializeField] private Transform swanPos;
    [SerializeField] private GameObject baguetteObj;
    [SerializeField] private float throwBaguetteSpeed;
    [SerializeField] private bool throwBaguette;

    [SerializeField] private Animator baguetteSpike;

    private void Update()
    {
        if (throwBaguette)
        {
            ThrowTomato();
        }
    }

    private void ThrowTomato()
    {
        baguetteObj.SetActive(true);

        baguetteObj.transform.position = Vector3.MoveTowards(baguetteObj.transform.position, swanPos.position, throwBaguetteSpeed * Time.deltaTime);

        if (Vector3.Distance(baguetteObj.transform.position, swanPos.position) <= 0.1f)
        {
            baguetteObj.SetActive(false);
            baguetteObj.transform.position = throwPos.position;
            throwBaguette = false;
        }
    }

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

    public override void OnDamage(DamageState damageState)
    {
        switch (damageState)
        {
            case DamageState.DamageState:
                break;
            case DamageState.DeathState:
                gameObject.SetActive(false);
                break;
        }

        SoundManager.Instance.OnPlayBaguetteDeath();
    }

    public override void SpecialPower()
    {
        if(Random.Range(0, 2) == 0)
        {
            if (breadMana <= special1Cost)
            {
                Fight();
            }

            fightType = FightType.SpecialSkillOne;

            animTrigger = "special1";
        }
        else
        {
            if (breadMana <= special2Cost)
            {
                Fight();
            }

            fightType = FightType.SpecialSkillTwo;

            animTrigger = "special2";
        }
    }

    public override void DoAction()
    {
        if (GameObject.FindWithTag("Swan").TryGetComponent(out Swan swan))
        {
            switch (fightType)
            {
                case FightType.BasicState:
                    DamageFromCurrentAttack = basicAttackDamage;
                    swan.IncomingDamage = DamageFromCurrentAttack;
                    throwBaguette = true;
                    OnAttack(fightType);
                    break;
                case FightType.HeavyState:
                    DamageFromCurrentAttack = heavyAttackDamage;
                    swan.IncomingDamage = DamageFromCurrentAttack;
                    OnAttack(FightType.HeavyState);
                    break;
                case FightType.SpecialSkillOne:
                    SoundManager.Instance.OnPlayBaguetteSkillOne();
                    DamageFromCurrentAttack = specialAttack1Damage;
                    swan.IncomingDamage = DamageFromCurrentAttack;
                    break;
                case FightType.SpecialSkillTwo:
                    baguetteSpike.SetTrigger("activate");
                    SoundManager.Instance.OnPlayBaguetteSkillTwo();
                    DamageFromCurrentAttack = specialAttack2Damage;
                    swan.IncomingDamage = DamageFromCurrentAttack;
                    break;
            }
        }
    }
}
