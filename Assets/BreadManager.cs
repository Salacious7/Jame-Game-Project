using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BreadManager : MonoBehaviour
{
    public static BreadManager Instance { get; set; }

    [SerializeField] private GameManager gameManager;
    public List<Bread> breads;
    [SerializeField, Range(0, 1f)] public float unfocusedTransparency;
    [field: SerializeField] public float TakeActionDelay {get; set;}
    [field: SerializeField] public float TurnEndDelay {get; set;}
    public List<Bread> breadOrders {get; private set;}
    Bread currentBread;
    [SerializeField] private SwanUI swanUI;
    [SerializeField] private UIManager uiManager;
    bool winCondition;

    [SerializeField] private Animator transitionAnim;
    [SerializeField] private Image transitionImage;


    private bool isGameEnd;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(StartInitEndBreadsTurn());
    }

    void Update()
    {
        
        if (breads.All(x => x.Dead) && !isGameEnd)
        {
            StartCoroutine(End());
            isGameEnd = true;
        }
    }

    public IEnumerator End()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.OnPlayWin();
        yield return new WaitForSeconds(2f);
        transitionImage.raycastTarget = true;
        transitionAnim.SetTrigger("isTransition");
        yield return new WaitForSeconds(1.5f);
        transitionAnim.SetTrigger("isEndTransition");
        gameManager.InitializeWinScreen();
        SoundManager.Instance.OnPlayEndCutscene();
        yield return new WaitForSeconds(13f);
        UIManager.Instance.OnBackToMenuBtn();
    }

    public IEnumerator StartBreadsTurn()
    {
        foreach (Bread bread in breads)
        {
            bread.OnArrowSelectionUIState(false);
        }

        if (breads.All(x => x.Dead))
            yield break;

        swanUI.ActionStateContainer.SetActive(false);
        uiManager.panelCurrentTurnObj.SetActive(true);
        CameraManager.Instance.StartCoroutine(CameraManager.Instance.CamLookBread());
        uiManager.currentTextCurrentTurn.text = "Bread's turn to shine";

        yield return new WaitForSeconds(2f);

        CameraManager.Instance.StartCoroutine(CameraManager.Instance.CamBackToInitialPos());

        Debug.Log("bread's turn starting");
        breadOrders = new List<Bread>(breads);
        breadOrders.RemoveAll(x => x.Dead);

        currentBread = breadOrders[Random.Range(0, breadOrders.Count)];
        // currentBread.selectedArrow.SetActive(true);
        Debug.Log(currentBread.name + " starting turn");
        swanUI.ActionStateButtonInteractable();

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
        swanUI.ActionStateButtonInteractable();
        uiManager.panelCurrentTurnObj.SetActive(true);
        uiManager.currentTextCurrentTurn.text = "Swan's turn to shine";
        CameraManager.Instance.StartCoroutine(CameraManager.Instance.CamLookSwan());

        yield return new WaitForSeconds(2f);
        swanUI.ActionStateContainer.SetActive(true);

        uiManager.panelCurrentTurnObj.SetActive(false);
        uiManager.currentTextCurrentTurn.text = "";

        CameraManager.Instance.StartCoroutine(CameraManager.Instance.CamBackToInitialPos());
        Debug.Log("bread's turn end");
    }

    public IEnumerator StartEndBreadsTurn()
    {
        swanUI.ActionStateButtonInteractable();
        uiManager.panelCurrentTurnObj.SetActive(true);
        uiManager.currentTextCurrentTurn.text = "Swan's turn to shine";
        CameraManager.Instance.StartCoroutine(CameraManager.Instance.CamLookSwan());

        yield return new WaitForSeconds(2f);
        swanUI.ActionStateContainer.SetActive(true);
        uiManager.panelCurrentTurnObj.SetActive(false);
        uiManager.currentTextCurrentTurn.text = "";

        CameraManager.Instance.StartCoroutine(CameraManager.Instance.CamBackToInitialPos());
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
        {
            StartCoroutine(InitEndBreadsTurn());
        }
    }

    private IEnumerator StartInitEndBreadsTurn()
    {
        swanUI.ActionStateContainer.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(StartEndBreadsTurn());
    }

    private IEnumerator InitEndBreadsTurn()
    {
        yield return new WaitForSeconds(1.3f);
        StartCoroutine(EndBreadsTurn());
    }
}
