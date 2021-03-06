﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Array to hold the managers, this will be used to keep it at one
    private GameManager[] allManagers;

    //End Condition
    public bool hasTomatoe = false;
    public bool hasPizzaBox = false;
    public bool hasCheese = false;
    public bool hasWater = false;
    public bool pizzaMade = false;
    public bool bossTalkPizza = false;
    public bool talkedToEmployee = false;
    public bool talkedToCartman = false;
    public bool seenCup = false;
    public bool dropBar = false;
    int counter = 0;

    DialogueManager dialogueManager;

    private PickUp[] items;

    /* Conditional Booleans */
    // Easier to have a list of bools than seperate bools.
    // Each place in the list represents a different condition:
    //  0 - Whether or not the player has heard the office dialogue.
    //  1 - Whether or not the player has gone to the pizzeria.
    //  2 - Whether or not the player has spoken to the ex-delivertman.
    //  3 - Whether or not you've talked to doodle.
    //  4 - Whether or not the Bicorn has been helped.
    //  5 - Whether or not you've talked to boss in the pizzeria
    //  6 - Whether or not you've talked to employee in the pizzeria
    //  7 - Whether or not you've talked to cartman and got his story
    //  8 - Whether or not you've seen Doodle's cup
    //  9 - Whether or not you've finished talking to doodle
    //  10 - Whether or not you've shown your badge
    //  11 - Whether or not you've entered central park a never never
    //  12 - Whether or not you've talked to chicago fairy
    //  13 - Whether or not you've given orange the mitten
    //  14 - Whether or not you've talked to orange after
    //  15 - Whether or not you've given the crowbar
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
        conditionalBools = new bool[16];
        for(int i = 0; i < conditionalBools.Length; i++)
        {
            conditionalBools[i] = false;
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
        items = FindObjectsOfType<PickUp>();
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
        items = FindObjectsOfType<PickUp>();
        if (conditionalBools[14])
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Name == "Crowbar")
                {
                    items[i].transform.position = new Vector2(-1.365167f, 0.4749857f);
                }
            }
            conditionalBools[14] = false;
        }

        //Reset conditionals if the main menu is the active scene
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            for (int i = 0; i < conditionalBools.Length; i++)
            {
                conditionalBools[i] = false;
                hasTomatoe = false;
                hasPizzaBox = false;
                hasCheese = false;
                hasWater = false;
                pizzaMade = false;
                bossTalkPizza = false;
                talkedToEmployee = false;
                talkedToCartman = false;
                seenCup = false;
                dropBar = false;
            }
        }
    
        //If you have all the pieces
        if (hasCheese && hasPizzaBox && hasTomatoe && hasWater)
        {
            //If you are in the bathroom
            if (SceneManager.GetActiveScene().name == "Bathroom")
            {
                //Pizza win
                pizzaMade = true;
            }
        }

        //If you have the pizza made and you are in the pizzeria
        if(pizzaMade && SceneManager.GetActiveScene().name == "Pizzeria")
        {
            counter++;
            //Play Dialouge



            //Play Credits
            if(/*dialogueManager.inDialogue == false &&*/ counter > 600)
            {
                SceneManager.LoadScene("Credits");
            }
            
        }
    }
}