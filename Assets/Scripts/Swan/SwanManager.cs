using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanManager : MonoBehaviour
{
    public List<Bread> breads = new List<Bread>();
    private bool selectedBread;
    public int selectedCharacterIndex { get; set; }

    private void EnableSelectedArrow()
    {
        for (int i = 0; i < breads.Count; i++)
        {
            if (i == selectedCharacterIndex)
            {
                breads[i].selectedArrow.SetActive(true);
            }
            else
            {
                breads[i].selectedArrow.SetActive(false);
            }
        }
    }

    public IEnumerator SelectBread(OnBreadHandler state)
    {
        EnableSelectedArrow();

        while (!selectedBread)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                selectedBread = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectedCharacterIndex--;

                if (selectedCharacterIndex < 0)
                    selectedCharacterIndex = breads.Count - 1;

                EnableSelectedArrow();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectedCharacterIndex++;

                if (selectedCharacterIndex > breads.Count - 1)
                    selectedCharacterIndex = 0;

                EnableSelectedArrow();
            }
        }

        yield return new WaitUntil(() => selectedBread);

        state.OnSuccess(breads[selectedCharacterIndex]);
        selectedBread = false;
    }
}




