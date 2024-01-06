using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject loseScreenObj;
    [SerializeField] private BreadManager breadManager;
    [SerializeField] private SwanManager swanManager;

    public void InitializeLoseScreen()
    {
        loseScreenObj.SetActive(true);
        StopAllCoroutines();
    }
}
