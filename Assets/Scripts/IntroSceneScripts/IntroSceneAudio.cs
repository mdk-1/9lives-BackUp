using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneAudio : MonoBehaviour
{

    [SerializeField]
    private AudioSource typewriter;
    [SerializeField]
    private AudioSource meow;
    public bool typingOn;
    public bool meowOn;


    private void Awake()
    {
        typingOn = false;

    }
    private void Update()
    {
        if (typingOn)
        {
        
            typewriter.Play();
            typingOn = false;

        }
    
    }
    public void ContinueSound()
    {
        typingOn = true;
    }
    public void MeowSound()
    {
        meow.Play();
        meowOn = true;
    }
}
