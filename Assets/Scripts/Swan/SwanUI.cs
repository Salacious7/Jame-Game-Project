using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwanUI : MonoBehaviour, OnEventHandler
{
    private Swan swan;
    private SwanState swanState;

    [Header("StatsUI")]
    public Slider healthBarSlider;
    public Slider specialPowerBarSlider;

    [Header("ActionStateUI")]
    public GameObject ActionStateContainer;

    public GameObject BasicActionInputStateContainer;
    public GameObject BasicArrowsActionInput;
    public Slider basicArrowTimerSlider;


    public GameObject HeavyActionInputStateContainer;
    public GameObject HeavyArrowsActionInput;
    public Slider heavyDataSlider;
    public Slider heavyArrowTimerSlider;

    [SerializeField] private List<GameObject> actionStateList = new List<GameObject>();
    [SerializeField] private List<Button> actionStateBtnList = new List<Button>();

    [Header("FightUI")]
    public GameObject FightUI;

    [Header("ItemsUI")]
    public GameObject ItemsUIObj;

    [Header("SpecialPowerUI")]
    public GameObject SpecialPowerUIObj;

    [Header("DefendUI")]
    public GameObject DefendObjUI;

    [Header("Special Powers")]
    public Button SwanLeapBtn;
    public Button GroundPummelBtn;

    private void Awake()
    {
        swan = GetComponent<Swan>();

        healthBarSlider.value = swan.swanData.health;
        specialPowerBarSlider.value = swan.swanData.mana;
    }

    private void Update()
    {
        if(swan.swanData.mana > 10f)
        {
            SwanLeapBtn.interactable = true;
        }
        else
        {
            SwanLeapBtn.interactable = false;
        }

        if(swan.swanData.mana > 20f)
        {
            GroundPummelBtn.interactable = true;
        }
        else
        {
            GroundPummelBtn.interactable = false;
        }
    }

    public void OnClickBasicButton()
    {
        swanState.FightState(Swan.FightType.BasicState, this);
    }

    public void OnClickHeavyButton()
    {
        swanState.FightState(Swan.FightType.HeavyState, this);
    }

    public void SpawnBasicUIStateArrows()
    {
        BasicActionInputStateContainer.SetActive(true);
        FightUI.SetActive(false);

        if(SpecialPowerUIObj == null)
            return;

        SpecialPowerUIObj.SetActive(false);
    }

    public void SpawnHeavyUIStateArrows()
    {
        FightUI.SetActive(false);
        HeavyActionInputStateContainer.SetActive(true);
    }

    public void ActionStateButtonInteractable()
    {
        foreach (Button btn in actionStateBtnList)
        {
            btn.interactable = true;
        }
    }

    public void ActionStateButtonUninteractable()
    {
        foreach (Button btn in actionStateBtnList)
        {
            btn.interactable = false;
        }
    }

    public void ActionStateNoAllAccessible()
    {
        BasicActionInputStateContainer.SetActive(false);
        HeavyActionInputStateContainer.SetActive(false);

        foreach (GameObject item in actionStateList)
        {
            item.SetActive(false);
        }
    }

    public void OnlyAccessOne(int index)
    {
        for(int i = 0; i < actionStateList.Count; i++)
        {
            if (index == i)
                actionStateList[i].SetActive(true);
            else
                actionStateList[i].SetActive(false);
        }
    }

    public void ActionStateUI()
    {
        FightUI.SetActive(!FightUI.activeSelf);

        if (!FightUI.activeSelf)
        {
            ActionStateNoAllAccessible();
        }
        else
        {
            OnlyAccessOne(0);
        }
    }

    public void ShowItemUI()
    {
        ItemsUIObj.SetActive(!ItemsUIObj.activeSelf);

        if (!ItemsUIObj.activeSelf)
        {
            ActionStateNoAllAccessible();
        }
        else
        {
            OnlyAccessOne(1);
        }
    }

    public void ShowSpecialPowerUI()
    {
        SpecialPowerUIObj.SetActive(!SpecialPowerUIObj.activeSelf);

        if (!SpecialPowerUIObj.activeSelf)
        {
            ActionStateNoAllAccessible();
        }
        else
        {
            OnlyAccessOne(2);
        }
    }

    public void ShowDefendStateUI()
    {
        DefendObjUI.SetActive(!DefendObjUI.activeSelf);

        if (!DefendObjUI.activeSelf)
        {
            ActionStateNoAllAccessible();
        }
        else
        {
            OnlyAccessOne(3);
        }
    }

    public void OnSuccess()
    {
        throw new System.NotImplementedException();
    }

    public void OnFailed(int amount)
    {
        throw new System.NotImplementedException();
    }
}
