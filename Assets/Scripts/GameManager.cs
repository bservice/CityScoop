using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Array to hold the managers, this will be used to keep it at one
    private GameManager[] allManagers;

    /* Conditional Booleans */
    // Easier to have a list of bools than seperate bools.
    // Each place in the list represents a different condition:
    //  0 - Whether or not the player has heard the office dialogue.
    //  1 - Whether or not the player has gone to the pizzeria.
    //  2 - Whether or not the player has spoken to the ex-delivertman.
    //  3 - Whether or not you've talked to doodle.
    //  4 - Whether or not the Bicorn has been helped.
    public bool[] conditionalBools;

    // Start is called before the first frame update
    void Start()
    {
        //Limit to one game manager in the game
        allManagers = FindObjectsOfType<GameManager>();
        if (allManagers.Length > 1)
        {
            Destroy(allManagers[1]);
            Destroy(allManagers[1].gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        conditionalBools = new bool[5];
        for(int i = 0; i < conditionalBools.Length; i++)
        {
            conditionalBools[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Getting the name of this scene.
        string sceneName = SceneManager.GetActiveScene().name;
        
        switch(sceneName)
        {
            case "Office":
                
                break;
        }
        */
    }
}