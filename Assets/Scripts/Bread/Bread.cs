using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bread : MonoBehaviour, IActionState
{
    [SerializeField] BreadManager breadManager;
    bool actionFinished;
    public bool Dead {get; private set;}
    public GameObject selectedArrow;
    public Slider breadHealthBarSlider;
    public Slider breadSpecialPowerBarSlider;

    public float breadHealth;

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

    public void ReduceBreadHealth(float damage)
    {
        breadHealth -= damage;
        breadHealthBarSlider.value = breadHealth;
    }

    IEnumerator TakeTurn()
    {
        actionFinished = false;
        
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
