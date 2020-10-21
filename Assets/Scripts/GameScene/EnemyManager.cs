using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region varaibles
    [Header("AI Control & Calibration")]
    public float aiSpeed = 5.0f; //speed of AI enemy
    public int aiHealth = 20; // health points of AI enemy
    public float minDistance; // variable to hold a distance in float - use to calculate distance between gameObjects
    public int index = 0; // varible to hold index in int - used for navigating between waypoints
    public float chasePlayerDistance = 5f; // variable to hold distance in float - used later for chase state
    public float damagePlayerDistance = 1f; // variable to hold distance in float - used later to do damage to player while in chase state


    public Animator anim;

    private Rigidbody2D aiRigidBody;

    //gameObject references
    public GameObject[] waypoint; //way points for patrol state
    public GameObject player; //referencing player object
    public GameObject aiBase; //referencing AI base object

    #endregion

    #region ai states
    public enum State //states for aiEnemy
    {
        patrol, //patrols to 3 different gameobjects, labelled as waypoints
        chase, //chases player gameobject, if within range will do damage over time
        flee, // if less than 25% health, AI will flee back to AI base for repairs
        death, // death state
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
            Patrol(); // calling patrol method
            yield return null; //yeilding no return
            if ((Vector2.Distance(player.transform.position, this.transform.position) < chasePlayerDistance)) //if current distance between player and AI is less than chase variable switch state to chase
            {
                state = State.chase; //switching state to cahse
            }
            //if player manages to hit AI that isn't in chase state, AI should return to base to repair
            if (aiHealth < 6) //if current health of AI is less than 6 (magic number for testing purposes) should have variable to hold 25% value of AI health
            {
                state = State.flee; //switch to flee state
            }
        }
        NextState(); //calling next state method

    }
    //chase state ()
    private IEnumerator chaseState()
    {
        while (state == State.chase)
        {
            MoveAi(player.transform.position);
            //do damage here
            yield return null;
            if ((Vector2.Distance(player.transform.position, this.transform.position) > chasePlayerDistance)) // if player moves out of chase range 
            {
                state = State.patrol; //return to patrol state
            }
            if (aiHealth < 6) // if AI health is less than 6 (magic number for testing purposes) should have variable to hold 25% value of AI healt
            {
                state = State.flee; //switch to flee state
            }
        }
        NextState();
    }
    private IEnumerator fleeState()
    {
        while (state == State.flee) // while loop for being in flee state
        {
            MoveAi(aiBase.transform.position); //move towards AI base
            yield return null; //must return yeild within IEnumerator 
            if (aiHealth == 20) // if AI at full HP (again, should not have magic number here - testing purposes)
            {
                state = State.patrol; // return to patrol state
            }
        }
        NextState();
    }
    private IEnumerator deathState()
    {
        if (state == State.death)
        {
            Death();
            yield return null;
            //destory gameobject
        }
    }
    #endregion

    #region methods
    public void Patrol() //patrol method (used within patrol state)
    {
        float distance = Vector2.Distance(transform.position, waypoint[index].transform.position); //adding position and waypoint index into distance variable
        anim.SetBool("IsRun", true);

        if (distance < minDistance) //if distance is less than minimum distance (if not near gameobject to consider)
        {
            index = Random.Range(0, waypoint.Length); //bounce between random waypoints

        }

        MoveAi(waypoint[index].transform.position); //move to next waypoint
    }

    void MoveAi(Vector2 targetPosition) // basic move method to get the AI moving
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, aiSpeed * Time.deltaTime);


    }
    void Death()
    {
        anim.SetBool("IsRun", false);
        anim.SetBool("IsDead", true);
        //destory gameobject
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
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has landed");
            state = State.death;
        }
        
    }
    #endregion
}