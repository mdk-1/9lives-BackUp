using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCat : MonoBehaviour
{
    public Platforms platform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (platform.escapeCat)
            {
                Debug.Log("NextScne");
            }
            else
            {
                Debug.Log("NotYet");
            }
        }
    }
}
