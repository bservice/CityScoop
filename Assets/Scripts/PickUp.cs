using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string name;
    private Inventory inventory;

    Vector2 cursorPosition;

    private bool added;

    private PauseTest pauseMenu;    

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.Paused)
        {
            CheckForClick();
        }
    }

    //Method to check for click
    public void CheckForClick()
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
                        inventory.RemoveItem(this);
                    }
                    else
                    {
                        //Add to inventory
                        inventory.AddItem(this);
                        added = true;
                    }                    
                }
            }
        }
    }
}
