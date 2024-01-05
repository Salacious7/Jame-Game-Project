using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanState : MonoBehaviour, OnEventHandler
{
    [Header("Vertical State Display")]
    private bool canRightMash;
    private float sliderValueIncreased;
    public float heavyRightMashTimer;
    private int leftRandomIndex;
    private int rightRandomIndex;

    private List<KeyCode> keyCodeArrows = new List<KeyCode>()
    {
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.UpArrow,
        KeyCode.RightArrow
    };

    [Header("Misc")]
    private List<Transform> currentTransformArrowObjUIList = new List<Transform>();
    private int[] arrowIndex = {0, 1, 2, 3};

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
                Debug.Log(keyCodeArrows[rightRandomIndex] + " AND " + keyCodeArrows[leftRandomIndex]);
                PlayHeavyArrowKey();
                break;
        }
    }

    public bool HorizontalArrowDisplay()
    {
        Shuffle<int>.StartShuffleArray(arrowIndex);

        int children = swanUI.BasicActionInputStateContainer.transform.childCount;

        for (int i = 0; i < children; ++i)
        {
            Transform item = swanUI.BasicActionInputStateContainer.transform.GetChild(i);
            currentTransformArrowObjUIList.Add(item);
            item.SetSiblingIndex(arrowIndex[i]);
        }

        return true;
    }

    public void VerticalArrowDisplay()
    {
        heavyRightMashTimer = 20f;
    }

    public IEnumerator InitializeBasicArrowKey(OnEventHandler state)
    {
        yield return new WaitUntil(() => HorizontalArrowDisplay());

        Debug.Log("Initialize Basic Arrow Key");

        bool condition = true;
        int index = 0;

        while (index < currentTransformArrowObjUIList.Count && condition)
        {
            yield return null;

            PlayBasicArrowKey(KeyCode.LeftArrow, ref condition, ref index);
            PlayBasicArrowKey(KeyCode.RightArrow, ref condition, ref index);
            PlayBasicArrowKey(KeyCode.DownArrow, ref condition, ref index);
            PlayBasicArrowKey(KeyCode.UpArrow, ref condition, ref index);

            Debug.Log(currentTransformArrowObjUIList[index].gameObject.name);
        }

        yield return new WaitUntil(() => index >= currentTransformArrowObjUIList.Count);

        swanUI.BasicActionInputStateContainer.SetActive(false);

        Debug.Log("Initialize done!");

        state.OnSuccess();
    }

    public void PlayBasicArrowKey(KeyCode keyCode, ref bool condition, ref int index)
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (keyCode.ToString() != currentTransformArrowObjUIList[index].gameObject.name)
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
        if (heavyRightMashTimer <= 0)
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
            return;
        }
        else
        {
            heavyRightMashTimer -= Time.deltaTime;
        }

        if (swanUI.heavyDataSlider.value == 100)
        {
            Debug.Log("Current damage: " + 20f);
            swanUI.HeavyActionInputStateContainer.SetActive(false);
            return;
        }

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
}
