using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes the mouse for scenes not containing actual gameplay.
/// </summary>
public class MenuMouse : MonoBehaviour
{
    public Texture2D mouseTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(mouseTexture, hotSpot, cursorMode);
    }
}
