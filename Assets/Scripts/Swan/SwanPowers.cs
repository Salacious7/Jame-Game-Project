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
    }

    public void UseGroundPummel()
    {
        swanState.FightState(Swan.FightType.SpecialPowerState, this);
        specialPowerType = SpecialPowerType.GroundPummel;
        swanUI.ActionStateNoAllAccessible();
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
        yield return new WaitForSeconds(1f);
        swanUI.specialPowerBarSlider.value -= 10f;
        bread.ReduceBreadHealth(20f);
        swanItemChance.GetClearBlueCrystalChance();
        breadManager.StartCoroutine(breadManager.StartBreadsTurn());
    }

    private IEnumerator GroundPummel(Bread bread)
    {
        swanUI.ActionStateNoAllAccessible();
        yield return new WaitForSeconds(1f);
        swanUI.specialPowerBarSlider.value -= 20f;
        bread.ReduceBreadHealth(30f);
        swanItemChance.GetCaffeinatedDrinkOrMilkChance();
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
