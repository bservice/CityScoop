using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a bit confusing, so allow me to explain!
/// </summary>
public class NPC : MonoBehaviour
{
    // After the input triggers, the dialogue will change.
    public int[] conditionsThatChangeDialogue;
    // The dialogue to change to.
    public Dialogue[] dialogues;

    // Mouse stuff
    public Texture2D specialTexture;
    public Texture2D normalTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        // For each of the conditions that will change this NPCs dialogue...
        for(int i = 0; i < conditionsThatChangeDialogue.Length; i++)
        {
            // Check to see if that condition has been met. If so...
            if(FindObjectOfType<GameManager>().conditionalBools[conditionsThatChangeDialogue[i]] == true)
            {
                // The dialogue will change.
                GetComponent<DialogueTrigger>().dialogue = dialogues[i];
            }
        }
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(specialTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(normalTexture, hotSpot, cursorMode);
    }

    public void OnMouseDown()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
