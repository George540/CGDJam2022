using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] GameObject flashingText;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(Blink());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            audioManager.GameStart();
            Debug.Log("Load level 1");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public IEnumerator Blink()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1f);
            flashingText.SetActive(!flashingText.activeSelf);
        }
    }
}
