using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Muffin : Bread
{
    [SerializeField] float healAmount;
    [SerializeField] float groupHealAmount;

    public override void OnAttack(FightType fightType)
    {
        switch (fightType)
        {
            case FightType.BasicState:
                SoundManager.Instance.OnPlayMuffinMainAttack();
                break;
            case FightType.HeavyState:
                SoundManager.Instance.OnPlayMuffinHeavyAttack();
                break;
        }
    }

    public override void OnHeal()
    {
        SoundManager.Instance.OnPlayMuffinHeal();
    }

    public override void OnDamage(DamageState damageState)
    {
        switch(damageState)
        {
            case DamageState.DamageState:
                break;
            case DamageState.DeathState:
                gameObject.SetActive(false);
                break;
        }
        
        SoundManager.Instance.OnPlayMuffinDeath();
    }

    public override IEnumerator SpecialPower()
    {
        if(Random.Range(0, 2) == 0)
        {
            if (breadMana <= special1Cost || breadManager.breadOrders.All(x => x.breadHealth / x.maxHealth >= 0.75f))
            {
                StartCoroutine(Fight());
                yield break;
            }

            Debug.Log(name + " used heal");
            UIManager.Instance.panelCurrentTurnObj.SetActive(true);
            UIManager.Instance.currentTextCurrentTurn.text = name + " used heal";

            yield return new WaitForSeconds(2f);
            UIManager.Instance.panelCurrentTurnObj.SetActive(false);
            UIManager.Instance.currentTextCurrentTurn.text = "";

            fightType = FightType.SpecialSkillOne;

            animTrigger = "special1";
        }
        else
        {
            if (breadMana <= special2Cost || breadManager.breadOrders.All(x => x.breadHealth / x.maxHealth >= 0.85f))
            {
                StartCoroutine(Fight());
                yield break;
            }

            UIManager.Instance.panelCurrentTurnObj.SetActive(true);
            UIManager.Instance.currentTextCurrentTurn.text = name + " used group heal";
            Debug.Log(name + " used group heal");

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
                    SoundManager.Instance.OnPlayMuffinSkillOne();
                    var bread = breadManager.breadOrders[Random.Range(0, breadManager.breadOrders.Count)];
                    bread.Heal(healAmount);
                    bread.HealAnimator.SetTrigger("activate");
                    UseMana(special1Cost);
                    break;
                case FightType.SpecialSkillTwo:
                    SoundManager.Instance.OnPlayMuffinSkillTwo();
                    breadManager.breadOrders.ForEach(x =>
                    {
                        x.Heal(groupHealAmount);
                        x.HealAnimator.SetTrigger("activate");
                    });
                    UseMana(special2Cost);
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
