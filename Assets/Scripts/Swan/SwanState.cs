using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanState : MonoBehaviour
{
    private SwanUI swanUI;
    List<string> arrowList = new List<string>();
    public float damageStatus;
    private bool canBasicFightArrowState, canHeavyFightArrowState;
    private float damageRecieved;
    private bool canRightMash;
    private float sliderValueIncreased;
    public float heavyRightMashTimer;
    private int leftRandomIndex;
    private int rightRandomIndex;

    public string[] tempArrows =
    {
        "left",
        "right",
        "down",
        "up"
    };

    private KeyCode[] heavyArrows =
    {
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.UpArrow,
        KeyCode.RightArrow
    };

    private void OnDisable()
    {
        arrowList.Clear();
    }

    private void Awake()
    {
        swanUI = GetComponent<SwanUI>();
        Shuffle(tempArrows);

        foreach (string item in tempArrows)
        {
            arrowList.Add(item);
            Debug.Log(item);
        }

        leftRandomIndex = Random.Range(0, heavyArrows.Length);
        rightRandomIndex = Random.Range(0, heavyArrows.Length);

        while (rightRandomIndex == leftRandomIndex)
        {
            rightRandomIndex = Random.Range(0, heavyArrows.Length);
        }

        Debug.Log(heavyArrows[rightRandomIndex] + " AND " + heavyArrows[leftRandomIndex]);
    }

    private void Update()
    {
        if (canBasicFightArrowState)
        {
            PlayBasicArrowKey();
        }
        else if (canHeavyFightArrowState)
        {
            PlayHeavyArrowKey();
        }
    }

    public void Shuffle(string[] a)
    {
        for (int i = a.Length - 1; i > 0; i--)
        {
            int random = Random.Range(0, i);

            string temp = a[i];

            a[i] = a[random];
            a[random] = temp;
        }
    }

    public void FightState(string state)
    {
        switch (state)
        {
            case "Basic":
                // swanUI.stateUI.visible = false;
                // swanUI.fightInputStateUI.visible = !swanUI.fightInputStateUI.visible;
                damageRecieved = 2.5f;
                canBasicFightArrowState = true;
                Debug.Log("Basic attack pressed!");
                break;
            case "Heavy":
                // swanUI.stateUI.visible = false;
                // swanUI.fightInputStateUI.visible = !swanUI.fightInputStateUI.visible;
                damageRecieved = 5f;
                heavyRightMashTimer = 20f;
                canHeavyFightArrowState = true;
                Debug.Log("Heavy attack pressed!");
                break;
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

        if (Input.GetKeyDown(heavyArrows[rightRandomIndex]) && canRightMash)
        {
            sliderValueIncreased += 10f;
            canRightMash = false;
        }

        if (Input.GetKeyDown(heavyArrows[leftRandomIndex]) && !canRightMash)
        {
            sliderValueIncreased += 10f;
            canRightMash = true;
        }

        swanUI.heavyDataSlider.value = sliderValueIncreased;
    }

    public void PlayBasicArrowKey()
    {
        if (arrowList.Count <= 0)
        {
            Debug.Log("Current damage: " + damageStatus);
            swanUI.HeavyActionInputStateContainer.SetActive(false);
            canBasicFightArrowState = false;
            return;
        }

        Debug.Log(arrowList[0]);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if ("left" != arrowList[0])
            {
                Debug.Log("You failed!");
                canBasicFightArrowState = false;
                return;
            }

            damageStatus += damageRecieved;
            arrowList.RemoveAt(0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if ("right" != arrowList[0])
            {
                Debug.Log("You failed!");
                canBasicFightArrowState = false;
                return;
            }

            damageStatus += damageRecieved;
            arrowList.RemoveAt(0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if ("up" != arrowList[0])
            {
                Debug.Log("You failed!");
                canBasicFightArrowState = false;
                return;
            }

            damageStatus += damageRecieved;
            arrowList.RemoveAt(0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if ("down" != arrowList[0])
            {
                Debug.Log("You failed!");
                canBasicFightArrowState = false;
                return;
            }

            damageStatus += damageRecieved;
            arrowList.RemoveAt(0);
        }
    }
}
