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
    public float catPointCountdown1 = 4;
    public float catPointCountdown2 = 2;
    public float a = 1;
    public float test;

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

        transform.position = new Vector2(transform.position.x + Time.deltaTime * dir * moveSpeed, transform.position.y);
    }

    private void TurnArouncCatcher()
    {
        catPointCountdown1 -= Time.deltaTime;
        catPointCountdown2 -= Time.deltaTime;

        if (catPointCountdown1 <= 0)
        {
            catPointCountdown1 = 3;
            catPoint1 = transform.position.x;

        }
        if (catPointCountdown2 <= 0)
        {
            catPointCountdown2 = 4;
            catPoint2 = transform.position.x;

        }
        test = catPoint2 - catPoint1;
        if (test <= 10 && test >= -10)
        {
            catPoint2 = 1000;
            if (dir == 1)
            {
                dir = -1;
                catSR.flipX = true;
            }
            else if (dir == -1)
            {
                dir = 1;
                catSR.flipX = false;
            }
        }
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
            if (transform.position.x > 0)
            {
                dir = -1;
                catSR.flipX = true;
            }

        }
    }
}
