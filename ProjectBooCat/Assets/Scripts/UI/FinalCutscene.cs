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
        if(timeLeft < 0)
        {
            Debug.Log("Title screen now");
            SceneManager.LoadScene("JORDAN Title Screen");
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

        yield return new WaitForSeconds(10.0f);

        // Scrolling
        text.SetActive(true);
        while (text.transform.localPosition.y < 1800)
        {
            float newY = Mathf.MoveTowards(text.transform.localPosition.y, 1800, 10);
            text.transform.localPosition = new Vector2(text.transform.localPosition.x, newY);
            yield return new WaitForSeconds(0.025f);
        }
    }
}
