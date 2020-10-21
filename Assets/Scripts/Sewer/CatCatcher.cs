using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawner;

public class CatCatcher : MonoBehaviour
{
    private SpriteRenderer catSR;

    public GameObject player;

    [SerializeField, Tooltip("1=Right,-1=Left")]
    private int dir = 1;
    [SerializeField]
    private float moveSpeed = 100f;

    public float distance = 5;
    public float minDistance = 1;


    private void Start()
    {
        catSR = gameObject.GetComponent<SpriteRenderer>();
   
        dir = 1;
        distance = Vector2.Distance(transform.position, player.transform.position);
        
}
    // Update is called once per frame
    void Update()

    {
        SpawnDirection();

        transform.position = new Vector2(transform.position.x + Time.deltaTime * dir * moveSpeed, transform.position.y);
    }

    private void SpawnDirection()
    {
        

        if (transform.position.y > 44)
        {
            if (transform.position.x < 0)
            {
                dir = 1;
                catSR.flipX = false;
            }
           if(transform.position.x>0)
            {
                dir = -1;
                catSR.flipX = true;
            }

        }
    }
}
