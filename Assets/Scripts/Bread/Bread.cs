using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bread : MonoBehaviour, IActionState
{
    [SerializeField] protected BreadManager breadManager;
    [field: SerializeField] public Transform AttackPosition {get; private set;}
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI manaText;
    [SerializeField] float basicAttackDamage;
    [SerializeField] float heavyAttackDamage;
    [SerializeField] protected float specialAttack1Damage;
    [SerializeField] protected float specialAttack2Damage;
    [SerializeField] protected float special1Cost;
    [SerializeField] protected float special2Cost;
    bool actionFinished;
    public bool Dead {get; private set;}
    public GameObject selectedArrow;
    public Slider breadHealthBarSlider;
    public Slider breadSpecialPowerBarSlider;

    [field: SerializeField] public float breadHealth {get; private set;}
    [field: SerializeField] public float breadMana {get; private set;}
    protected float DamageFromCurrentAttack {get; set;}

    public float maxHealth {get; private set;}
    public float maxMana {get; private set;}

    public enum FightType
    {
        BasicState,
        HeavyState,
    }

    public FightType fightType;

    void Awake()
    {
        maxHealth = breadHealth;
        maxMana = breadMana;

        UpdateHealthUI(breadHealth);
        UpdateManaUI(breadMana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Heal(float amount)
    {
        breadHealth += amount;

        if(breadHealth >= maxHealth)
            breadHealth = maxHealth;

        OnHeal();

        UpdateHealthUI(breadHealth);
    }

    public abstract void OnHeal();

    public void RestoreMana(float amount)
    {
        breadMana += amount;

        if(breadMana >= maxMana)
            breadMana = maxMana;

        UpdateManaUI(breadMana);
    }

    public void UseMana(float amount)
    {
        breadMana -= amount;
        UpdateHealthUI(breadMana);

        UpdateManaUI(breadMana);
    }

    public void Defend()
    {
        
    }

    public void Fight()
    {
        Attack();

        if (Random.Range(0, 2) == 0)
        {
            OnAttack(FightType.BasicState);
            Debug.Log(name + " used basic attack");
            DamageFromCurrentAttack = basicAttackDamage;
            if(GameObject.FindWithTag("Swan").TryGetComponent(out Swan swan))
                swan.IncomingDamage = DamageFromCurrentAttack;
            //trigger animation
        }
        else
        {
            OnAttack(FightType.HeavyState);
            Debug.Log(name + " used heavy attack");
            DamageFromCurrentAttack = heavyAttackDamage;
            if(GameObject.FindWithTag("Swan").TryGetComponent(out Swan swan))
                swan.IncomingDamage = DamageFromCurrentAttack;
            //trigger animation
        }
    }

    public abstract void OnAttack(FightType fightType);

    protected void Attack()
    {
        if (GameObject.FindWithTag("Swan").TryGetComponent(out SwanState swanState))
        {
            var swan = GameObject.FindWithTag("Swan").GetComponent<Swan>();
            swan.fightType = Swan.FightType.DefendState;
            swanState.FightState(Swan.FightType.DefendState, swan);
        }
        else
        {
            Debug.LogError("Could not find swan!");
        }
    }

    public abstract void SpecialPower();

    public void UseItem()
    {
        
    }

    public void StartTurn()
    {
        StartCoroutine(nameof(TakeTurn));
    }

    public void EndTurn()
    {
        actionFinished = true;
    }

    public void ReduceBreadHealth(float damage)
    {
        breadHealth -= damage;
        breadHealthBarSlider.value = breadHealth;
        UpdateHealthUI(breadHealth);

        OnDeath();

        if (breadHealth <= 0)
        {
            Dead = true;
        }
    }


    public abstract void OnDeath();

    public void UpdateHealthUI(float value)
    {
        if(value <= 0)
        {
            healthText.text = "000";
            healthText.ForceMeshUpdate();
            return;
        }

        string s = "";

        if(value < 100)
            s = "0";
        else if(value < 10)
            s = "00";

        healthText.text = s + value.ToString("0");
        healthText.ForceMeshUpdate();
    }

    public void UpdateManaUI(float value)
    {
        if(value <= 0)
        {
            manaText.text = "000";
            manaText.ForceMeshUpdate();
            return;
        }

        string s = "";

        if(value < 100)
            s = "0";
        else if(value < 10)
            s = "00";

        manaText.text = s + value.ToString("0");
        manaText.ForceMeshUpdate();
    }

    IEnumerator TakeTurn()
    {
        actionFinished = false;
        selectedArrow.SetActive(false);
        yield return new WaitForSeconds(breadManager.TakeActionDelay);
        breadManager.SetTransparency(breadManager.unfocusedTransparency);
        Debug.Log(name + " taking action");

        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = name + " is taking action!";

        yield return new WaitForSeconds(2f);

        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        ChooseAction();
        
        // while(!actionFinished)
        //     yield return null;

        yield return new WaitUntil(() => actionFinished);

        yield return new WaitForSeconds(breadManager.TurnEndDelay);
        Debug.Log(name + " end turn");
        breadManager.SetTransparency(1);

        breadManager.EndTurn();
    }

    void ChooseAction()
    {
        switch(Random.Range(0, 2))
        {
            case 0: Fight();
                break;
            case 1: SpecialPower();
                break;
            // case 2: UseItem();
            //     break;
            // case 3: Defend();
            //     break;
            
            default: break;
        }
    }
}
