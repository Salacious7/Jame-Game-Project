using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator transitionAnim;
    [SerializeField] private Image transitionImage;
    [SerializeField] GameObject openingCutscene;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject sceneTransition;

    private void Awake()
    {
        transitionImage.raycastTarget = false;
    }

    private void Start()
    {
        SoundManager.Instance.OnPlayTitleMusic();
    }

    public void OnPlayGameButton()
    {
        StartCoroutine(PlayGame());
    }

    public void OnQuitGameButton()
    {
        StartCoroutine(QuitGame());
    }

    private IEnumerator PlayGame()
    {
        transitionImage.raycastTarget = true;
        transitionAnim.SetTrigger("isTransition");
        yield return new WaitForSecondsRealtime(1.5f);
        menu.SetActive(false);
        openingCutscene.SetActive(true);
    }

    private IEnumerator QuitGame()
    {
        transitionImage.raycastTarget = true;
        transitionAnim.SetTrigger("isTransition");
        yield return new WaitForSecondsRealtime(1.5f);
        Application.Quit();
    }
}
