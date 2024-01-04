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

    public List<NegativeStatus> negativeStatuses = new List<NegativeStatus>();
}

public class Swan : MonoBehaviour, IActionState
{
    [SerializeField] private SwanData swanData;
    private SwanUI swanUI;

    private void Awake()
    {
        swanUI = GetComponent<SwanUI>();

        BreadCrumbs.onEntityHeal += HealSwan;
        ShinyFeather.onIncreaseDamage += ItemDamage;
        ClearBlueCrystal.onEntityMana += ItemMana;
        CaffeinatedDrink.onEntityPassive += ItemPassive;
        Milk.onEntityRemoveNegativeStatus += ItemRemoveNegativeStatus;
    }

    public void Defend()
    {
        throw new System.NotImplementedException();
    }

    public void Fight()
    {
        swanUI.ActionStateUI();
    }

    public void SpecialPower()
    {
        throw new System.NotImplementedException();
    }

    public void UseItem()
    {
        swanUI.ShowItemUI();
    }

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
        if (swanData.negativeStatuses.Count == 0)
            return;

        swanData.negativeStatuses.Clear();

        Debug.Log("Your negtive status has been cleared!");
    }
}
