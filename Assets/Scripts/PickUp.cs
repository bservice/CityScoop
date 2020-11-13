using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string name;
    private Inventory inventory;

    Vector2 cursorPosition;

    public string Name
    {
        get { return name; }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForClick();
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
                    //Add to inventory
                    inventory.AddItem(this);
                    transform.position = new Vector2(100.0f, 100.0f);
                }
            }
        }
    }
}
