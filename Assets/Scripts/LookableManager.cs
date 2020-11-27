using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookableManager : MonoBehaviour
{
    // The list of all lookable objects.
    public Lookable[] lookables;
    // Whether or not the cursor is over a lookable object.
    public bool isHovered = false;

    // Cursor Controls
    public Texture2D normalTexture;
    public Texture2D hoverTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;

    //Pause menu
    public PauseTest pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        //Following code prevents more than one inventory from being created
        lookables = FindObjectsOfType<Lookable>();
        /*
        if (lookables.Length > 1)
        {
            Destroy(lookables[1]);
            Destroy(lookables[1].gameObject);
        }*/
        for(int i = 0; i < lookables.Length; i++)
        {
            lookables[i].cursorTexture = hoverTexture;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.Paused)
        {
            foreach (Lookable l in lookables)
            {
                if (l.isHovered = true)
                {
                    Cursor.SetCursor(hoverTexture, hotSpot, cursorMode);
                    return;
                }
            }
            Cursor.SetCursor(normalTexture, hotSpot, cursorMode);
        }
    }
}
