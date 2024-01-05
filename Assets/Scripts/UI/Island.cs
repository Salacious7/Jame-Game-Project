using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] SpriteRenderer islandRenderer;
    [SerializeField] Sprite island1;
    [SerializeField] Sprite island2;
    [SerializeField] float swapInterval;
    Sprite currentSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentSprite = island1;
        StartCoroutine(nameof(SwapSprite));
    }

    IEnumerator SwapSprite()
    {
        if(currentSprite == island1)
            currentSprite = island2;
        else
            currentSprite = island1;

        islandRenderer.sprite = currentSprite;

        yield return new WaitForSeconds(swapInterval);
        StartCoroutine(nameof(SwapSprite));
    }
}
