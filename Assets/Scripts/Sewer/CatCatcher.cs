using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Defines all Cat Catcher Movements and Damages.  Cat Catcher will run in a direction. if the cat catcher hits a boundary 
/// and gets stuck,cat catcher will turn around
/// If cat catcher gets close to player cat catcher will chase player
/// if cat catcher hits player, player takes damage, animation occurs and sound occurs
/// </summary>
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

    public float damage = 0;
    public GameObject bloodSplat;

    public float killCountDown;
    public float killCountDownReset;
    public AudioClip bloodSplatSound;
    public AudioSource audiosource;

    public float chasePlayerDistance;




    private void Start()
    {
        catSR = gameObject.GetComponent<SpriteRenderer>();

        dir = 1;
        distance = Vector2.Distance(transform.position, player.transform.position);
        killCountDown = 0;
        killCountDownReset = 0.9f;
        chasePlayerDistance = 1;


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
      
        if ((Vector2.Distance(player.transform.position, this.transform.position) < chasePlayerDistance))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x + Time.deltaTime * dir * moveSpeed, transform.position.y);
        }
        
       
        killCountDown -= Time.deltaTime;
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

        if (catPointTest <= zigga && catPointTest >= -zigga && transform.position.y > -22)
        {
            activate = true;
            catPointCountdown = 1;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Instantiate(bloodSplat, transform.position, Quaternion.identity);
            audiosource.PlayOneShot(bloodSplatSound, 0.75f);

            if (killCountDown<=0)
            {

                if (GameManager.instance.health >= 1)
                {
                    GameManager.instance.health -= 1;
                    killCountDown = killCountDownReset;
                    damage++;
                }
                else
                {
                 
                    SceneManager.LoadScene(7);
                }
           
             
               
                
            }
          
           
        }
    }
}

