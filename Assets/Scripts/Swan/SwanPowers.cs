using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanPowers : MonoBehaviour, OnEventHandler
{
    private SwanState swanState;

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
    }

    public void UseSwanLeap()
    {
        swanState.FightState(Swan.FightType.BasicState, this);
        specialPowerType = SpecialPowerType.SwanLeap;
    }

    public void UseGroundPummel()
    {
        swanState.FightState(Swan.FightType.BasicState, this);
        specialPowerType = SpecialPowerType.GroundPummel;
    }

    public void OnSuccess()
    {
        switch(specialPowerType)
        {
            case SpecialPowerType.SwanLeap:
                Debug.Log("Attacked using SwanLeap is Success!");
                break;
            case SpecialPowerType.GroundPummel:
                Debug.Log("Attacked using GroundPummel is Success!");
                break;
        }
    }

    public void OnError()
    {
        
    }
}
