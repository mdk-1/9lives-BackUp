using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject sewerClosed;
    public GameObject sewerOpen;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //handle outro dialogue/level transition here
            sewerClosed.SetActive(false);
            sewerOpen.SetActive(true);            
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        levelTransition();
    }

    public void levelTransition()
    {
        if(sewerOpen.activeInHierarchy == true)
        {
            SceneManager.LoadScene(4);
        }
    }
}
