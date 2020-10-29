using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour 
{ 
    //very simple script that allows me to destory gameobjects after X amount of time

    public float lifetime; //need a variable to trigger the destruction of gameobject after it's no longer needed

    private void Start()
    {
        Destroy(gameObject, lifetime); //simple statement to destory a gameobject no longer required after it's "lifetime"
    }
}
