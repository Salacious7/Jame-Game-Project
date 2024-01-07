using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningCutscene : MonoBehaviour
{
    [SerializeField] List<GameObject> slides;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        int slide = 0;

        while(slide < slides.Count)
        {
            slides.ForEach(x => x.SetActive(false));
            slides[slide].SetActive(true);

            while(true)
            {
                if(Input.GetMouseButtonDown(0))
                    break;

                yield return null;
            }
            slide++;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
