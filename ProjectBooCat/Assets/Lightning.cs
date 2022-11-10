using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lightning : MonoBehaviour
{
    bool isCoroutineRunning = false;
    public AudioManager audioManager;

    void Update()
    {
        if (!isCoroutineRunning) StartCoroutine(RandomLightning());
    }

    public IEnumerator RandomLightning()
    {
        isCoroutineRunning = true;
        StartCoroutine(LightningStrike());
        System.Random rand = new System.Random();
        float wait = (float) rand.NextDouble();
        wait = wait * 12.0f + 4.0f;
        yield return new WaitForSeconds(wait);
        isCoroutineRunning = false;
    }

    public IEnumerator LightningStrike()
    {
        audioManager.ThunderClap();
        Image image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        while(image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 0.125f);
            yield return new WaitForSeconds(0.1f);
        }

        //yield return new WaitForSeconds(1.5f);
    }
}
