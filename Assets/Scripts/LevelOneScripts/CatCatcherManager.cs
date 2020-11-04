using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCatcherManager : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint;
    private float aiSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.sewerTriggered == true)
        {
            if (this.transform.position.x == patrolPoints[currentPoint].position.x)
            {
                currentPoint++;
            }

            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;    //reset it to zero
            }
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(patrolPoints[currentPoint].position.x, transform.position.y), aiSpeed * Time.deltaTime);
        }

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.health = -1;
        }
    }

}
