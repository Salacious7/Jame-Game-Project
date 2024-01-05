using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwanUI : MonoBehaviour, OnEventHandler
{
    private Swan swan;
    private SwanState swanState;


    [Header("ActionStateUI")]
    public GameObject ActionStateContainer;
    public GameObject BasicActionInputStateContainer;
    public GameObject HeavyActionInputStateContainer;
    public UnityEngine.UI.Slider heavyDataSlider;
    [SerializeField] private List<Button> actionStateBtnList = new List<Button>();

    [Header("FightUI")]
    public GameObject FightUI;

    [Header("ItemsUI")]
    public GameObject ItemsUIObj;

    [Header("SpecialPowerUI")]
    public GameObject SpecialPowerUIObj;

    [Header("DefendUI")]
    public GameObject DefendObjUI;

    private void Awake()
    {
        swan = GetComponent<Swan>();
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

    public void ActionStateAllAccessible()
    {
        ActionStateContainer.SetActive(true);

        foreach (Button btn in actionStateBtnList)
        {
            btn.interactable = true;
        }
    }

    public void ActionStateNoAllAccessible()
    {
        foreach (Button btn in actionStateBtnList)
        {
            btn.interactable = false;
        }

        FightUI.SetActive(false);
        ItemsUIObj.SetActive(false);
        SpecialPowerUIObj.SetActive(false);
        DefendObjUI.SetActive(false);
    }

    public void OnlyAccessOne(int index)
    {
        for(int i = 0; i < actionStateBtnList.Count; i++)
        {
            if (index == i)
                actionStateBtnList[i].interactable = true;
            else
                actionStateBtnList[i].interactable = false;
        }
    }

    public void ActionStateUI()
    {
        FightUI.SetActive(!FightUI.activeSelf);

        if(!FightUI.activeSelf)
        {
            ActionStateAllAccessible();
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
            ActionStateAllAccessible();
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
            ActionStateAllAccessible();
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
            ActionStateAllAccessible();
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

    public void OnFailed()
    {
        throw new System.NotImplementedException();
    }
}
