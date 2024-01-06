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
    [SerializeField] private SwanUI swanUI;
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        StartCoroutine(EndBreadsTurn());
    }

    public IEnumerator StartBreadsTurn()
    {
        uiManager.panelCurrentTurnObj.SetActive(true);
        uiManager.currentTextCurrentTurn.text = "Bread's turn to shine. Dodge their hits.";

        yield return new WaitForSeconds(2f);

        swanUI.ActionStateContainer.SetActive(false);
        uiManager.panelCurrentTurnObj.SetActive(false);
        uiManager.currentTextCurrentTurn.text = "";

        Debug.Log("bread's turn starting");
        breadOrders = new List<Bread>(breads);
        breadOrders.RemoveAll(x => x.Dead);

        currentBread = breadOrders[Random.Range(0, breadOrders.Count)];
        // currentBread.selectedArrow.SetActive(true);
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

    public IEnumerator EndBreadsTurn()
    {
        uiManager.panelCurrentTurnObj.SetActive(true);
        uiManager.currentTextCurrentTurn.text = "Swan's turn to shine.";

        yield return new WaitForSeconds(1.5f);

        uiManager.panelCurrentTurnObj.SetActive(false);
        uiManager.currentTextCurrentTurn.text = "";

        swanUI.ActionStateNoAllAccessible();
        Debug.Log("bread's turn end");
    }

    public void EndTurn()
    {
        // currentBread.selectedArrow.SetActive(false);
        breadOrders.Remove(currentBread);

        SetTransparency(1);

        if(breadOrders.Count > 0)
        {
            currentBread = breadOrders[Random.Range(0, breadOrders.Count)];
            Debug.Log(currentBread.name + " starting turn");
            // currentBread.selectedArrow.SetActive(true);
            currentBread.StartTurn();
        }
        else
            StartCoroutine(InitEndBreadsTurn());
    }

    private IEnumerator InitEndBreadsTurn()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(EndBreadsTurn());
    }
}
