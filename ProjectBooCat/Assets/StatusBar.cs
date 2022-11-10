using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Image status, card;
    [SerializeField] Sprite catIndicator, ghostIndicator, card0, card1, card2, card3;

    public void UpdateStatus(bool isGhost)
    {
        if (isGhost) status.sprite = ghostIndicator;
        else status.sprite = catIndicator;
    }

    public void UpdateCountdown(int keys)
    {
        switch (keys) {
            case 0:
                card.sprite = card0;
                break;

            case 1:
                card.sprite = card1;
                break;

            case 2:
                card.sprite = card2;
                break;

            case 3:
                card.sprite = card3;
                break;

            default:
                Debug.Log("not normal");
                break;
        }
    }
}
