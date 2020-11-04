using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCat : MonoBehaviour
{
    public Platforms platform;
    public GameObject cutScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (platform.escapeCat)
            {
                cutScene.SetActive(true);
            }
            else
            {
                Debug.Log("NotYet");
            }
        }
    }
}
