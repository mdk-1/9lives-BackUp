using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script to apply to climbable gameobjects, will only climb up
public class ClimbableObjects : MonoBehaviour
{
    public float ladderSpeed = 20f;
    public static bool climbCheck = false;

    //detect climbale object collision with player
    //if player pushes up, player moves up the object
    //no gravity change, player will fall back down without holding up
    //toggle climb check to true
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (Input.GetAxis("Vertical") > 0)
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ladderSpeed);
                //other.GetComponent<Animator>().SetBool("IsClimbing", true);
                climbCheck = true;
            }
        }
        //no reverse input as it was bugging falling animation
    }
    //detech when player leaves climbable object collider, reset rigidbody velocity
    //toggle climb check
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //other.GetComponent<Animator>().SetBool("IsClimbing", false);
            climbCheck = false;
        }
    }
}
