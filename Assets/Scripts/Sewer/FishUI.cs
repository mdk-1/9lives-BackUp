using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FishUI : MonoBehaviour
{
    public Platforms platforms;
    private Color fishTransition;
    [SerializeField, Tooltip("Attach to Fishes")]
    private Image[] fishys;
    public bool fishActivate;
    private int index;

    private bool sewerActivate;
    private float sewerCount;

    [SerializeField]
    private GameObject[] sewerPipes;

    public AudioSource plinkSource;
    public AudioSource pipeBurstSource;



    private void Start()
    {
        fishActivate = false;
        index = 0;
        sewerCount = 0.25f;

    }
    private void Update()
    {
        FishFade();
        SewerBurst();
        if (index>=fishys.Length)
        {
            platforms.escapeCat = true;
        }
    }

    private void SewerBurst()
    {
        if (sewerActivate)
        {
            if (sewerCount<=0)
            {
                sewerPipes[index].SetActive(false);
                sewerCount = 0.25f;
                sewerActivate = false;
                index++;
            }
            else
            {
                sewerCount -= Time.deltaTime;
            }
        }
    }
    /// <summary>
    /// Changes the Fish UI Color when each Fish is collected.
    ///
    /// </summary>
    private void FishFade()
    {
        if (index < fishys.Length)
        {
            if (fishActivate == true)
            {
                plinkSource.Play();
                pipeBurstSource.Play();
                fishTransition = new Color(255, 255, 255, 255);
                fishys[index].color = fishTransition;
                fishActivate = false;
                sewerActivate = true;

            }
        }
    }
}
