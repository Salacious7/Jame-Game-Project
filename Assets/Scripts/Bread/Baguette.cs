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
            UIManager.Instance.currentTextCurrentTurn.text = name + " used roll";
            Debug.Log(name + " used roll");

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
            UIManager.Instance.currentTextCurrentTurn.text = name + " used barrage";
            Debug.Log(name + " used barrage");

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
                case FightType.SpecialSkillOne:
                    SoundManager.Instance.OnPlayBaguetteSkillOne();
                    DamageFromCurrentAttack = specialAttack1Damage;
                    swan.IncomingDamage = DamageFromCurrentAttack;
                    break;
                case FightType.SpecialSkillTwo:
                    SoundManager.Instance.OnPlayBaguetteSkillTwo();
                    DamageFromCurrentAttack = specialAttack2Damage;
                    swan.IncomingDamage = DamageFromCurrentAttack;
                    break;
            }
        }
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
