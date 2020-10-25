using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingObjects : MonoBehaviour
{
    public float ladderSpeed = 20f;

    //detect climbale object collision with player
    //if player pushes up, player moves up the object
    //no gravity change, player will fall back down without holding up
    //toggle climb check to true
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Animator>().SetBool("IsClimbing", true);
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            ClimbableObjects.climbCheck = true;

            if (Input.GetAxis("Horizontal") > 0)
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(ladderSpeed, 0);                             
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(-ladderSpeed, 0);
            }
        }
    }
    //detech when player leaves climbable object collider, reset rigidbody velocity
    //toggle climb check
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            other.GetComponent<Rigidbody2D>().gravityScale = 3f;
            other.GetComponent<Animator>().SetBool("IsClimbing", false);
            ClimbableObjects.climbCheck = false;
        }
    }
}
