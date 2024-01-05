using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadManager : MonoBehaviour
{
    [SerializeField] List<Bread> breads;
    [field: SerializeField] public float TakeActionDelay {get; set;}
    [field: SerializeField] public float TurnEndDelay {get; set;}
    List<Bread> breadOrders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBreadsTurn()
    {
        breadOrders = new List<Bread>(breads);

        breadOrders[Random.Range(0, breadOrders.Count)].StartTurn();
    }

    void EndBreadsTurn()
    {

    }

    public void EndTurn(Bread bread)
    {
        breadOrders.Remove(bread);

        if(breadOrders.Count > 0)
            breadOrders[Random.Range(0, breadOrders.Count)].StartTurn();
        else
            EndBreadsTurn();
    }
}
