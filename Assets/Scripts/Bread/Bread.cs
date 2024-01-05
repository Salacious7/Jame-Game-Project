using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Bread : MonoBehaviour, IActionState
{
    [SerializeField] BreadManager breadManager;
    bool actionFinished;
    public bool Dead {get; private set;}
    public GameObject selectedArrow;

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
        
    }

    public void Fight()
    {
        
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

    IEnumerator TakeTurn()
    {
        actionFinished = false;
        
        yield return new WaitForSeconds(breadManager.TakeActionDelay);
        breadManager.SetTransparency(breadManager.unfocusedTransparency);
        Debug.Log(name + " taking action");
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
        if(GameObject.FindWithTag("Swan").TryGetComponent(out SwanState swanState))
        {
            var swan = GameObject.FindWithTag("Swan").GetComponent<Swan>();
            swan.fightType = Swan.FightType.DefendState;
            swanState.FightState(Swan.FightType.DefendState, swan);
        }
        // switch(Random.Range(0, 4))
        // {
        //     case 0: Fight();
        //         break;
        //     case 1: SpecialPower();
        //         break;
        //     case 2: UseItem();
        //         break;
        //     case 3: Defend();
        //         break;
            
        //     default: break;
        // }
    }
}
