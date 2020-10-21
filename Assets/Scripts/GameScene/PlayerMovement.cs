using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script to handle character prefab movement input and animations

public class PlayerMovement : MonoBehaviour
{
    //player movement variables
    [Header("Player Movement Variables")]
    [Space]
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool wasCrouched = false;

    //reference character controller
    public CharacterController2D controller;
    //reference animator 
    public Animator animator;

    //intialise animator
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //get input from player for movement, store in horizontalMove
    //set animation state based on player movement speed
    void Update()
    {
        //running input/animation
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); //animation float value Speed must always be positive - use absolute Mathf to convert

        //jumping input/animation
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
            jump = true;
        }

        //crouching input toggle control
        if (Input.GetButtonDown("Crouch"))
        {
            if (wasCrouched == false)
            {
                crouch = true;
                wasCrouched = true;
            }
            else if (wasCrouched == true)
            {
                crouch = false;
                wasCrouched = false;
            }
        }

        //climbing input/animation
        if (ClimbableObjects.climbCheck == true)
        {
            if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
            {
                animator.SetBool("IsClimbing", true);
             }
        }
        else if (ClimbableObjects.climbCheck == false)
            {
                animator.SetBool("IsClimbing", false);
            }

        //falling animation
        if (controller.isFalling == true && ClimbableObjects.climbCheck == false)
        {
            animator.SetBool("IsFalling", true);
        }
        else if (controller.isFalling == false)
        {
            animator.SetBool("IsFalling", false);
        }
    }
    //landing event after jumping/climbing/falling
    public void OnLanding()
    {
        //set jumping animation to false on land
        animator.SetBool("IsJumping", false);

        //set climbing animation to false on land
        animator.SetBool("IsClimbing", false);

        //if player moves horizontal while falling disable falling check and falling animation on landing
        controller.isFalling = false;
        animator.SetBool("IsFalling", false);
    }
    //crouching animation event
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    // move player with controller, based on input within horizontalMove
    // check if player is crouching or jumping
    // check is player is falling
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, crouch, jump);
        controller.Falling();
        jump = false;
    }
}
