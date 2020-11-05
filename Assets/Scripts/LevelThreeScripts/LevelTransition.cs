using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public GameObject houseDoor;
    public GameObject introPanel;
    private float houseDoorAnim = 1f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //disable chasing cat catcher here
            StartCoroutine(StartTransition());
        }

    }
    IEnumerator StartTransition()
    {
        houseDoor.SetActive(true);
        yield return new WaitForSeconds(houseDoorAnim);
        introPanel.SetActive(true);
    }
}
