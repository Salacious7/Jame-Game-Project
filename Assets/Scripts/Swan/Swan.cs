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

public class Swan : MonoBehaviour, IActionState, OnEventHandler, OnBreadHandler
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
        swanState.FightState(FightType.BasicState);
        fightType = FightType.BasicState;
    }

    public void UseHeavyAttack()
    {
        swanState.FightState(FightType.HeavyState);
        fightType = FightType.HeavyState;
    }

    public void UseDefend()
    {
        swanData.defense += 10f;
        swanUI.DefendObjUI.SetActive(false);
    }

    public void OnSuccess(Bread bread)
    {
        switch (fightType)
        {
            case FightType.BasicState:
                Debug.Log("Attacked using Basic Attack is Success!");
                BasicAttack(bread);
                break;
            case FightType.HeavyState:
                Debug.Log("Attacked using Heavy Attack is Success!");
                HeavyAttack(bread);
                break;
        }
    }

    public void OnSuccess()
    {

    }

    public void BasicAttack(Bread bread)
    {
        Debug.Log("Damage hit to " + bread.name);
    }

    public void HeavyAttack(Bread bread)
    {

        if (swanUI.heavyDataSlider.value < 25f)
        {
            Debug.Log("Current damage: " + 5f);
        }
        else if (swanUI.heavyDataSlider.value <= 50f)
        {
            Debug.Log("Current damage: " + 10f);
        }
        else if (swanUI.heavyDataSlider.value <= 75f)
        {
            Debug.Log("Current damage: " + 15f);
        }
        else if (swanUI.heavyDataSlider.value > 75f)
        {
            Debug.Log("Current damage: " + 20f);
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
