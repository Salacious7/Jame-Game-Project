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

    public override void SpecialPower()
    {
        if(Random.Range(0, 2) == 0)
        {
            if(breadMana <= special1Cost || breadManager.breadOrders.All(x => x.breadHealth / x.maxHealth >= 0.75f))
            {
                SoundManager.Instance.OnPlayMuffinSkillOne();
                Fight();
                return;
            }

            Debug.Log(name + " used heal");
            var bread = breadManager.breadOrders[Random.Range(0, breadManager.breadOrders.Count)];
            bread.Heal(healAmount);
            bread.HealAnimator.SetTrigger("activate");
            UseMana(special1Cost);

            //trigger animation
            animTrigger = "special1";
        }
        else
        {
            if(breadMana <= special2Cost || breadManager.breadOrders.All(x => x.breadHealth / x.maxHealth >= 0.85f))
            {
                SoundManager.Instance.OnPlayMuffinSkillTwo();
                Fight();
                return;
            }

            Debug.Log(name + " used group heal");
            breadManager.breadOrders.ForEach(x => 
            {
                x.Heal(groupHealAmount);
                x.HealAnimator.SetTrigger("activate");
            });
            UseMana(special2Cost);

            //trigger animation
            animTrigger = "special2";
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
