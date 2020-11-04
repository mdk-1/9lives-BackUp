using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

// script to display dialgue in correct order & transition to scene on end

public class DialogueManager : MonoBehaviour
{
    public Text nameText; //variable for 'speaker' name to be displayed
    public Text dialogueText; //variable for 'dialogue' to be displayed
    public string levelToLoad;

    private Queue<string> sentences; //variable to hold and track all dialogue displayed -- using queue as string

    void Awake() //calling awake to initialise as soon as the scene loads
    {
        sentences = new Queue<string>(); // initilising the queue of dialogue
    }
    public void StartDialogue(Dialogue dialogue) //method to call to start dialogue -- can be placed on any gameobject however this is going to be triggered on scene load.
    {
        nameText.text = dialogue.name; // display the name text variable 'speaker' into the UI element

        sentences.Clear(); //clear anything stored before looping anything else in

        foreach(string sentence in dialogue.sentences) // queue up anything in sentences to be displayed as dialogue
        {
            sentences.Enqueue(sentence); // add next sentence to the dialogue queue
        }
        DisplayNextSentence(); // call method to display the next sentence 
    }
    public void DisplayNextSentence() //method to display next sentence in queue
    {
        if (sentences.Count == 0) //check if there are sentences in queue - if none then end
        {
            EndDialogue(); //if true, call end dialogue method
            return; // and return out of method 
        }

        string sentence = sentences.Dequeue(); // remove sentence of dialogue from queue to be displayed in order
        StopAllCoroutines(); //need to stop all text animation effects if continue button is pressed too quickly
        StartCoroutine(TypeSentence(sentence)); // call a coroutine method to give the effect of text animation on the speech box

    }

    IEnumerator TypeSentence (string sentence) // numerator method will be used to display the dialogue letter by letter
    {
        dialogueText.text = ""; // dialogue text should be empty to give typing effect
        foreach (char letter in sentence.ToCharArray()) //loop through each 'letter' of the dialogue/sentence and convert to a character array
        {
            dialogueText.text += letter; //append letter to end of the string
            yield return null; // wait a single frame for each letter to appear
        }
    }

    void EndDialogue()
    {
        Debug.Log("end of dialogue");
        StartGame();

    }
    void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}

