using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCats : MonoBehaviour
{
    public Animator anim;
    public GameObject bloodEffect;
    private float catSpeed = 20f;

    private void Start()
    {
        anim.SetBool("IsRun", true);
    }
    void Update()
    {
        transform.Translate(Vector2.left * catSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D hit) 
    {
        if (hit.CompareTag("Player")) 
        {
            //any effects played here
            GameManager.instance.health = -1;
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
