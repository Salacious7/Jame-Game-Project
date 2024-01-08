using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using FMOD.Studio;

public class OpeningCutscene : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] List<GameObject> slides;
    [SerializeField] private GameObject nextTextObj;

    [SerializeField] private Animator transitionAnim;
    [SerializeField] private Image transitionImage;

    private int slide;

    private void Start()
    {
        StartCoroutine(ShowNextText());
        StartCoroutine(StartCutscene());
    }

    private IEnumerator ShowNextText()
    {
        yield return new WaitForSeconds(8f);

        nextTextObj.SetActive(true);
    }

    private IEnumerator StartCutscene()
    {
        transitionAnim.SetTrigger("isEndTransition");
        CutsceneVoiceState(slide);

        while (slide < slides.Count)
        {
            yield return null;
            slides.ForEach(x => x.SetActive(false));
            slides[slide].SetActive(true);

            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    break;
                }

                yield return null;
            }

            slide++;
            CutsceneVoiceState(slide);
        }

        StartCoroutine(PlayGame());
    }

    private void CutsceneVoiceState(int value)
    {
        switch (value)
        {
            case 0:
                SoundManager.Instance.OnPlayPanelOne();
                break;
            case 1:
                SoundManager.Instance.OnPlayPanelTwo();
                break;
            case 2:
                SoundManager.Instance.OnPlayPanelThree();
                break;
            case 3:
                SoundManager.Instance.OnPlayPanelFour();
                break;
            case 4:
                SoundManager.Instance.OnPlayPanelFive();
                break;
            case 5:
                SoundManager.Instance.OnPlayPanelSix();
                break;
            case 6:
                SoundManager.Instance.OnPlayPanelEnd();
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
