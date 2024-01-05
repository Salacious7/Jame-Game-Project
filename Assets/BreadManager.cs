using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadManager : MonoBehaviour
{
    [SerializeField] List<Bread> breads;
    [SerializeField, Range(0, 1f)] public float unfocusedTransparency;
    [field: SerializeField] public float TakeActionDelay {get; set;}
    [field: SerializeField] public float TurnEndDelay {get; set;}
    List<Bread> breadOrders;
    Bread currentBread;

    // Start is called before the first frame update
    void Start()
    {
        StartBreadsTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBreadsTurn()
    {
        Debug.Log("bread's turn starting");
        breadOrders = new List<Bread>(breads);
        breadOrders.RemoveAll(x => x.Dead);

        currentBread = breadOrders[Random.Range(0, breadOrders.Count)];
        Debug.Log(currentBread.name + " starting turn");
        currentBread.StartTurn();
    }

    public void SetTransparency(float value)
    {
        foreach(var b in breads)
            if(b != currentBread && b.TryGetComponent(out SpriteRenderer renderer))
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, value);
    }

    public void FinishAction()
    {
        currentBread.EndTurn();
    }

    void EndBreadsTurn()
    {
        //switch over to swans turn
        Debug.Log("bread's turn end");
    }

    public void EndTurn()
    {
        breadOrders.Remove(currentBread);

        SetTransparency(1);

        if(breadOrders.Count > 0)
        {
            currentBread = breadOrders[Random.Range(0, breadOrders.Count)];
            Debug.Log(currentBread.name + " starting turn");
            currentBread.StartTurn();
        }
        else
            EndBreadsTurn();
    }
}
