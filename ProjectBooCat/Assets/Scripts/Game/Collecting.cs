using System;
using Unity.VisualScripting;
using UnityEngine;

public class Collecting : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collectible"))
        {
            GameManager.Instance.AddKey();
            Destroy(col.gameObject);
            Debug.Log("Collected Key");
        }
    }
}
