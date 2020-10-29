using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorTriggerClose : MonoBehaviour
{
    public Animator anim;
    public GameObject eCover;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eCover.SetActive(false);
        }
    }
    private void Start()
    {
        anim.enabled = false;
    }
}
