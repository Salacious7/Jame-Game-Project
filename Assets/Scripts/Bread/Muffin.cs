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

    public override void OnDeath()
    {
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
            breadManager.breadOrders[Random.Range(0, breadManager.breadOrders.Count)].Heal(healAmount);
            UseMana(special1Cost);

            //trigger animation
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
            breadManager.breadOrders.ForEach(x => x.Heal(groupHealAmount));
            UseMana(special2Cost);

            //trigger animation
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
