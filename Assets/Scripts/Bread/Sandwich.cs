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

    [SerializeField] private Animator mayoAnim, ketchupAnim;

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

    public override IEnumerator SpecialPower()
    {
        if(Random.Range(0, 2) == 0)
        {
            if (breadMana <= special1Cost)
            {
                StartCoroutine(Fight());
                yield break;
            }

            UIManager.Instance.panelCurrentTurnObj.SetActive(true);
            UIManager.Instance.currentTextCurrentTurn.text = name + " used hot sauce";
            Debug.Log(name + " used hot sauce");

            yield return new WaitForSeconds(2f);
            UIManager.Instance.panelCurrentTurnObj.SetActive(false);
            UIManager.Instance.currentTextCurrentTurn.text = "";

            fightType = FightType.SpecialSkillOne;

            animTrigger = "special1";
        }
        else
        {
            if (breadMana <= special2Cost)
            {
                StartCoroutine(Fight());
                yield break;
            }

            UIManager.Instance.panelCurrentTurnObj.SetActive(true);
            UIManager.Instance.currentTextCurrentTurn.text = name + " used mayo";
            Debug.Log(name + " used mayo");

            yield return new WaitForSeconds(2f);
            UIManager.Instance.panelCurrentTurnObj.SetActive(false);
            UIManager.Instance.currentTextCurrentTurn.text = "";

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
                    throwTomato = true;
                    OnAttack(fightType);
                    break;
                case FightType.HeavyState:
                    DamageFromCurrentAttack = heavyAttackDamage;
                    swan.IncomingDamage = DamageFromCurrentAttack;
                    OnAttack(FightType.HeavyState);
                    break;
                case FightType.SpecialSkillOne:
                    ketchupAnim.SetTrigger("activate");
                    SoundManager.Instance.OnPlaySandwichSkillOne();
                    DamageFromCurrentAttack = specialAttack1Damage;
                    swan.IncomingDamage = DamageFromCurrentAttack;
                    break;
                case FightType.SpecialSkillTwo:
                    mayoAnim.SetTrigger("activate");
                    SoundManager.Instance.OnPlaySandwichSkillTwo();
                    DamageFromCurrentAttack = specialAttack2Damage;
                    swan.IncomingDamage = DamageFromCurrentAttack;
                    break;
            }
        }
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
