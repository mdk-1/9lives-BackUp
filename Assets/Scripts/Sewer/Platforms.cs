using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    #region Timer
    [Header("Timers")]
    [SerializeField, Tooltip("Timer for States")]
    private float countdownTimer = 10;
    #endregion
    #region Platforms
    [Header("Platforms")]
    [SerializeField, Tooltip("Assign to Platform")]
    private GameObject[] platforms;

    public int platformDir = 0;
    private float platformSpeed = 10f;

    #endregion
    #region State Machine
    [Header("State Machine")]
    [SerializeField, Tooltip("What the Current State is?")]
    private State currentState;

    private enum State
    {
        Still,
        Horizontal,
        Vertical,
        Tippy,
    }


    #endregion
    void InitialiseVariables()
    {
        countdownTimer = 10;
        currentState = State.Still;
        platformDir = 1;
    }
    private void Start()
    {
        InitialiseVariables();
        NextState();

    }
    private void Update()
    {
        countdownTimer -= Time.deltaTime;
    }
    #region StateMachine States
    IEnumerator StillState()
    {
        //loop while in state
        while (currentState == State.Still)
        {

            if (countdownTimer <= 0)
            {
                currentState = State.Horizontal;
                countdownTimer = 40;
            }


            yield return 0;//returns to reset loop
        }
        NextState(); //switches to next state as determined by if condition above
    }
    IEnumerator HorizontalState()
    {
        //loop while in state
        while (currentState==State.Horizontal)
        {
            PlatformHorizontal();

            if (countdownTimer<=0)
            {
                currentState = State.Vertical;
                countdownTimer = 20;
            }


            yield return 0;//returns to reset loop
        }
        NextState(); //switches to next state as determined by if condition above
    }

    private void PlatformHorizontal()
    {

        platforms[0].transform.position = new Vector2(platforms[0].transform.position.x + Time.deltaTime * platformDir * -1 * platformSpeed, platforms[0].transform.position.y);
        platforms[1].transform.position = new Vector2(platforms[1].transform.position.x + Time.deltaTime *0.8f*platformDir * platformSpeed, platforms[1].transform.position.y);
        platforms[2].transform.position = new Vector2(platforms[2].transform.position.x + Time.deltaTime *0.7f* platformDir * -1 * platformSpeed, platforms[2].transform.position.y);
        if (platforms[0].transform.position.x < -35)
        {
            platformDir = -1;
        }
        if (platforms[0].transform.position.x >5)
        {
            platformDir = 1;
        }
    }

    IEnumerator VerticalState()
    {
        //loop while in state
        while (currentState == State.Vertical)
        {

            if (true)
            {

            }


            yield return 0;//returns to reset loop
        }
        NextState(); //switches to next state as determined by if condition above
    }
    IEnumerator TippyState()
    {
        //loop while in state
        while (currentState == State.Tippy)
        {

            if (true)
            {

            }


            yield return 0;//returns to reset loop
        }
        NextState(); //switches to next state as determined by if condition above
    }
    #endregion
    void NextState()
    {
        string methodName = currentState.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                    System.Reflection.BindingFlags.NonPublic |
                                    System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

}
