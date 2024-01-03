using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwanUI : MonoBehaviour
{
    private Swan swan;
    private SwanState swanState;

    [SerializeField] 
    private UIDocument uIDocument;
    private VisualElement root;

    private Button fightBtn, itemsBtn,
           sPowerBtn, defendBtn;

    private Button basicAtkBtn, heavyAtkBtn;

    public GameObject ActionContainer;
    public GameObject BasicActionInputStateContainer;
    public GameObject HeavyActionInputStateContainer;
    public UnityEngine.UI.Slider heavyDataSlider;

    public VisualElement stateUI { get; private set; }
    public VisualElement fightInputStateUI { get; private set; }
    public VisualElement fightInputStateContainerUI { get; private set; }

    private List<Button> actionStateBtnList = new List<Button>();
    private int currentActionStateBtnIndex;

    private void Awake()
    {
        swan = GetComponent<Swan>();
        swanState = GetComponent<SwanState>();

        // InitActionStateUI();
    }

    private void InitActionStateUI()
    {
        root = uIDocument.rootVisualElement;

        fightBtn = root.Q<Button>("FightBtn");
        itemsBtn = root.Q<Button>("ItemsBtn");
        sPowerBtn = root.Q<Button>("SPowerBtn");
        defendBtn = root.Q<Button>("DefendBtn");

        fightBtn.clicked += () => swan.Fight();
        itemsBtn.clicked += () => swan.UseItem();
        sPowerBtn.clicked += () => swan.SpecialPower();
        defendBtn.clicked += () => swan.Defend();

        stateUI = root.Q<VisualElement>("StateUI");
        fightInputStateUI = root.Q<VisualElement>("FightInputState");
        fightInputStateContainerUI = root.Q<VisualElement>("FightInputStateContainer");

        basicAtkBtn = root.Q<Button>("BasicAttackBtn");
        heavyAtkBtn = root.Q<Button>("HeavyAttackBtn");

        basicAtkBtn.clicked += () => swanState.FightState("Basic");
        heavyAtkBtn.clicked += () => swanState.FightState("Heavy");
    }

    public void SpawnBasicUIStateArrows()
    {
        // fightInputStateContainerUI.

        swanState.FightState("Basic");
        ActionContainer.SetActive(false);
        BasicActionInputStateContainer.SetActive(true);
    }

    public void SpawnHeavyUIStateArrows()
    {
        // fightInputStateContainerUI.

        swanState.FightState("Heavy");
        ActionContainer.SetActive(false);
        HeavyActionInputStateContainer.SetActive(true);
    }

    public void ActionStateUI()
    {
        ActionContainer.active = !ActionContainer.active;

        // stateUI.visible = !stateUI.visible;
    }

    public void DefendStateUI()
    {

    }
}
