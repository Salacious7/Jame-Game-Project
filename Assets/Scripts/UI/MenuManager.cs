using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator transitionAnim;
    [SerializeField] private Image transitionImage;

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
        SceneManager.LoadScene(1);
    }
}
