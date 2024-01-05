using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanItemChance : MonoBehaviour
{
    public GameObject breadCrumbs;
    public GameObject shinyFeather;
    public GameObject clearBlueCrystal;
    public GameObject caffeinatedDrink;
    public GameObject milk;

    private int randomChanceIndex;

    public bool CheckForChance()
    {
        int randomChanceIndex = Random.Range(0, 2);

        if(randomChanceIndex == 1)
        {
            return true;
        }

        return false;
    }

    public void GetBreadCrumbsChance()
    {
        if(CheckForChance())
        {
            breadCrumbs.SetActive(true);
        }
    }

    public void GetShinyFeatherChance()
    {
        if (CheckForChance())
        {
            shinyFeather.SetActive(true);
        }
    }

    public void GetClearBlueCrystalChance()
    {
        if (CheckForChance())
        {
            clearBlueCrystal.SetActive(true);
        }
    }

    public void GetCaffeinatedDrinkOrMilkChance()
    {
        Debug.Log(CheckForChance());

        if (CheckForChance())
        {
            int randomIndex = Random.Range(0, 1);

            GameObject obj = randomIndex == 1 ? caffeinatedDrink : milk;

            obj.SetActive(true);
        }
    }

    public void GetMilkChance()
    {
        if (CheckForChance())
        {
            milk.SetActive(true);
        }
    }
}
