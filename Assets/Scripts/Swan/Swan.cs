using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SwanData
{
    public float health;
    public float damageBoost;
    public float mana;
    public float passiveBoost;
    public float defenseBoost;
    public float basicDamage;
    public float heavyDamage;

    [Header("Negative Status")]
    public List<SpecialPower> negativeStatus = new List<SpecialPower>();
}

public class Swan : MonoBehaviour, IActionState, OnEventHandler, OnBreadHandler
{
    public SwanData swanData;
    [SerializeField] BreadManager breadManager;
    [SerializeField] private SwanManager swanManager;
    private SwanState swanState;
    private SwanUI swanUI;
    private SwanItemChance swanItemChance;

    public float damageIndex;
    private bool hasHeavyDamaged;

    public enum FightType
    {
        BasicState,
        HeavyState,
        SpecialPowerState,
        DefendState
    }

    public FightType fightType {get; set;}

    private void Awake()
    {
        swanState = GetComponent<SwanState>();
        swanUI = GetComponent<SwanUI>();
        swanItemChance = GetComponent<SwanItemChance>();
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

    public void UseDefendBtn()
    {
        swanUI.DefendObjUI.SetActive(false);
        swanUI.ActionStateNoAllAccessible();

        StartCoroutine(UseDefend());
    }

    private IEnumerator UseDefend()
    {
        yield return new WaitForSeconds(1f);

        swanData.defenseBoost += 10f;
        breadManager.StartBreadsTurn();
    }

    public void OnSuccess(Bread bread)
    {
        switch (fightType)
        {
            case FightType.BasicState:
                Debug.Log("Attacked using Basic Attack is Success!");
                swanItemChance.GetBreadCrumbsChance();
                StartCoroutine(BasicAttack(bread));
                break;
            case FightType.HeavyState:
                Debug.Log("Attacked using Heavy Attack is Success!");
                swanItemChance.GetBreadCrumbsChance();
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
                swanItemChance.GetShinyFeatherChance();
                breadManager.EndTurn();
                break;
        }
    }

    public IEnumerator BasicAttack(Bread bread)
    {
        yield return new WaitForSeconds(1f);

        switch (damageIndex)
        {
            case 0:
                bread.ReduceBreadHealth(3f + swanData.damageBoost);
                break;
            case 1:
                bread.ReduceBreadHealth(7f + swanData.damageBoost);
                break;
            case 2:
                bread.ReduceBreadHealth(15f + swanData.damageBoost);
                break;
            case 3:
                bread.ReduceBreadHealth(25f + swanData.damageBoost);
                break;
        }

        yield return new WaitForSeconds(2f);

        swanData.damageBoost = 0;
        bread.selectedArrow.SetActive(false);
        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    public IEnumerator HeavyAttack(Bread bread)
    {
        if(!hasHeavyDamaged)
        {
            if (swanState.sliderValueIncreased < 25f)
            {
                Debug.Log("Heavy Damage: 0f");
                bread.ReduceBreadHealth(0f + swanData.damageBoost);
            }
            else if (swanState.sliderValueIncreased < 50f)
            {
                Debug.Log("Heavy Damage: 10f");
                bread.ReduceBreadHealth(10f + swanData.damageBoost);
            }
            else if (swanState.sliderValueIncreased < 75f)
            {
                Debug.Log("Heavy Damage: 15f");
                bread.ReduceBreadHealth(15f + swanData.damageBoost);
            }
            else if (swanState.sliderValueIncreased >= 75f)
            {
                Debug.Log("Heavy Damage: 20f");
                bread.ReduceBreadHealth(20f + swanData.damageBoost);
            }

            hasHeavyDamaged = true;
        }

        yield return new WaitForSeconds(1f);

        swanData.damageBoost = 0;
        hasHeavyDamaged = false;
        bread.selectedArrow.SetActive(false);
        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    public void OnFailed(int amount)
    {
        switch (fightType)
        {
            case FightType.DefendState:
                Debug.Log("Defending State");
                StartCoroutine(DamageFromEnemy(amount));
                break;
        }
    }

    #region Item
    public void HealSwan(BreadCrumbs breadCrumbs)
    {
        swanData.health += breadCrumbs.IncreaseHealth();
        swanUI.healthBarSlider.value = swanData.health;
        StartCoroutine(StartUseItem());

        breadCrumbs.DoSomething();
        Debug.Log("Your health increased!");
    }

    public void ItemDamage(ShinyFeather shinyFeather)
    {
        swanData.damageBoost += shinyFeather.IncreaseDamage();
        StartCoroutine(StartUseItem());
        shinyFeather.DoSomething();

        Debug.Log("Your damage increased!");
    }

    public void ItemMana(ClearBlueCrystal clearBlueCrystal)
    {
        swanData.mana += clearBlueCrystal.IncreaseMana();
        swanUI.specialPowerBarSlider.value = swanData.mana;
        StartCoroutine(StartUseItem());
        clearBlueCrystal.DoSomething();
        Debug.Log("Your mana increased!");
    }

    public void ItemPassive(CaffeinatedDrink caffeinatedDrink)
    {
        swanData.passiveBoost += caffeinatedDrink.IncreasePassive();
        StartCoroutine(StartUseItem());
        caffeinatedDrink.DoSomething();
        Debug.Log("Your passive increased!");
    }

    public void ItemRemoveNegativeStatus(Milk milk)
    {

        if (swanData.negativeStatus.Count <= 0)
            return;

        StartCoroutine(StartUseItem());

        swanData.negativeStatus.Clear();
        milk.DoSomething();

        Debug.Log("Your negtive status has been cleared!");
    }

    public IEnumerator DamageFromEnemy(int amount)
    {
        yield return new WaitForSeconds(1f);

        switch (amount)
        {
            case 0:
                swanUI.healthBarSlider.value -= (swanData.defenseBoost > 0 ? 15f / swanData.defenseBoost : 15f);
                break;
            case 1:
                swanUI.healthBarSlider.value -= (swanData.defenseBoost > 0 ? 12f / swanData.defenseBoost : 12f);
                break;
            case 2:
                swanUI.healthBarSlider.value -= (swanData.defenseBoost > 0 ? 6f / swanData.defenseBoost : 6f);
                break;
            case 3:
                swanUI.healthBarSlider.value -= 0f;
                break;
        }

        yield return new WaitForSeconds(1f);

        swanData.defenseBoost = 0;
        breadManager.EndTurn();
    }
    #endregion

    public IEnumerator StartUseItem()
    {
        swanUI.ActionStateNoAllAccessible();

        yield return new WaitForSeconds(1f);

        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }
}
