using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCatcherManagerLevelThree : MonoBehaviour
{
    public GameObject player;
    public GameObject bloodEffect;
    private float aiSpeed = 15f;

    // Update is called once per frame
    void Update()
    {

     this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, transform.position.y), aiSpeed * Time.deltaTime);


    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(bloodEffect, player.transform.position, Quaternion.identity);
            GameManager.instance.health = -1;
        }
    }

}
