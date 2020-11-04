using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private IEnumerator attackState()
    {
        while (state == State.attack)
        {
            anim.SetBool("IsAttacking", true);
            if ((Vector2.Distance(player.transform.position, this.transform.position) < chasePlayerDistance))
            {
                catThrow = true;
                CatAttack();
                yield return new WaitForSeconds(bossCatSpawnCoolDown);
            }

            if ((Vector2.Distance(player.transform.position, this.transform.position) > chasePlayerDistance)) // if player moves out of chase range, or ai is low HP
            {
                state = State.patrol; //return to patrol state
            }
        }
        
        NextState();
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
    }
    public void OnAiDeath()
    {
        //death animation
        Instantiate(bloodSplat, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        SceneManager.LoadScene(8);
    }
    public void OnAiHit()
    {
        state = State.patrol;
    }
    public void CatAttack()
    {
        if (catThrow == true)
        {
            Instantiate(bossBlackCat, new Vector2(bossCatSpawner.transform.position.x, bossCatSpawner.transform.position.y), Quaternion.identity);
            catThrow = false;
        }
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