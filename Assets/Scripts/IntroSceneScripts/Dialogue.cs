using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to handle dialouge input

[System.Serializable] // serialize entire script
public class Dialogue //this does not need to be monobehaviour as it will be called elsewhere
{
    public string name; //going to be the label of the speech box - useful for giving names to characters or speakers

    [TextArea(3, 10)] //attribute to add min/max lines for text area
    public string[] sentences; //need to store dialogue for displaying in order
}
