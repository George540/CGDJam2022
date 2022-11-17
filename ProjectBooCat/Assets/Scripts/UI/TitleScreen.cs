using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] GameObject flashingText, title;
    private float timer;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(Blink());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (((Keyboard.current.anyKey.wasPressedThisFrame && Input.GetKeyDown(KeyCode.Escape) && !Keyboard.current.escapeKey.isPressed)
             || Gamepad.current.allControls.Any(x => x is ButtonControl button && x.IsPressed() && !x.synthetic)) 
            && timer > 1.5f)
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
