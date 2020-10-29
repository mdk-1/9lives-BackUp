using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform currentCheckpoint;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.health > 1)
            {
                GameManager.instance.health -= 1;
                other.transform.position = new Vector3(currentCheckpoint.transform.position.x, currentCheckpoint.transform.position.y, other.transform.position.z);
            }
            else
            {
                //might create IEnumerator here to trigger death outro before transition to game over
                SceneManager.LoadScene(7);
            }
            
        }
    }
}
