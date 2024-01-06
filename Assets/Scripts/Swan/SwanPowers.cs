using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanPowers : MonoBehaviour, OnBreadHandler, OnEventHandler
{
    private SwanState swanState;
    private SwanUI swanUI;
    private SwanItemChance swanItemChance;
    [SerializeField] private BreadManager breadManager;

    [Header("Data")]
    private string currentSpecialPower;

    [Header("Special Power")]
    public SwanLeap swanLeap;
    public GroundPummel groundPummel;

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
    }

    public void UseSwanLeap()
    {
        swanState.FightState(Swan.FightType.SpecialPowerState, this);
        specialPowerType = SpecialPowerType.SwanLeap;
        swanUI.ActionStateNoAllAccessible();
        swanUI.ActionStateButtonUninteractable();
    }

    public void UseGroundPummel()
    {
        swanState.FightState(Swan.FightType.SpecialPowerState, this);
        specialPowerType = SpecialPowerType.GroundPummel;
        swanUI.ActionStateNoAllAccessible();
        swanUI.ActionStateButtonUninteractable();
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
        UIManager.Instance.currentTextCurrentTurn.text = "Swan used Swan Leap!";

        yield return new WaitForSeconds(2f);
        UIManager.Instance.panelCurrentTurnObj.SetActive(false);
        UIManager.Instance.currentTextCurrentTurn.text = "";

        swanUI.specialPowerBarSlider.value -= 10f;
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

        swanUI.specialPowerBarSlider.value -= 20f;
        bread.ReduceBreadHealth(30f);
        swanItemChance.GetCaffeinatedDrinkOrMilkChance();

        yield return new WaitForSeconds(1f);

        breadManager.StartCoroutine(breadManager.StartBreadsTurn());

        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    public void OnFailed(int amount)
    {
        
    }

    public void OnSuccess()
    {
        throw new System.NotImplementedException();
    }
}
