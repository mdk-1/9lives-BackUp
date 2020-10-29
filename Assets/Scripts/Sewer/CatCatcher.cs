using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Spawner;

public class CatCatcher : MonoBehaviour
{
    private SpriteRenderer catSR;

    public GameObject player;

    [SerializeField, Tooltip("1=Right,-1=Left")]
    private int dir = 1;
    [SerializeField]
    private float moveSpeed = 100f;


    public float catPoint1;
    public float catPoint2;
    public float catPointCountdown = 2;
    public float catPointReset = 2;
    public float catPointTest;
    public float zigga = 10;
    public bool activate = false;

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
        TurnArouncCatcher();
        if (dir == 1)
        {
            catSR.flipX = false;
        }
        if (dir == -1)
        {
            catSR.flipX = true;
        }
        transform.position = new Vector2(transform.position.x + Time.deltaTime * dir * moveSpeed, transform.position.y);
    }

    private void TurnArouncCatcher()
    {
        catPointCountdown -= Time.deltaTime;
        catPoint2 = transform.position.x;
        catPointTest = catPoint1 - transform.position.x;

        if (catPointCountdown <= 0)
        {
            catPointCountdown = catPointReset;
            catPoint1 = transform.position.x;

        }
      
        if (catPointTest <= zigga && catPointTest >= -zigga)
        {
            activate = true;
            catPointCountdown = 5;
            dir *= -1;
            catPoint1 = 2000;
        }
    }

    private void SpawnDirection()
    {


        if (transform.position.y > 44)
        {
            if (transform.position.x < 0)
            {
                dir = 1;
            }
            if (transform.position.x > 0)
            {
                dir = -1;

            }

        }
    }
}
