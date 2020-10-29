using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorTrigger : MonoBehaviour
{
    public Animator anim;
    public GameObject l4Trigger;
    public GameObject l3Trigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            l4Trigger.SetActive(true);
            l3Trigger.SetActive(false);
            anim.enabled = true;
        }
    }
    private void Start()
    {
        anim.enabled = false;
    }
}
