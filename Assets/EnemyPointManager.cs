using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointManager : MonoBehaviour
{
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
          if (other.GetComponent<SpriteRenderer>().flipX == true)
            {
                other.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (other.GetComponent<SpriteRenderer>().flipX == false)
            {
                other.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
