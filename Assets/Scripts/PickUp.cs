using System.Collections;
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

    // Cursor Controls
    public Texture2D specialTexture;
    public Texture2D normalTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Dialogue for the item once it's in the inventory.
    public Dialogue invDialogue;

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
            UseItem("Soda","Target");
        }
    }

    //Method to check for click
    public bool CheckForClick()
    {
        if (!pauseMenu.Paused)
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
                        if (added)
                        {
                            //transform.position = new Vector2(100.0f, 100.0f);  Commenting this out so that players don't accidentally delete their items.
                            if (interObjRef != null)
                                interObjRef.Temp = this;
                            //inventory.RemoveItem(this);  Commenting this out so that players don't accidentally delete their items.
                            // Adding fun dialogue instead!
                            FindObjectOfType<DialogueManager>().StartDialogue(invDialogue);
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
        return false;
    }

    // Triggers dialogue about the pickup.
    public void OnMouseDown()
    {
        if (!pauseMenu.Paused)
        {
            if (!added)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }
    }

    //To pull an item from your inventory to use or of the ground
    public void UseItem(Vector2 target, string itemTag)
    {
        if (!inventory.HaveItem(this.name))
        {
            //Debug.Log("HIT return");
            //return;
        }

        //To store the mouses position
        Vector2 locationOfMouse = Input.mousePosition;
        //Grab vector2 for cursor to use in AABB math
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

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
                    if (Vector2.Distance(this.transform.position, target) <= 0.2f)
                    {
                        //Validating item
                        if (this.tag != itemTag)
                        {
                            Debug.Log("HIT RETURN: " + this.tag + " " + itemTag);
                            return;
                        }
                        //Do something with the target zone and/or the object in use
                        HitZone(itemTag);
                    }
                }
            }
        }
    }

    //To pull an item from your inventory to use or of the ground using tags
    public void UseItem(string itemTag, string targetTag)
    {
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
        taggedItem = GameObject.FindWithTag(targetTag);

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
                        //Validating item
                        if (this.tag != itemTag)
                        {
                            Debug.Log("HIT RETURN: " + this.tag + " " + itemTag);
                            return;
                        }
                        //Do something with the target zone and/or the object in use
                        HitZone(itemTag);
                    }
                }
            }
        }
    }

    public void HitZone(string item)
    {
        switch (item)
        {
            case "Cheese":
                break;
            case "Baseball":
                break;
            case "Car":
                break;
            case "Apple":
                break;
            case "Stick":
                break;
            case "Camera":
                break;
            case "Glasses":
                break;
            case "Flashlight":
                break;
            case "MoneyKey":
                break;
            case "HouseKey":
                break;
            case "Scissors":
                break;
            case "Mitten":
                break;
            case "Pizza":
                break;
            case "IDcard":
                break;
            case "Pencil":
                break;
            case "Battery":
                break;
            case "Rock":
                break;
            case "Newspaper":
                break;
            case "Key":
                break;
            case "Cookie":
                break;
            case "Soda":
                Debug.Log("HIT ZONE SODA");
                break;
            case "Crowbar":
                break;
            case "Papers":
                break;
            case "Screwdriver":
                    break;
            case "Quarters":
                break;
            case "Ball":
                break;
            default:
                break;

        }
        Debug.Log("HIT ZONE");
    }

    // Cursor changers
    void OnMouseEnter()
    {
        Cursor.SetCursor(specialTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(normalTexture, hotSpot, cursorMode);
    }
}
