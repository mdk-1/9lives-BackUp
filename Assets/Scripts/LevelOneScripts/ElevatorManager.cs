using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public GameObject player;
    Vector3 m_YAxis;

    Rigidbody2D playerRb;

    private void Start()
    {
        m_YAxis = new Vector3(0, 30, 0);
        playerRb = player.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRb.isKinematic = true;
            playerRb.velocity = -m_YAxis;
            player.GetComponent<Animator>().speed = 0;
            

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRb.isKinematic = false;
            player.GetComponent<Animator>().speed = 1;
        }
    }
}
