using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalCutscene : MonoBehaviour
{

    [SerializeField] Image background;
    [SerializeField] List<Sprite> images;
    [SerializeField] GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cutscene());
    }

   public IEnumerator Cutscene()
   {
        // Cutscene
        background.sprite = images[0];
        yield return new WaitForSeconds(3);
        background.sprite = images[1];
        yield return new WaitForSeconds(2);
        background.sprite = images[2];
        yield return new WaitForSeconds(3);
        background.sprite = images[3];
        yield return new WaitForSeconds(2);
        background.sprite = images[4];
        yield return new WaitForSeconds(2);
        background.sprite = images[5];
        yield return new WaitForSeconds(4);
        background.sprite = images[6];
        yield return new WaitForSeconds(3);
        background.sprite = images[7];
        yield return new WaitForSeconds(4);
        background.sprite = images[8];
        yield return new WaitForSeconds(8);
        background.sprite = images[9];
        yield return new WaitForSeconds(5);
        background.sprite = images[8];

        //Scrolling
        text.SetActive(true);
        while (text.transform.position.y != 2760)
        {
            float newY = Mathf.MoveTowards(text.transform.position.y, 2760, 10);
            text.transform.position = new Vector2(text.transform.position.x, newY);
            yield return new WaitForSeconds(0.025f);
        }
    }
}
