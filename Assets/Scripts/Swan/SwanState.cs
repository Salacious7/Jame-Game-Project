using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanState : MonoBehaviour
{
    private SwanUI swanUI;
    List<KeyCode> arrowList = new List<KeyCode>();
    List<GameObject> arrowInputObjList = new List<GameObject>();
    public float damageStatus;
    private bool canBasicFightArrowState, canHeavyFightArrowState;
    private float damageRecieved;
    private bool canRightMash;
    private float sliderValueIncreased;
    public float heavyRightMashTimer;
    private int leftRandomIndex;
    private int rightRandomIndex;

    [SerializeField] private GameObject[] arrowInputUIs;

    private KeyCode[] keyCodeArrows =
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
        Shuffle<KeyCode>.StartShuffle(keyCodeArrows);


        foreach (KeyCode item in keyCodeArrows)
        {
            arrowList.Add(item);
            // Debug.Log(item);
        }

        //

        leftRandomIndex = Random.Range(0, keyCodeArrows.Length);
        rightRandomIndex = Random.Range(0, keyCodeArrows.Length);

        while (rightRandomIndex == leftRandomIndex)
        {
            rightRandomIndex = Random.Range(0, keyCodeArrows.Length);
        }

        Debug.Log(keyCodeArrows[rightRandomIndex] + " AND " + keyCodeArrows[leftRandomIndex]);
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

    public void FightState(string state)
    {
        switch (state)
        {
            case "Basic":
                // swanUI.stateUI.visible = false;
                // swanUI.fightInputStateUI.visible = !swanUI.fightInputStateUI.visible;
                damageRecieved = 2.5f;
                canBasicFightArrowState = true;

                Shuffle<GameObject>.StartShuffle(arrowInputUIs);

                foreach (GameObject obj in arrowInputUIs)
                {
                    GameObject arrowInputObj = Instantiate(obj);
                    arrowInputObj.transform.SetParent(swanUI.BasicActionInputStateContainer.transform);
                    arrowInputObj.name = arrowInputObj.name.Replace("(Clone)", "");
                    arrowInputObjList.Add(arrowInputObj);
                    Debug.Log(arrowInputObj.name);
                }

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
    public void PlayBasicArrowKey()
    {
        if (arrowInputObjList.Count <= 0)
        {
            Debug.Log("Current damage: " + damageStatus);
            swanUI.HeavyActionInputStateContainer.SetActive(false);
            canBasicFightArrowState = false;
            return;
        }

        Debug.Log(arrowInputObjList[0].gameObject.name);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (KeyCode.LeftArrow.ToString() != arrowInputObjList[0].gameObject.name)
            {
                Debug.Log("You failed!");
                canBasicFightArrowState = false;
                return;
            }

            damageStatus += damageRecieved;
            arrowInputObjList.RemoveAt(0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (KeyCode.RightArrow.ToString() != arrowInputObjList[0].gameObject.name)
            {
                Debug.Log("You failed!");
                canBasicFightArrowState = false;
                return;
            }

            damageStatus += damageRecieved;
            arrowInputObjList.RemoveAt(0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (KeyCode.UpArrow.ToString() != arrowInputObjList[0].gameObject.name)
            {
                Debug.Log("You failed!");
                canBasicFightArrowState = false;
                return;
            }

            damageStatus += damageRecieved;
            arrowInputObjList.RemoveAt(0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (KeyCode.DownArrow.ToString() != arrowInputObjList[0].gameObject.name)
            {
                Debug.Log("You failed!");
                canBasicFightArrowState = false;
                return;
            }

            damageStatus += damageRecieved;
            arrowInputObjList.RemoveAt(0);
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
