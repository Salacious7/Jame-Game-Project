using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanState : MonoBehaviour, OnEventHandler
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

    [Header("Component")]
    private SwanUI swanUI;

    private void Awake()
    {
        swanUI = GetComponent<SwanUI>();
        Shuffle<KeyCode>.StartShuffleList(keyCodeArrows);

        leftRandomIndex = Random.Range(0, keyCodeArrows.Count);
        rightRandomIndex = Random.Range(0, keyCodeArrows.Count);

        while (rightRandomIndex == leftRandomIndex)
        {
            rightRandomIndex = Random.Range(0, keyCodeArrows.Count);
        }

        getHeavyMashTimer = setHeavyMashTimer;
    }

    public void OnSuccess()
    {

    }

    public void OnError()
    {
        Debug.Log("Failed");
    }

    public void FightState(Swan.FightType fightStateType, OnEventHandler state)
    {
        switch (fightStateType)
        {
            case Swan.FightType.BasicState:
                Debug.Log("Basic attack pressed!");
                swanUI.SpawnBasicUIStateArrows();
                StartCoroutine(InitializeBasicArrowKey(state));
                break;
            case Swan.FightType.HeavyState:
                Debug.Log("Heavy attack pressed!");
                swanUI.SpawnHeavyUIStateArrows();
                PlayHeavyArrowKey();
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

    public IEnumerator InitializeBasicArrowKey(OnEventHandler state)
    {
        yield return new WaitUntil(() => HorizontalArrowDisplay());

        Debug.Log("Initialize Basic Arrow Key");

        bool condition = true;
        int index = 0;

        while (index < swanUI.BasicActionInputStateContainer.transform.childCount && condition)
        {
            yield return null;

            Debug.Log(swanUI.BasicActionInputStateContainer.transform.GetChild(index).gameObject.name);

            PlayBasicArrowKey(KeyCode.LeftArrow, ref condition, ref index);
            PlayBasicArrowKey(KeyCode.RightArrow, ref condition, ref index);
            PlayBasicArrowKey(KeyCode.DownArrow, ref condition, ref index);
            PlayBasicArrowKey(KeyCode.UpArrow, ref condition, ref index);
        }

        yield return new WaitUntil(() => index >= swanUI.BasicActionInputStateContainer.transform.childCount);

        swanUI.BasicActionInputStateContainer.SetActive(false);

        Debug.Log("Initialize done!");

        state.OnSuccess();
    }

    public void PlayBasicArrowKey(KeyCode keyCode, ref bool condition, ref int index)
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (keyCode.ToString() != swanUI.BasicActionInputStateContainer.transform.GetChild(index).gameObject.name)
            {
                Debug.Log("You failed!");

                condition = false;
                swanUI.HeavyActionInputStateContainer.SetActive(false);

                return;
            }

            index++;
        }
    }

    private void PlayHeavyArrowKey()
    {
        if (getHeavyMashTimer <= 0)
        {
            if (sliderValueIncreased <= 25f)
            {
                Debug.Log("Current damage: " + 5f);
            }
            else if (sliderValueIncreased <= 50f)
            {
                Debug.Log("Current damage: " + 10f);
            }
            else if (sliderValueIncreased <= 75f)
            {
                Debug.Log("Current damage: " + 15f);
            }
            else if (sliderValueIncreased <= 100f)
            {
                Debug.Log("Current damage: " + 20f);
            }

            swanUI.HeavyActionInputStateContainer.SetActive(false);

            getHeavyMashTimer = setHeavyMashTimer;

            return;
        }
        else
        {
            getHeavyMashTimer -= Time.deltaTime;
        }

        if (swanUI.heavyDataSlider.value == 100)
        {
            Debug.Log("Current damage: " + 20f);
            swanUI.HeavyActionInputStateContainer.SetActive(false);
            return;
        }

        HeavyMashInput(keyCodeArrows[(int)Mathf.PingPong(0, 1)]);

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

        swanUI.heavyDataSlider.value = sliderValueIncreased;
    }

    private void HeavyMashInput(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            sliderValueIncreased += 10f;
            canRightMash = true;
        }
    }
}
