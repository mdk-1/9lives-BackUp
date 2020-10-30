using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FishUI : MonoBehaviour
{
    private Color fishTransition;
    [SerializeField,Tooltip ("Attach to Fishes")]
    private Image[] fishys;
    public bool fishActivate;
    private int index;
    private void Start()
    {
    fishActivate = false;
     index = 0;
    
    }
    private void Update()
    {
        FishFade();
    }
    /// <summary>
    /// Changes the Fish UI Color when each Fish is collectect.
    ///
    /// </summary>
    private void FishFade()
    {
        if (index < fishys.Length)
        {
            if (fishActivate==true)
            {
               fishTransition = new Color(255, 255, 255, 255);
                fishys[index].color = fishTransition;
                fishActivate = false;          
                index++;
              
            }
        }
    }
}
