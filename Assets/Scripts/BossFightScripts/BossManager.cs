using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region varaibles
    [Header("AI Control & Calibration")]
    public float aiSpeed = 5.0f; //speed of AI enemy
    public int aiHealth = 2; // health points of AI enemy
    public float minDistance; // variable to hold a distance in float - use to calculate distance between gameObjects
    public int index = 0; // varible to hold index in int - used for navigating between waypoints
    public float chasePlayerDistance = 20f; // variable to hold distance in float - used later for chase state
    private float timeOfDeath = 3f;
    

    //reference to animator
    public Animator anim;
    //reference to rigidbody
    private Rigidbody2D aiRigidBody;

    //gameObject references
    //referencing player object
    public GameObject player; 
    //reference for boss cat spawns
    public GameObject bossBlackCat;
    public GameObject bossCatSpawner;
    private float bossCatSpawnCoolDown = 2f;
    private bool catThrow;
    //reference for particle effect 
    public GameObject bloodSplat; 
    //reference for waypoints
    public Transform[] patrolPoints;
    private int currentPoint;

    #endregion

    #region ai states
    public enum State //states for aiEnemy
    {
        patrol, //patrols to waypoints
        attack, //chase player
    }
    public State state; // referencing State as state

    void Start()
    {
        anim = GetComponent<Animator>();
        aiRigidBody = GetComponent<Rigidbody2D>();
        NextState(); // calling next state method on start to initialise AI states
    }
    //patrol state ()
    private IEnumerator patrolState()
    {
        while (state == State.patrol) //while state is compared to patrol state loop
        {
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsWalking", true);
            Patrol();
            yield return null;
            if ((Vector2.Distance(player.transform.position, this.transform.position) < chasePlayerDistance))
            {
                state = State.attack;
            }
        }
        NextState();

    }
    //chase state ()
    private IEnumerator attackState()
    {
        catThrow = true;
        while (state == State.attack)
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsAttacking", true);
            //throw cats here - instanciate cat prefeb
            
            if (catThrow == true)
            {
                Instantiate(bossBlackCat, new Vector2(bossCatSpawner.transform.position.x, bossCatSpawner.transform.position.y), Quaternion.identity);
                catThrow = false;
                yield return new WaitForSeconds(bossCatSpawnCoolDown);
            }

            if ((Vector2.Distance(player.transform.position, this.transform.position) > chasePlayerDistance)) // if player moves out of chase range, or ai is low HP
            {
                state = State.patrol; //return to patrol state
            }
        }
        
        NextState();
    }
    
    private IEnumerator deathState()
    {
        yield return new WaitForSeconds(timeOfDeath);
        Destroy(this.gameObject);
        //load outro scene here
    }
    #endregion

    #region methods
    public void Patrol() //patrol method (used within patrol state)
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

        //transform.position = new Vector3(Mathf.PingPong(boundary.xMin, boundary.xMax), transform.position.y, transform.position.z);
        /*float distance = Vector2.Distance(transform.position, waypoint[index].transform.position); //adding position and waypoint index into distance variable
        if (distance < minDistance) //if distance is less than minimum distance (if not near gameobject to consider)
        {
            index = Random.Range(0, waypoint.Length); //bounce between random waypoints
        
        }
        //rotate sprite and move in direction of next waypoint
        Vector2 dir = waypoint[index].transform.position - this.transform.position; //caculate direction to face against next waypoint
        if (dir.magnitude > .05f) //if direction magnitude is greater than distance
        {
            transform.Translate(dir.normalized * aiSpeed * Time.deltaTime, Space.World); //move to next waypoint
        }*/
    }
    public void OnAiDeath()
    {
        Instantiate(bloodSplat, transform.position, Quaternion.identity);
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsAttacking", false);
        StartCoroutine(deathState());
    }
    public void OnAiHit()
    {
        state = State.patrol;
    }
    public void NextState() //method to change aiState
    {
        // work out the name of the method we want to run
        string methodName = state.ToString() + "State"; //example: if our current state is walk then this will return "walkState" 

        //give us a variable so we run a method using it's name
        System.Reflection.MethodInfo info = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        //run the method 
        StartCoroutine((IEnumerator)info.Invoke(this, null));
        //using StartCoroutine means we can leave and come back to the method that is running
        //All Coroutines must return IEnumerator
    }

    #endregion
}