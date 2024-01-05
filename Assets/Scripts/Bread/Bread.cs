using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bread : MonoBehaviour, IActionState
{
    [SerializeField] BreadManager breadManager;
    bool actionFinished;
    public bool Dead {get; private set;}

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

    public void StartTurn()
    {
        StartCoroutine(nameof(TakeTurn));
    }

    public void EndTurn()
    {
        actionFinished = true;
    }

    IEnumerator TakeTurn()
    {
        actionFinished = false;
        yield return new WaitForSeconds(breadManager.TakeActionDelay);

        ChooseAction();
        
        while(!actionFinished)
            yield return null;

        yield return new WaitForSeconds(breadManager.TurnEndDelay);

        breadManager.EndTurn(this);
    }

    void ChooseAction()
    {
        switch(Random.Range(0, 4))
        {
            case 0: Fight();
                break;
            case 1: SpecialPower();
                break;
            case 2: UseItem();
                break;
            case 3: Defend();
                break;
            
            default: break;
        }
    }
}
