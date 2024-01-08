using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandwich : Bread
{
    [SerializeField] private Transform throwPos;
    [SerializeField] private Transform swanPos;
    [SerializeField] private GameObject tomatoObj;
    [SerializeField] private float throwTomatoSpeed;
    [SerializeField] private bool throwTomato;

    private void Update()
    {
        if(throwTomato)
        {
            ThrowTomato();
        }
    }

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
            animTrigger = "special1";
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
            animTrigger = "special2";
        }

        Attack();
    }

    private void ThrowTomato()
    {
        tomatoObj.SetActive(true);

        tomatoObj.transform.position = Vector3.MoveTowards(tomatoObj.transform.position, swanPos.position, throwTomatoSpeed * Time.deltaTime);

        if(Vector3.Distance(tomatoObj.transform.position, swanPos.position) <= 0.1f)
        {
            tomatoObj.SetActive(false);
            tomatoObj.transform.position = throwPos.position;
            throwTomato = false;
        }
    }
}
