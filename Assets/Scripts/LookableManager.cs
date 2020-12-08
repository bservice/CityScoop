using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookableManager : MonoBehaviour
{
    // The list of all lookable objects.
    public Lookable[] lookables;
    // THe list of all NPC objects.
    public NPC[] npcs;
    // Whether or not the cursor is over a lookable object.
    public bool isHovered = false;

    // Cursor Controls
    public Texture2D normalTexture;
    public Texture2D searchTexture;
    public Texture2D speakTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;

    //Pause menu
    public PauseTest pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        //Following code prevents more than one inventory from being created
        lookables = FindObjectsOfType<Lookable>();
        npcs = FindObjectsOfType<NPC>();

        // Giving each lookable it's mouse textures.
        for(int i = 0; i < lookables.Length; i++)
        {
            lookables[i].specialTexture = searchTexture;
            lookables[i].normalTexture = normalTexture;
        }
        // Giving each NPC it's mouse textures.
        for (int i = 0; i < npcs.Length; i++)
        {
            npcs[i].specialTexture = speakTexture;
            npcs[i].normalTexture = normalTexture;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
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
        */
    }
}
