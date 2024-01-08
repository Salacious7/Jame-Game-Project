using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwanManager : MonoBehaviour
{
    private bool selectedBread;
    public int selectedCharacterIndex { get; set; }

    private List<Bread> activeBreads = new List<Bread>();

    private void EnableSelectedArrow()
    {
        activeBreads = new List<Bread>(BreadManager.Instance.breads);
        activeBreads.RemoveAll(x => x.Dead);

        for (int i = 0; i < activeBreads.Count; i++)
        {
            if (i == selectedCharacterIndex)
            {
                activeBreads[i].selectedArrow.SetActive(true);
            }
            else
            {
                activeBreads[i].selectedArrow.SetActive(false);
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
                    selectedCharacterIndex = activeBreads.Count - 1;

                EnableSelectedArrow();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectedCharacterIndex++;

                if (selectedCharacterIndex > activeBreads.Count - 1)
                    selectedCharacterIndex = 0;

                EnableSelectedArrow();
            }
        }

        yield return new WaitUntil(() => selectedBread);

        state.OnSuccess(activeBreads[selectedCharacterIndex]);
        selectedBread = false;
    }
}




