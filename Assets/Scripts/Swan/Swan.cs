using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SwanData
{
    public float health;
    public float damage;
    public float mana;
    public float passive;
    public float defense;
    public float basicDamage;
    public float heavyDamage;

    [Header("Negative Status")]
    public List<SpecialPower> negativeStatus = new List<SpecialPower>();
}

public class Swan : MonoBehaviour, IActionState, OnEventHandler
{
    [SerializeField] private SwanData swanData;
    private SwanState swanState;
    private SwanUI swanUI;

    public enum FightType
    {
        BasicState,
        HeavyState
    }

    private FightType fightType;

    private void Awake()
    {
        swanState = GetComponent<SwanState>();
        swanUI = GetComponent<SwanUI>();

        BreadCrumbs.onEntityHeal += HealSwan;
        ShinyFeather.onIncreaseDamage += ItemDamage;
        ClearBlueCrystal.onEntityMana += ItemMana;
        CaffeinatedDrink.onEntityPassive += ItemPassive;
        Milk.onEntityRemoveNegativeStatus += ItemRemoveNegativeStatus;
    }

    public void Fight()
    {
        swanUI.ActionStateUI();
    }

    public void UseItem()
    {
        swanUI.ShowItemUI();
    }

    public void SpecialPower()
    {
        swanUI.ShowSpecialPowerUI();
    }

    public void Defend()
    {
        swanUI.ShowDefendStateUI();
    }

    public void UseBasicAttack()
    {
        swanState.FightState(FightType.BasicState, this);
        fightType = FightType.BasicState;
    }

    public void UseHeavyAttack()
    {
        swanState.FightState(FightType.HeavyState, this);
        fightType = FightType.HeavyState;
    }

    public void UseDefend()
    {
        swanData.defense += 10f;
        swanUI.DefendObjUI.SetActive(false);
    }

    public void OnSuccess()
    {
        switch (fightType)
        {
            case FightType.BasicState:
                Debug.Log("Attacked using Basic Attack is Success!");
                break;
            case FightType.HeavyState:
                Debug.Log("Attacked using Heavy Attack is Success!");
                break;
        }
    }

    public void OnError()
    {
  
    }

    #region Item
    public void HealSwan(BreadCrumbs breadCrumbs)
    {
        swanData.health += breadCrumbs.IncreaseHealth();

        Debug.Log("Your health increased!");
    }

    public void ItemDamage(ShinyFeather shinyFeather)
    {
        swanData.damage += shinyFeather.IncreaseDamage();

        Debug.Log("Your damage increased!");
    }

    public void ItemMana(ClearBlueCrystal clearBlueCrystal)
    {
        swanData.mana += clearBlueCrystal.IncreaseMana();

        Debug.Log("Your mana increased!");
    }

    public void ItemPassive(CaffeinatedDrink caffeinatedDrink)
    {
        swanData.passive += caffeinatedDrink.IncreasePassive();

        Debug.Log("Your passive increased!");
    }

    public void ItemRemoveNegativeStatus()
    {
        if (swanData.negativeStatus.Count <= 0)
            return;

        swanData.negativeStatus.Clear();

        Debug.Log("Your negtive status has been cleared!");
    }
    #endregion
}
