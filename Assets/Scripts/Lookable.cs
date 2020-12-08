using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookable : MonoBehaviour
{
    // Cursor Controls
    public Texture2D specialTexture;
    public Texture2D normalTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

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

    
    private void Update()
    {
        //BoxCollider2D collider = this.GetComponent<BoxCollider2D>();
        //if(collider.IsTouching())
    }
}
