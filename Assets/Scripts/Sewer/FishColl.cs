using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script attaches to the threee collectibles.  When the player merges with collectible, the bool in fishUI 
/// "fishactivate" is turned to true and the next collectible game object appears.
/// The fish collectible just recieved is then destroyed.
/// If there are no more collectibles the fish is simply destroyed.
/// </summary>
public class FishColl : MonoBehaviour
{

    [Tooltip("Assign to Fish UI")]
    public FishUI fishUI = null;
    [SerializeField, Tooltip("Assign to next fish")]
    private GameObject nextFish = null;



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fishUI.fishActivate = true;

            if (nextFish != null)
            {
                nextFish.SetActive(true);
                GameObject.Destroy(this.gameObject);
            }

            GameObject.Destroy(this.gameObject);


        }
    }

}
