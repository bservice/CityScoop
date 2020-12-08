using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookableManager : MonoBehaviour
{
    // The list of all lookable objects.
    public Lookable[] lookables;
    // The list of all NPC objects.
    public NPC[] npcs;
    // The list of all Pickups
    public PickUp[] pickUps;

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
        // Finding all of the objects that can be looked at or interacted with.
        lookables = FindObjectsOfType<Lookable>();
        npcs = FindObjectsOfType<NPC>();
        pickUps = FindObjectsOfType<PickUp>();

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
        // Giving each pickup it's textures.
        for (int i = 0; i < pickUps.Length; i++)
        {
            pickUps[i].specialTexture = searchTexture;
            pickUps[i].normalTexture = normalTexture;
        }
    }
}
