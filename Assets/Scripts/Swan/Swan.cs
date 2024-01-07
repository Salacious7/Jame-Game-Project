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
    [SerializeField] private GameManager gameManager;
    [SerializeField] Transform startingPosition;
    private SwanState swanState;
    private SwanUI swanUI;
    private SwanItemChance swanItemChance;

    public float damageIndex;
    private bool hasHeavyDamaged;
    [SerializeField] private float setWaitTimer;
    private float getWaitTimer;
    [SerializeField] private float setDamageTimer;
    private float getDamageTimer;
    private Bread target;

    public float IncomingDamage {get; set;}

    [Header("Components")]
    private Animator anim;
    private SpriteRenderer swanSprite;

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
        swanSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        getDamageTimer = setDamageTimer;
        getWaitTimer = setWaitTimer;
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
        StartCoroutine(InitBasicAttack());
    }

    public IEnumerator InitBasicAttack()
    {
        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Select enemy to Attack";
        swanUI.ActionStateNoAllAccessible();
        swanUI.ActionStateButtonUninteractable();

        getWaitTimer -= Time.deltaTime;

        yield return new WaitForSeconds(getWaitTimer);

        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        getWaitTimer = setWaitTimer;
        swanState.FightState(FightType.BasicState, this);
        fightType = FightType.BasicState;
    }

    public void UseHeavyAttack()
    {
        StartCoroutine(InitHeavyAttack());
    }

    public IEnumerator InitHeavyAttack()
    {
        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Select enemy to Attack";
        swanUI.ActionStateNoAllAccessible();
        swanUI.ActionStateButtonUninteractable();

        getWaitTimer -= Time.deltaTime;

        yield return new WaitForSeconds(getWaitTimer);

        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        getWaitTimer = setWaitTimer;
        swanState.FightState(FightType.HeavyState, this);
        fightType = FightType.HeavyState;
    }

    public void UseDefendBtn()
    {
        StartCoroutine(UseDefend());
    }

    private IEnumerator UseDefend()
    {
        swanUI.ActionStateNoAllAccessible();
        swanUI.ActionStateButtonUninteractable();

        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Defense increased!";

        yield return new WaitForSeconds(2f);

        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        swanData.defenseBoost += 2f;

        yield return new WaitForSeconds(1f);
        
        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
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
                anim.SetTrigger("isBlock");
                swanItemChance.GetShinyFeatherChance();
                breadManager.EndTurn();
                break;   
        }
    }

    public IEnumerator BasicAttack(Bread bread)
    {
        swanUI.ActionStateNoAllAccessible();
        swanUI.ActionStateButtonUninteractable();
        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Basic Attack!";
        target = bread;

        yield return new WaitForSeconds(0f);

        anim.SetTrigger("basicAttack");

        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        switch (damageIndex)
        {
            case 1:
                bread.ReduceBreadHealth(3f + swanData.damageBoost);
                break;
            case 2:
                bread.ReduceBreadHealth(7f + swanData.damageBoost);
                break;
            case 3:
                bread.ReduceBreadHealth(15f + swanData.damageBoost);
                break;
            case 4:
                bread.ReduceBreadHealth(25f + swanData.damageBoost);
                break;
        }

        yield return new WaitForSeconds(1f);

        swanData.damageBoost = 0;
        bread.selectedArrow.SetActive(false);
        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    public IEnumerator HeavyAttack(Bread bread)
    {
        swanUI.ActionStateNoAllAccessible();
        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Heavy Attack!";
        target = bread;

        anim.SetTrigger("heavyAttack");

        yield return new WaitForSeconds(1f);

        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        if (!hasHeavyDamaged)
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
            swanState.sliderValueIncreased = 0f;
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

    public IEnumerator DamageFromEnemy(int amount)
    {
        yield return new WaitForSeconds(1f);

        switch (amount)
        {
            case 0:
                swanData.health -= swanData.defenseBoost > 0 ? IncomingDamage / swanData.defenseBoost : IncomingDamage;
                break;
            case 1:
                swanData.health -= swanData.defenseBoost > 0 ? IncomingDamage * 0.75f / swanData.defenseBoost : IncomingDamage;
                break;
            case 2:
                swanData.health -= swanData.defenseBoost > 0 ? IncomingDamage * 0.5f / swanData.defenseBoost : IncomingDamage;
                break;
            case 3:
                swanData.health -= swanData.defenseBoost > 0 ? IncomingDamage * 0.25f / swanData.defenseBoost : IncomingDamage;
                break;
        }

        anim.SetTrigger("isDamage");

        UpdateHealthUI(swanData.health);

        yield return new WaitForSeconds(2f);

        swanSprite.color = new Color(255f, 255f, 255f, 255f);
        swanData.defenseBoost = 0;

        if (swanData.health > 0)
        {
            breadManager.EndTurn();
        }
        else
        {
            anim.SetTrigger("isBlock");
            gameManager.InitializeLoseScreen();
        }

    }

    public void UpdateHealthUI(float value)
    {
        swanUI.healthBarSlider.value = value;

        if(value <= 0)
        {
            swanUI.healthText.text = "000";
            swanUI.healthText.ForceMeshUpdate();
            return;
        }

        string s = "";

        if(value < 100)
            s = "0";
        else if(value < 10)
            s = "00";

        swanUI.healthText.text = s + value.ToString("0");
        swanUI.healthText.ForceMeshUpdate();
    }

    public void UpdateManaUI(float value)
    {
        swanUI.specialPowerBarSlider.value = value;

        if(value <= 0)
        {
            swanUI.manaText.text = "000";
            swanUI.manaText.ForceMeshUpdate();
            return;
        }

        string s = "";

        if(value < 100)
            s = "0";
        else if(value < 10)
            s = "00";

        swanUI.manaText.text = s + value.ToString("0");
        swanUI.manaText.ForceMeshUpdate();
    }


    #region Item
    public void HealSwan(BreadCrumbs breadCrumbs)
    {
        StartCoroutine(StartHealSwan(breadCrumbs));
    }

    public IEnumerator StartHealSwan(BreadCrumbs breadCrumbs)
    {
        swanUI.ActionStateNoAllAccessible();
        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Swan used Bread Crumbs Item!";

        yield return new WaitForSeconds(2f);

        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        swanData.health += breadCrumbs.IncreaseHealth();
        UpdateHealthUI(swanData.health);
        breadCrumbs.DoSomething();
        Debug.Log("Your health increased!");

        yield return new WaitForSeconds(1f);
        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    public void ItemDamage(ShinyFeather shinyFeather)
    {
        StartCoroutine(StartItemDamage(shinyFeather));
    }

    public IEnumerator StartItemDamage(ShinyFeather shinyFeather)
    {
        swanUI.ActionStateNoAllAccessible();
        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Swan used Shiny Feather Item!";

        yield return new WaitForSeconds(2f);
        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        swanData.damageBoost += shinyFeather.IncreaseDamage();
        shinyFeather.DoSomething();

        Debug.Log("Your damage increased!");

        yield return new WaitForSeconds(1f);

        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    public void ItemMana(ClearBlueCrystal clearBlueCrystal)
    {
        StartCoroutine(StartItemMana(clearBlueCrystal));
    }

    public IEnumerator StartItemMana(ClearBlueCrystal clearBlueCrystal)
    {
        swanUI.ActionStateNoAllAccessible();
        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Swan used Clear Blue Crystal Item!";

        yield return new WaitForSeconds(2f);
        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";


        swanData.mana += clearBlueCrystal.IncreaseMana();
        UpdateManaUI(swanData.mana);
        swanUI.specialPowerBarSlider.value = swanData.mana;
        clearBlueCrystal.DoSomething();
        Debug.Log("Your mana increased!");

        yield return new WaitForSeconds(1f);
        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    public void ItemPassive(CaffeinatedDrink caffeinatedDrink)
    {
        StartCoroutine(StartItemPassive(caffeinatedDrink));
    }

    public IEnumerator StartItemPassive(CaffeinatedDrink caffeinatedDrink)
    {
        swanUI.ActionStateNoAllAccessible();
        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Swan used Caffeinated Drink Item!";

        yield return new WaitForSeconds(2f);
        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        swanData.passiveBoost += caffeinatedDrink.IncreasePassive();
        caffeinatedDrink.DoSomething();
        Debug.Log("Your passive increased!");

        yield return new WaitForSeconds(1f);
        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    public void ItemRemoveNegativeStatus(Milk milk)
    {
        StartCoroutine(StartItemRemoveNegativeStatus(milk));
    }

    public IEnumerator StartItemRemoveNegativeStatus(Milk milk)
    {
        swanUI.ActionStateNoAllAccessible();
        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Swan used Milk Item!";

        yield return new WaitForSeconds(2f);
        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        swanData.negativeStatus.Clear();
        milk.DoSomething();

        Debug.Log("Your negtive status has been cleared!");

        yield return new WaitForSeconds(1f);
        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    public void OnFailed()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Attack Movement

    public void MoveToBread()
    {
        StartCoroutine(nameof(MoveToBreadCo));
    }

    IEnumerator MoveToBreadCo()
    {
        float timer = 0;

        float duration = 0.25f;
        var startingPos = transform.position;
        while(timer < duration)
        {
            transform.position = Vector3.Lerp(startingPos, target.AttackPosition.position, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void MoveToStartingPosition()
    {
        StartCoroutine(nameof(MoveToStartingPositionCo));
    }

    IEnumerator MoveToStartingPositionCo()
    {
        float timer = 0;

        float duration = 0.25f;
        var startingPos = target.AttackPosition.position;
        while(timer < duration)
        {
            transform.position = Vector3.Lerp(target.AttackPosition.position, startingPosition.position, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    #endregion
}
