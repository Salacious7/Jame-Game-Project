using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class OpeningCutscene : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] List<GameObject> slides;

    [SerializeField] private Animator transitionAnim;
    [SerializeField] private Image transitionImage;

    private IEnumerator Start()
    {
        int slide = 0;

        transitionAnim.SetTrigger("isEndTransition");

        while (slide < slides.Count)
        {
            yield return null;
            slides.ForEach(x => x.SetActive(false));
            slides[slide].SetActive(true);

            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                    break;

                yield return null;
            }

            slide++;
        }

        StartCoroutine(PlayGame());
    }

    private void CutsceneVoiceState(int value)
    {
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
    }

    private IEnumerator PlayGame()
    {
        transitionImage.raycastTarget = true;
        transitionAnim.SetTrigger("isTransition");
        yield return new WaitForSecondsRealtime(1.5f);
        SoundManager.Instance.OnMusicStop();
        LoadGameScene();
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
