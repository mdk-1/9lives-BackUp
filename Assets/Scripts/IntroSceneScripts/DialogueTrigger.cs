using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// script to pass through dialogue from dialouge script across to dialouge manager script

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    public void Awake() // method to pass through dialogue across to dialoguemanager
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
