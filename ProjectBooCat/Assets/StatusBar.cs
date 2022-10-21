using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Image status;
    [SerializeField] Sprite catIndicator, ghostIndicator;

    public void UpdateStatus(bool isGhost)
    {
        if (isGhost) status.sprite = ghostIndicator;
        else status.sprite = catIndicator;
    }
}
