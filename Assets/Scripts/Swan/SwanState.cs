using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanState : MonoBehaviour, OnBreadHandler
{
    [Header("Vertical State Display")]
    private bool canRightMash;
    private float sliderValueIncreased;
    
    [SerializeField] private float setHeavyMashTimer;
    private float getHeavyMashTimer;

    private int leftRandomIndex;
    private int rightRandomIndex;

    private List<KeyCode> keyCodeArrows = new List<KeyCode>()
    {
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.UpArrow,
        KeyCode.RightArrow
    };

    public enum FightType
    {
        BasicState,
        HeavyState
    }

    FightType fightType;

    [Header("Component")]
    private SwanUI swanUI;
    public SwanManager swanManager;
    private Swan swan;

    private void Awake()
    {
        swanUI = GetComponent<SwanUI>();
        swan = GetComponent<Swan>();
        Shuffle<KeyCode>.StartShuffleList(keyCodeArrows);

        leftRandomIndex = Random.Range(0, keyCodeArrows.Count);
        rightRandomIndex = Random.Range(0, keyCodeArrows.Count);

        while (rightRandomIndex == leftRandomIndex)
        {
            rightRandomIndex = Random.Range(0, keyCodeArrows.Count);
        }

        getHeavyMashTimer = setHeavyMashTimer;
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
            case Swan.FightType.DefendState:
                Debug.Log("Defending attack!");
                swanUI.SpawnBasicUIStateArrows();
                StartCoroutine(InitializeDefendBasicArrowKey(state));
                break;
        }
    }

    public bool HorizontalArrowDisplay()
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

    public IEnumerator InitializeDefendBasicArrowKey(OnEventHandler state)
    {
        yield return new WaitUntil(() => HorizontalArrowDisplay());

        Debug.Log("Initialize Basic Arrow Key");

        bool condition = true;
        int index = 0;

        while (index < swanUI.BasicActionInputStateContainer.transform.childCount && condition)
        {
            yield return null;

            Debug.Log(swanUI.BasicActionInputStateContainer.transform.GetChild(index).gameObject.name);

            PlayDefendBasicArrowKey(KeyCode.LeftArrow, state, ref condition, ref index);
            PlayDefendBasicArrowKey(KeyCode.RightArrow, state, ref condition, ref index);
            PlayDefendBasicArrowKey(KeyCode.DownArrow, state, ref condition, ref index);
            PlayDefendBasicArrowKey(KeyCode.UpArrow, state, ref condition, ref index);
        }

        yield return new WaitUntil(() => index >= swanUI.BasicActionInputStateContainer.transform.childCount);

        swanUI.BasicActionInputStateContainer.SetActive(false);

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

                state.OnFailed();

                return;
            }

            index++;
        }
    }

    public IEnumerator InitializeBasicArrowKey(Bread bread, OnBreadHandler state)
    {
        yield return new WaitUntil(() => HorizontalArrowDisplay());

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
        swanUI.HeavyActionInputStateContainer.SetActive(true);

        while (getHeavyMashTimer >= 0 && sliderValueIncreased <= 100)
        {
            yield return null;

            Debug.Log("Heavy attack pressed!");

            if (Input.GetKeyDown(keyCodeArrows[rightRandomIndex]) && canRightMash)
            {
                sliderValueIncreased += 10f;
                canRightMash = false;
            }

            if (Input.GetKeyDown(keyCodeArrows[leftRandomIndex]) && !canRightMash)
            {
                sliderValueIncreased += 10f;
                canRightMash = true;
            }

            getHeavyMashTimer -= Time.deltaTime;

            swanUI.heavyDataSlider.value = sliderValueIncreased;
        }

        yield return new WaitUntil(() => getHeavyMashTimer <= 0 || sliderValueIncreased >= 100);

        swanUI.HeavyActionInputStateContainer.SetActive(false);
        swanUI.heavyDataSlider.value = 0f;
        getHeavyMashTimer = setHeavyMashTimer;

        state.OnSuccess(bread);
    }
}
