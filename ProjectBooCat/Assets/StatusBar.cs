using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Image status;
    [SerializeField] Sprite catIndicator, ghostIndicator, card1, card2, card3, card4, card5;
    [SerializeField] GameObject cardIndicator, end;
    int pointer = 0;

    public void UpdateStatus(bool isGhost)
    {
        if (isGhost) status.sprite = ghostIndicator;
        else status.sprite = catIndicator;
    }

    public void AddCard()
    {
        pointer++;
        end.GetComponent<RectTransform>().anchoredPosition = new Vector3(-98 + pointer * 17, 87.5f);
    }

    public void RemoveAllCards()
    {
        pointer = 0;
        end.GetComponent<RectTransform>().position = new Vector3(-98, 87.5f);
    } 
}
