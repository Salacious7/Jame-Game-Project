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

    public void OnPlayGameButton()
    {
        StartCoroutine(PlayGame());
    }

    private IEnumerator PlayGame()
    {
        transitionImage.raycastTarget = true;
        transitionAnim.SetTrigger("isTransition");
        yield return new WaitForSecondsRealtime(1.5f);
        menu.SetActive(false);
        sceneTransition.SetActive(false);
        openingCutscene.SetActive(true);
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
