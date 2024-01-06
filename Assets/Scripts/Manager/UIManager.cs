using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    public GameObject panelCurrentTurnObj;
    public TextMeshProUGUI currentTextCurrentTurn;

    [SerializeField] private Animator transitionAnim;
    [SerializeField] private Image transitionImage;

    [SerializeField] private GameObject pauseMenuObj;

    private void Awake()
    {
        transitionImage.raycastTarget = false;
        Time.timeScale = 1f;

        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pauseMenuObj.SetActive(true);
        }
    }

    public void OnResumeBtn()
    {
        Time.timeScale = 1f;
        pauseMenuObj.SetActive(false);
    }

    public void OnBackToMenuBtn()
    {
        StartCoroutine(BackToMenu());
    }

    private IEnumerator BackToMenu()
    {
        transitionImage.raycastTarget = true;
        transitionAnim.SetTrigger("isTransition");
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(0);
    }
}
