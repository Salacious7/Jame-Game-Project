using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bread : MonoBehaviour, IActionState
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Defend()
    {
        throw new System.NotImplementedException();
    }

    public void Fight()
    {
        throw new System.NotImplementedException();
    }

    public abstract void SpecialPower();

    public void UseItem()
    {
        throw new System.NotImplementedException();
    }
}
