﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string name;
    private Inventory inventory;
    public InteractingObjects interObjRef;

    Vector2 cursorPosition;

    private bool added;

    private PauseTest pauseMenu;

    private AudioSource soundEffect;

    public string Name
    {
        get { return name; }
    }

    public bool Added
    {
        get { return added; }
        set
        {
            added = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        inventory = FindObjectOfType<Inventory>();
        pauseMenu = FindObjectOfType<PauseTest>();
        added = false;
        soundEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.Paused)
        {
            CheckForClick();
            UseItem("Target");
        }
    }

    //Method to check for click
    public bool CheckForClick()
    {
        //Grab vector2 for cursor to use in AABB math
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        //Selection for objects
        if (Input.GetMouseButtonDown(0))
        {
            //AABB collision test for cursor
            if (cursorPosition.x < this.GetComponent<BoxCollider2D>().bounds.max.x && cursorPosition.x > this.GetComponent<BoxCollider2D>().bounds.min.x)
            {
                //Potential collision!
                //Check the next condition in a nested if statement, just to not have a ton of &'s and to be more efficient
                if (cursorPosition.y > this.GetComponent<BoxCollider2D>().bounds.min.y && cursorPosition.y < this.GetComponent<BoxCollider2D>().bounds.max.y)
                {
                    //Collision!

                    //If the item is clicked on again after it's been added to the inventory, remove it
                    if(added)
                    {
                        transform.position = new Vector2(100.0f, 100.0f);
                        interObjRef.Temp = this;
                        inventory.RemoveItem(this);
                    }
                    else
                    {
                        //Checking to make sure the inventory isn't full
                        if (inventory.Count < 16)
                        {
                            //Add to inventory
                            inventory.AddItem(this);
                            added = true;
                            soundEffect.PlayOneShot(soundEffect.clip);
                        }
                    }
                    return true;
                }
            }
        }
        return false;
    }

    // Triggers dialogue about the pickup.
    public void OnMouseDown()
    {
        if(!added)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    //To pull an item from your inventory to use or of the ground
    public void UseItem(Vector2 target, string tag)
    {
        Debug.Log("HIT");
        //Validating item
        if (this.tag != tag)
        {
            return;
        }

        //To store the mouses position
        Vector2 locationOfMouse = Input.mousePosition;

        //To see if they are clicking if so have that object follow the cursor
        if (this.CheckForClick())
        {
            this.transform.position = locationOfMouse;

            //to see if the item is near the target zone
            if (Vector2.Distance(this.transform.position, target) <= 2.0f)
            {
                //Do something with the target zone and/or the object in use
            }
        }
    }

    //To pull an item from your inventory to use or of the ground using tags
    public void UseItem(string tag)
    {
        //Validating item
        if (this.tag != tag)
        {
            //return;
        }
        if(!inventory.HaveItem(this.name))
        {
            //Debug.Log("HIT return");
            //return;
        }

        //To store the mouses position
        Vector2 locationOfMouse = Input.mousePosition;
        //Grab vector2 for cursor to use in AABB math
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        //To get an item with the desired tag
        GameObject taggedItem;
        taggedItem = GameObject.FindWithTag(tag);

        //To see if they are clicking if so have that object follow the cursor
        //if (this.CheckForClick())
        //{
        //    this.transform.position = locationOfMouse;
        //
        //    //to see if the item is near the target zone
        //    if (Vector2.Distance(this.transform.position, taggedItem.transform.position) <= 2.0f)
        //    {
        //        //Do something with the target zone and/or the object in use
        //        Debug.Log("HIT ZONE");
        //    }
        //}
        //Selection for objects
        if (Input.GetMouseButton(1))
        {
            //AABB collision test for cursor
            if (cursorPosition.x < this.GetComponent<BoxCollider2D>().bounds.max.x && cursorPosition.x > this.GetComponent<BoxCollider2D>().bounds.min.x)
            {
                //Potential collision!
                //Check the next condition in a nested if statement, just to not have a ton of &'s and to be more efficient
                if (cursorPosition.y > this.GetComponent<BoxCollider2D>().bounds.min.y && cursorPosition.y < this.GetComponent<BoxCollider2D>().bounds.max.y)
                {
                    //Collision!
                    this.transform.position = cursorPosition;
                    
                    //to see if the item is near the target zone
                    if (Vector2.Distance(this.transform.position, taggedItem.transform.position) <= 0.2f)
                    {
                        //Do something with the target zone and/or the object in use
                        Debug.Log("HIT ZONE");
                    }
                }
            }
        }
    }
}
