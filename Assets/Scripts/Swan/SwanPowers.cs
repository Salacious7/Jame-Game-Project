using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanPowers : MonoBehaviour, OnBreadHandler, OnEventHandler
{
    private Swan swan;
    private SwanState swanState;
    private SwanUI swanUI;
    private SwanItemChance swanItemChance;
    [SerializeField] private BreadManager breadManager;

    [Header("Data")]
    private string currentSpecialPower;
    [SerializeField] private float setWaitTimer;
    private float getWaitTimer;

    [Header("Special Power")]
    public SwanLeap swanLeap;
    public GroundPummel groundPummel;

    [Header("Components")]
    private Animator anim;

    private enum SpecialPowerType
    {
        SwanLeap,
        GroundPummel
    }

    SpecialPowerType specialPowerType;

    private void Awake()
    {
        swanState = GetComponent<SwanState>();
        swanUI = GetComponent<SwanUI>();
        swanItemChance = GetComponent<SwanItemChance>();
        swan = GetComponent<Swan>();
        anim = GetComponent<Animator>();
    }

    public void UseSwanLeap()
    {
        swanUI.ActionStateContainer.SetActive(false);
        swanUI.ActionStateNoAllAccessible();
        swanUI.ActionStateButtonUninteractable();

        swanState.FightState(Swan.FightType.SpecialPowerState, this);
        specialPowerType = SpecialPowerType.SwanLeap;
    }

    public void UseGroundPummel()
    {
        swanUI.ActionStateContainer.SetActive(false);
        swanUI.ActionStateNoAllAccessible();
        swanUI.ActionStateButtonUninteractable();

        swanState.FightState(Swan.FightType.SpecialPowerState, this);
        specialPowerType = SpecialPowerType.GroundPummel;
    }

    public void OnSuccess(Bread bread)
    {
        switch(specialPowerType)
        {
            case SpecialPowerType.SwanLeap:
                Debug.Log("Attacked using SwanLeap is Success!");
                StartCoroutine(SwanLeap(bread));
                
                break;
            case SpecialPowerType.GroundPummel:
                Debug.Log("Attacked using GroundPummel is Success!");
                StartCoroutine(GroundPummel(bread));
                break;
        }
    }

    private IEnumerator SwanLeap(Bread bread)
    {
        swanUI.ActionStateNoAllAccessible();
        swanUI.ActionStateButtonUninteractable();

        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "Swan used Swan Scream!";

        yield return new WaitForSeconds(2f);

        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        SoundManager.Instance.OnPlaySwanSkillOne();

        anim.SetTrigger("specialScream");

        swan.swanData.mana -= 10f;
        swanUI.manaText.text = swan.swanData.mana.ToString();

        bread.ReduceBreadHealth(20f);
        swanItemChance.GetClearBlueCrystalChance();

        yield return new WaitForSeconds(1f);

        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    private IEnumerator GroundPummel(Bread bread)
    {
        swanUI.ActionStateButtonUninteractable();
        swanUI.ActionStateNoAllAccessible();

        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "Swan used Ground Pummel!";

        yield return new WaitForSeconds(2f);
        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";


        SoundManager.Instance.OnPlaySwanSkillTwo();
        anim.SetTrigger("specialStomp");
        swan.swanData.mana -= 20f;
        swanUI.manaText.text = swan.swanData.mana.ToString();

        bread.ReduceBreadHealth(30f);
        swanItemChance.GetCaffeinatedDrinkOrMilkChance();

        yield return new WaitForSeconds(1f);

        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    private IEnumerator FailedSpecialPower()
    {
        swanUI.ActionStateButtonUninteractable();
        swanUI.ActionStateNoAllAccessible();

        UIManager.Instance.panelCurrentTurnObj.SetActive(true);
        UIManager.Instance.currentTextCurrentTurn.text = "Failed to Special Power!";

        yield return new WaitForSeconds(2f);
        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        yield return new WaitForSeconds(1f);

        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    public void OnSuccess()
    {

    }

    public void OnFailed(int index)
    {

    }

    public void OnFailed()
    {
        StartCoroutine(FailedSpecialPower());
    }
}
