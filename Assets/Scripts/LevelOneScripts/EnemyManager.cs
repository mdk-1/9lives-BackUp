using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region varaibles
    [Header("AI Control & Calibration")]
    public float aiSpeed = 5.0f; //speed of AI enemy
    private float aiFleeSpeed = 30f;
    public int aiHealth = 2; // health points of AI enemy
    public float minDistance; // variable to hold a distance in float - use to calculate distance between gameObjects
    public int index = 0; // varible to hold index in int - used for navigating between waypoints
    public float chasePlayerDistance = 20f; // variable to hold distance in float - used later for chase state
    private float timeOfDeath = 3f;
    private bool faceLeft = false;

    //reference to sprite transform
    public Transform aiTransform;
    //reference to animator
    public Animator anim;
    //reference to rigidbody
    private Rigidbody2D aiRigidBody;

    //gameObject references
    public GameObject[] waypoint; //waypoints for patrol state
    public GameObject player; //referencing player object
    public GameObject bloodSplat; //reference for particle effect 

    #endregion

    #region ai states
    public enum State //states for aiEnemy
    {
        patrol, //patrols to waypoints
        chase, //chase player
        flee, //flees after hit
        recover, //ai recovers from hit 
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
            AiWayPointDirection();
            anim.SetBool("IsCrouching", false);
            anim.SetBool("IsRun", true);
            Patrol(); // calling patrol method
            yield return null; //yeilding no return
            if ((Vector2.Distance(player.transform.position, this.transform.position) < chasePlayerDistance)) //if current distance between player and AI is less than chase variable switch state to chase
            {
                state = State.chase; //switching state to chase
            }
        }
        NextState(); //calling next state method

    }
    //chase state ()
    private IEnumerator chaseState()
    {
        while (state == State.chase)
        {
            AiDirection();
            anim.SetBool("IsRun", true);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), aiSpeed * Time.deltaTime);
            yield return null;

            if ((Vector2.Distance(player.transform.position, this.transform.position) > chasePlayerDistance)) // if player moves out of chase range, or ai is low HP
            {
                state = State.patrol; //return to patrol state
            }
        }
        NextState();
    }
    private IEnumerator fleeState()
    {
        while (state == State.flee)
        {
            //AiDirection();
            anim.SetBool("IsCrouching", false);
            anim.SetBool("IsRun", true);
            if (Vector2.Distance(player.transform.position, transform.position) < chasePlayerDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-player.transform.position.x, transform.position.y), aiFleeSpeed * Time.deltaTime);
            }

            if (Vector2.Distance(player.transform.position, transform.position) > chasePlayerDistance)
            {
                state = State.recover;
            }
            yield return null;
        }
        NextState();
    }
    private IEnumerator recoverState()
    {
        while (state == State.recover)
        {
            anim.SetBool("IsRun", false);
            anim.SetBool("IsCrouching", true);       
            yield return new WaitForSeconds(2);

            if (Vector2.Distance(player.transform.position, transform.position) > chasePlayerDistance)
            {
                state = State.patrol;
            }
            else if (Vector2.Distance(player.transform.position, transform.position) > chasePlayerDistance)
            {
                state = State.flee;
            }
        }
        NextState();
    }
    
    private IEnumerator deathState()
    {
        yield return new WaitForSeconds(timeOfDeath);
        Destroy(this.gameObject);
    }
    #endregion

    #region methods
    public void Patrol() //patrol method (used within patrol state)
    {
        float distance = Vector2.Distance(transform.position, waypoint[index].transform.position); //adding position and waypoint index into distance variable
        if (distance < minDistance) //if distance is less than minimum distance (if not near gameobject to consider)
        {
            index = Random.Range(0, waypoint.Length); //bounce between random waypoints
        
        }
        //rotate sprite and move in direction of next waypoint
        Vector2 dir = waypoint[index].transform.position - this.transform.position; //caculate direction to face against next waypoint
        if (dir.magnitude > .05f) //if direction magnitude is greater than distance
        {
            transform.Translate(dir.normalized * aiSpeed * Time.deltaTime, Space.World); //move to next waypoint
            //targetRot = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg; //calculate rotation of next waypoint
            //aiTransform.rotation = Quaternion.Euler(new Vector3(0, targetRot, 0)); // rotate ai to face direction next waypoint on Y axis
        }
    }
    public void OnAiDeath()
    {
        Instantiate(bloodSplat, transform.position, Quaternion.identity);
        anim.SetBool("IsRun", false);
        anim.SetBool("IsDead", true);
        StartCoroutine(deathState());
    }
    public void OnAiHit()
    {
        state = State.flee;
    }
    public void AiDirection()
    {
        if (transform.position.x < player.transform.position.x && faceLeft)
        {
            AiFlip();
        }
        else if (transform.position.x > player.transform.position.x && !faceLeft)
        {
            AiFlip();
        }
    }
    public void AiWayPointDirection()
    {
        if (transform.position.x < waypoint[index].transform.position.x && faceLeft)
        {
            AiFlip();
        }
        if (transform.position.x > waypoint[index].transform.position.x && !faceLeft)
        {
            AiFlip();
        }
    }
    private void AiFlip()
    {
        faceLeft = !faceLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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