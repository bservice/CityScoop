using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script allows the user to make dialogue appear as soon as the scene it's on is loaded.
/// A few notes about it, since it's a little finicky:
///     1) Always put this on the background in the scene.
///     2) "numLinesBeforeBackgroundChange" should only be altered if you want the background to start off black (like with the intro).
///         If that isn't the desired affect, then don't change it (when at 0, it doesn't do anything)!
///     3) This script always runs last, and will only work when the GameManager is present. The GameManager only starts out on 
///         the main menu though, so the game must be started from there to test it.
/// </summary>
public class EnterScene : MonoBehaviour
{
    // The actual dialogue that will play at the start of the scene.
    public Dialogue dialogue;
    // The position of the conditional variable in the GameManager that will be turned on after this runs
    //  (ensures the dialogue doesn't play EVERY time the scene is entered).
    public int conditional;

    // How many lines before the background changes (if empty, nothing happens).
    public int numLinesBeforeBackgroundChange = 0;
    // The darkness that will cover the screen during this dialogue.
    public GameObject darkness;
    // How many starting lines of dialogue have happened.
    private int numLines = 0; 

    // Start is called before the first frame update
    void Start()
    {
        // If this dialogue has never been displayed...
        if (FindObjectOfType<GameManager>().conditionalBools[conditional] == false)
        {
            // If the user wants the background to change...
            if (numLinesBeforeBackgroundChange > 0)
            {
                // Enable the darkness.
                darkness.GetComponent<SpriteRenderer>().enabled = true;
            }

            // Start the dialogue.
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            // Mark that the condition has been met so it doesn't play again.
            FindObjectOfType<GameManager>().conditionalBools[conditional] = true;
        }
    }

    private void Update()
    {
        // If enough dialogue has passed...
        if(numLines >= numLinesBeforeBackgroundChange)
        {
            // Disable the darkness.
            darkness.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Increments the number of lines of this dialogue that have already happened.0
    public void Increment()
    {
        numLines++;
    }
}
