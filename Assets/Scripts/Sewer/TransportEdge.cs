using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransportEdge : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnPoints;
    int spawn = 0;


    private void Start()
    {
        spawn = Random.Range(0, spawnPoints.Length);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bound")
        {

            transform.position = spawnPoints[spawn].transform.position;
            spawn = Random.Range(0, spawnPoints.Length);
        }
    }



}

