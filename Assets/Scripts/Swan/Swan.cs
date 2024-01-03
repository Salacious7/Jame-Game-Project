using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SwanData
{
    public float health;
    public float defense;
}

public class Swan : MonoBehaviour, IActionState
{
    [SerializeField] private SwanData swanData;
    [SerializeField] private Controller controller;
    private SwanUI swanUI;

    private void Awake()
    {
        swanUI = GetComponent<SwanUI>();
    }

    public void Start()
    {
        controller.Swan = this;
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
        throw new System.NotImplementedException();
    }
}
