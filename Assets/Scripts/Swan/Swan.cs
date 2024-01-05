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
    [SerializeField] BreadManager breadManager;
    [SerializeField] private SwanManager swanManager;
    private SwanState swanState;
    private SwanUI swanUI;


    public enum FightType
    {
        BasicState,
        HeavyState,
        DefendState
    }

    public FightType fightType {get; set;}

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
        swanUI.ActionStateNoAllAccessible();
    }

    public void UseHeavyAttack()
    {
        swanState.FightState(FightType.HeavyState, this);
        fightType = FightType.HeavyState;
        swanUI.ActionStateNoAllAccessible();
    }

    public void UseDefend()
    {
        swanData.defense += 10f;
        swanUI.DefendObjUI.SetActive(false);
        swanUI.ActionStateNoAllAccessible();
    }

    public void OnSuccess(Bread bread)
    {
        switch (fightType)
        {
            case FightType.BasicState:
                Debug.Log("Attacked using Basic Attack is Success!");
                StartCoroutine(BasicAttack(bread));
                break;
            case FightType.HeavyState:
                Debug.Log("Attacked using Heavy Attack is Success!");
                StartCoroutine(HeavyAttack(bread));
                break;
        }
    }

    public void OnSuccess()
    {
        switch (fightType)
        {
            case FightType.DefendState:
                Debug.Log("Defended attack!");
                breadManager.EndTurn();
                break;
        }
    }

    public IEnumerator BasicAttack(Bread bread)
    {
        yield return new WaitForSeconds(1f);

        swanManager.breads[swanManager.selectedCharacterIndex].selectedArrow.SetActive(false);
        breadManager.StartBreadsTurn();
    }

    public IEnumerator HeavyAttack(Bread bread)
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

        yield return new WaitForSeconds(1f);
    }

    public void OnFailed(int amount)
    {
        switch (fightType)
        {
            case FightType.BasicState:
                Debug.Log("Attacked using Basic Attack is Success!");
                StartCoroutine(BasicFailedAttack(amount));
                break;
        }
    }

    public IEnumerator BasicFailedAttack(int amount)
    {
        yield return new WaitForSeconds(1f);

        switch(amount)
        {
            case 0:
                Debug.Log("Damage only 1");
                break;
            case 1:
                Debug.Log("Damage only 2");
                break;
            case 2:
                Debug.Log("Damage only 3");
                break;
            case 3:
                Debug.Log("Damage only 4");
                break;
        }    

    }

    #region Item
    public void HealSwan(BreadCrumbs breadCrumbs)
    {
        swanData.health += breadCrumbs.IncreaseHealth();
        swanUI.ActionStateNoAllAccessible();

        Debug.Log("Your health increased!");
    }

    public void ItemDamage(ShinyFeather shinyFeather)
    {
        swanData.damage += shinyFeather.IncreaseDamage();
        swanUI.ActionStateNoAllAccessible();

        Debug.Log("Your damage increased!");
    }

    public void ItemMana(ClearBlueCrystal clearBlueCrystal)
    {
        swanData.mana += clearBlueCrystal.IncreaseMana();
        swanUI.ActionStateNoAllAccessible();

        Debug.Log("Your mana increased!");
    }

    public void ItemPassive(CaffeinatedDrink caffeinatedDrink)
    {
        swanData.passive += caffeinatedDrink.IncreasePassive();
        swanUI.ActionStateNoAllAccessible();

        Debug.Log("Your passive increased!");
    }

    public void ItemRemoveNegativeStatus()
    {
        if (swanData.negativeStatus.Count <= 0)
            return;

        swanUI.ActionStateNoAllAccessible();
        swanData.negativeStatus.Clear();

        Debug.Log("Your negtive status has been cleared!");
    }

    public void OnFailed()
    {
        throw new NotImplementedException();
    }
    #endregion
}
