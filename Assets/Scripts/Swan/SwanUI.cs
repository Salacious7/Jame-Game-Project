using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwanUI : MonoBehaviour
{
    private Swan swan;
    private SwanState swanState;

    [Header("ActionStateUI")]
    public GameObject BasicActionInputStateContainer;
    public GameObject HeavyActionInputStateContainer;
    public UnityEngine.UI.Slider heavyDataSlider;

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
        swanState.FightState(Swan.FightType.BasicState);
    }

    public void OnClickHeavyButton()
    {
        swanState.FightState(Swan.FightType.HeavyState);
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

    public void ActionStateUI()
    {
        FightUI.SetActive(!FightUI.activeSelf);
    }

    public void ShowItemUI()
    {
        ItemsUIObj.SetActive(!ItemsUIObj.activeSelf);
    }

    public void ShowSpecialPowerUI()
    {
        SpecialPowerUIObj.SetActive(!SpecialPowerUIObj.activeSelf);
    }

    public void ShowDefendStateUI()
    {
        DefendObjUI.SetActive(!DefendObjUI.activeSelf);
    }
}
