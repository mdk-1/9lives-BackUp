using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishColl : MonoBehaviour
{

    [SerializeField,Tooltip("Assign to Fish UI")]
    public FishUI fishUI=null;
    [SerializeField, Tooltip("Assign to next fish")]
    private GameObject nextFish=null;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hewp");
            fishUI.fishActivate = true;
            if (nextFish != null)
            {
                nextFish.SetActive(true);
            }
          
                GameObject.Destroy(this.gameObject);
         

        }
    }

}
