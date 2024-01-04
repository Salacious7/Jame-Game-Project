using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLake : MonoBehaviour
{
    [SerializeField] SpriteRenderer lakeRenderer;
    [SerializeField] Sprite lake1;
    [SerializeField] Sprite lake2;
    [SerializeField] float swapInterval;
    Sprite currentSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentSprite = lake1;
        StartCoroutine(nameof(SwapSprite));
    }

    IEnumerator SwapSprite()
    {
        if(currentSprite == lake1)
            currentSprite = lake2;
        else
            currentSprite = lake1;

        lakeRenderer.sprite = currentSprite;

        yield return new WaitForSeconds(swapInterval);
        StartCoroutine(nameof(SwapSprite));
    }
}
