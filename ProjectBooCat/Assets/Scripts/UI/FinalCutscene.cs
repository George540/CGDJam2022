using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalCutscene : MonoBehaviour
{

    [SerializeField] Image background;
    [SerializeField] List<Sprite> images;
    [SerializeField] GameObject text;
    float timeLeft = 64;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cutscene());
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            // SceneManager.LoadScene("JORDAN Title Screen");
            Debug.Log("Title screen now");
        }
    }

    public IEnumerator Cutscene()
    {
        // Cutscene
        int i = -1;
        float[] lengths = { 3, 2, 2, 3, 2, 2, 4, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (float length in lengths) {
            background.sprite = images[++i];
            yield return new WaitForSeconds(length);
        }

        yield return new WaitForSeconds(5);

        // Scrolling
        text.SetActive(true);
        while (text.transform.position.y != 276)
        {
            float newY = Mathf.MoveTowards(text.transform.position.y, 276, 1);
            text.transform.position = new Vector2(text.transform.position.x, newY);
            yield return new WaitForSeconds(0.025f);
        }
    }
}
