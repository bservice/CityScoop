using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Array to hold the managers, this will be used to keep it at one
    private GameManager[] allManagers;

    //End Condition
    bool hasTomatoe = false;
    bool hasPizzaBox = false;
    bool hasCheese = false;
    bool hasWater = false;
    bool pizzaMade = false;
    int counter = 0;

    DialogueManager dialogueManager;

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

        dialogueManager = FindObjectOfType<DialogueManager>();
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

        //If you have all the pieces
        if (hasCheese && hasPizzaBox && hasTomatoe && hasWater)
        {
            //If you are in the bathroom
            if (SceneManager.GetActiveScene().name == "Bathroom")
            {
                //You win
                pizzaMade = true;
            }
        }

        //If you have the pizza made and you are in the pizzeria
        if(pizzaMade && SceneManager.GetActiveScene().name == "Pizzeria")
        {
            counter++;
            //Play Dialouge



            //Play Credits
            if(dialogueManager.inDialogue == false && counter > 600)
            {
                SceneManager.LoadScene("Credits");
            }
            
        }
    }
}