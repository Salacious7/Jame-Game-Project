using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanState : MonoBehaviour, OnBreadHandler
{
    [Header("Vertical State Display")]
    private bool canRightMash, canLeftMash;
    public float sliderValueIncreased;

    [SerializeField] private float setDefenseTimer;
    private float getDefenseTimer;
    [SerializeField] private float setHeavyMashTimer;
    private float getHeavyMashTimer;

    private KeyCode leftKeyCode, rightKeyCode;

    public enum FightType
    {
        BasicState,
        HeavyState,
        SpecialPowerState
    }

    FightType fightType;

    [Header("Component")]
    private SwanUI swanUI;
    public SwanManager swanManager;
    private Swan swan;
    private SwanPowers swanPowers;

    private void Awake()
    {
        swanUI = GetComponent<SwanUI>();
        swan = GetComponent<Swan>();
        swanPowers = GetComponent<SwanPowers>();

        getHeavyMashTimer = setHeavyMashTimer;
        getDefenseTimer = setDefenseTimer;
    }

    public void OnSuccess(Bread bread)
    {
        switch(fightType)
        {
            case FightType.BasicState:
                swanUI.SpawnBasicUIStateArrows();
                StartCoroutine(InitializeBasicArrowKey(bread, swan));
                break;
            case FightType.HeavyState:
                StartCoroutine(InitializeHeavyArrowKey(bread, swan));
                swanUI.SpawnHeavyUIStateArrows();
                break;
            case FightType.SpecialPowerState:
                swanUI.SpawnBasicUIStateArrows();
                StartCoroutine(InitializeBasicArrowKey(bread, swanPowers));
                break;
        }
    }

    public void OnFailed(int amount)
    {
        Debug.Log("Failed");
    }

    public void FightState(Swan.FightType fightStateType, OnEventHandler state)
    {
        switch (fightStateType)
        {
            case Swan.FightType.BasicState:
                Debug.Log("Basic attack pressed!");
                swanManager.StartCoroutine(swanManager.SelectBread(this));
                fightType = FightType.BasicState;
                break;
            case Swan.FightType.HeavyState:
                Debug.Log("Heavy attack pressed!");
                swanManager.StartCoroutine(swanManager.SelectBread(this));
                fightType = FightType.HeavyState;
                break;
            case Swan.FightType.SpecialPowerState:
                Debug.Log("Basic attack pressed!");
                swanManager.StartCoroutine(swanManager.SelectBread(this));
                fightType = FightType.SpecialPowerState;
                break;
            case Swan.FightType.DefendState:
                Debug.Log("Defending attack!");
                swanUI.SpawnBasicUIStateArrows();
                StartCoroutine(InitializeDefendBasicArrowKey(state));
                break;
        }
    }

    public bool BasicArrowDisplay()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in swanUI.BasicActionInputStateContainer.transform)
        {
            children.Add(child);
        }

        for (int i = 0; i < children.Count; i++)
        {
            int randomIndex = Random.Range(i, children.Count);
            Transform temp = children[i];
            children[i] = children[randomIndex];
            children[randomIndex] = temp;
        }

        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }

        return true;
    }

    public bool HeavyArrowDisplay()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in swanUI.HeavyArrowsActionInput.transform)
        {
            children.Add(child);
        }

        for (int i = 0; i < children.Count; i++)
        {
            int randomIndex = Random.Range(i, children.Count);
            Transform temp = children[i];
            children[i] = children[randomIndex];
            children[randomIndex] = temp;
        }

        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }

        children[0].gameObject.SetActive(true);
        children[1].gameObject.SetActive(true);

        return true;
    }


    public IEnumerator InitializeDefendBasicArrowKey(OnEventHandler state)
    {
        yield return new WaitUntil(() => BasicArrowDisplay());

        Debug.Log("Initialize Basic Arrow Key");

        bool condition = true;
        int index = 0;

        while (index < swanUI.BasicActionInputStateContainer.transform.childCount && condition && getDefenseTimer > 0)
        {
            yield return null;

            Debug.Log(swanUI.BasicActionInputStateContainer.transform.GetChild(index).gameObject.name);

            PlayDefendBasicArrowKey(KeyCode.LeftArrow, state, ref condition, ref index);
            PlayDefendBasicArrowKey(KeyCode.RightArrow, state, ref condition, ref index);
            PlayDefendBasicArrowKey(KeyCode.DownArrow, state, ref condition, ref index);
            PlayDefendBasicArrowKey(KeyCode.UpArrow, state, ref condition, ref index);

            getDefenseTimer -= Time.deltaTime;
        }

        if(getDefenseTimer <= 0)
        {
            Debug.Log("You failed!");

            condition = false;
            swanUI.BasicActionInputStateContainer.SetActive(false);

            state.OnFailed(0);
            getDefenseTimer = setDefenseTimer;

            yield break;
        }

        yield return new WaitUntil(() => index >= swanUI.BasicActionInputStateContainer.transform.childCount);

        getDefenseTimer = setDefenseTimer;
        swanUI.BasicActionInputStateContainer.SetActive(false);
        swan.damageIndex = index;

        Debug.Log("Initialize done!");

        state.OnSuccess();
    }

    public void PlayDefendBasicArrowKey(KeyCode keyCode, OnEventHandler state, ref bool condition, ref int index)
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (keyCode.ToString() != swanUI.BasicActionInputStateContainer.transform.GetChild(index).gameObject.name)
            {
                Debug.Log("You failed!");

                condition = false;
                swanUI.BasicActionInputStateContainer.SetActive(false);

                state.OnFailed(index);

                return;
            }

            index++;
        }
    }

    public IEnumerator InitializeBasicArrowKey(Bread bread, OnBreadHandler state)
    {
        yield return new WaitUntil(() => BasicArrowDisplay());

        Debug.Log("Initialize Basic Arrow Key");

        bool condition = true;
        int index = 0;

        while (index < swanUI.BasicActionInputStateContainer.transform.childCount && condition)
        {
            yield return null;

            Debug.Log(swanUI.BasicActionInputStateContainer.transform.GetChild(index).gameObject.name);

            PlayBasicArrowKey(KeyCode.LeftArrow, state, ref condition, ref index);
            PlayBasicArrowKey(KeyCode.RightArrow, state, ref condition, ref index);
            PlayBasicArrowKey(KeyCode.DownArrow, state, ref condition, ref index);
            PlayBasicArrowKey(KeyCode.UpArrow, state, ref condition, ref index);
        }

        yield return new WaitUntil(() => index >= swanUI.BasicActionInputStateContainer.transform.childCount);

        swanUI.BasicActionInputStateContainer.SetActive(false);

        Debug.Log("Initialize done!");

        state.OnSuccess(bread);
    }

    public void PlayBasicArrowKey(KeyCode keyCode, OnBreadHandler state, ref bool condition, ref int index)
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (keyCode.ToString() != swanUI.BasicActionInputStateContainer.transform.GetChild(index).gameObject.name)
            {
                Debug.Log("You failed!");

                condition = false;
                swanUI.BasicActionInputStateContainer.SetActive(false);

                state.OnFailed(index);

                return;
            }

            index++;
        }
    }

    public IEnumerator InitializeHeavyArrowKey(Bread bread, OnBreadHandler state)
    {
        yield return new WaitUntil(() => HeavyArrowDisplay());

        while (getHeavyMashTimer >= 0 && sliderValueIncreased <= 100)
        {
            yield return null;

            GetKeyCode();

            if (Input.GetKeyDown(leftKeyCode) && !canRightMash)
            {
                sliderValueIncreased += 10f;
                canRightMash = true;
                canLeftMash = false;
            }

            if (Input.GetKeyDown(rightKeyCode) && !canLeftMash)
            {
                sliderValueIncreased += 10f;
                canRightMash = false;
                canLeftMash = true;
            }

            getHeavyMashTimer -= Time.deltaTime;

            swanUI.heavyDataSlider.value = sliderValueIncreased;
        }

        yield return new WaitUntil(() => getHeavyMashTimer <= 0 || sliderValueIncreased >= 100);

        swanUI.HeavyActionInputStateContainer.SetActive(false);
        swanUI.heavyDataSlider.value = 0f;
        getHeavyMashTimer = setHeavyMashTimer;

        swanUI.HeavyArrowsActionInput.transform.GetChild(0).gameObject.SetActive(false);
        swanUI.HeavyArrowsActionInput.transform.GetChild(1).gameObject.SetActive(false);

        state.OnSuccess(bread);
    }

    private void GetKeyCode()
    {
        if (swanUI.HeavyArrowsActionInput.transform.GetChild(0).gameObject.name == KeyCode.UpArrow.ToString())
        {
            leftKeyCode = KeyCode.UpArrow;
        }
        else if (swanUI.HeavyArrowsActionInput.transform.GetChild(0).gameObject.name == KeyCode.DownArrow.ToString())
        {
            leftKeyCode = KeyCode.DownArrow;
        }
        else if (swanUI.HeavyArrowsActionInput.transform.GetChild(0).gameObject.name == KeyCode.RightArrow.ToString())
        {
            leftKeyCode = KeyCode.RightArrow;
        }
        else if (swanUI.HeavyArrowsActionInput.transform.GetChild(0).gameObject.name == KeyCode.LeftArrow.ToString())
        {
            leftKeyCode = KeyCode.LeftArrow;
        }

        if (swanUI.HeavyArrowsActionInput.transform.GetChild(1).gameObject.name == KeyCode.UpArrow.ToString())
        {
            rightKeyCode = KeyCode.UpArrow;
        }
        else if (swanUI.HeavyArrowsActionInput.transform.GetChild(1).gameObject.name == KeyCode.DownArrow.ToString())
        {
            rightKeyCode = KeyCode.DownArrow;
        }
        else if (swanUI.HeavyArrowsActionInput.transform.GetChild(1).gameObject.name == KeyCode.RightArrow.ToString())
        {
            rightKeyCode = KeyCode.RightArrow;
        }
        else if (swanUI.HeavyArrowsActionInput.transform.GetChild(1).gameObject.name == KeyCode.LeftArrow.ToString())
        {
            rightKeyCode = KeyCode.LeftArrow;
        }

    }
}
