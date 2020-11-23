using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingObjects : MonoBehaviour
{
    public Inventory inventoryRef;
    private PickUp temp;

    public PickUp Temp
    {
        get { return temp; }
        set { temp = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //To pull an item from your inventory to use
    void UseItem(Vector2 target)
    {
        //To store the mouses position
        Vector2 locationOfMouse = Input.mousePosition;

        //To see if they are clicking if so have that object follow the cursor
        for(int i = 0; i < inventoryRef.Count; i++)
        {
            //To see if they are clicking on an item in the inventory or on the ground
            if(inventoryRef.inventory[i].CheckForClick())
            {
                temp.transform.position = locationOfMouse;

                //to see if the item is near the target zone
                if (Vector2.Distance(temp.transform.position, target) <= 2.0f)
                {
                    //Do something with the target zone and/or the object in use
                }
            }
        }
    }
}
