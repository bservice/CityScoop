using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookable : MonoBehaviour
{
    // Whether or not the cursor is over this lookable object.
    public bool isHovered = false;

    // Cursor Controls
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        isHovered = true;
    }

    void OnMouseExit()
    {
        //Cursor.SetCursor(null, Vector2.zero, cursorMode);
        isHovered = false;
    }

    public void OnMouseDown()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
