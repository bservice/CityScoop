using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Inventory[] inventories;
    public List<PickUp> inventory;

    private float invX;
    private float invY;

    private int prevCount;
    private int rowCount;

    // Start is called before the first frame update
    void Start()
    {
        //Following code prevents more than one inventory from being created
        inventories = FindObjectsOfType<Inventory>();
        if (inventories.Length > 1)
        {
            Destroy(inventories[1]);
            Destroy(inventories[1].gameObject);
        }

        invX = -1.323f;
        invY = -0.788f;

        rowCount = 0;

        //Allows the inventory to be accessed in other scenes
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.Count > 0)
        {
            if (prevCount != inventory.Count)
            {
                if(inventory.Count > 8)
                {
                    //If the inventory goes to the next row, run two loops for each row
                    for (int i = 0; i < 8; i++)
                    {
                        DisplayItem(inventory[i], invX, invY, i);
                    }
                    for (int i = 8; i < inventory.Count; i++)
                    {
                        //Set new x and y for the lower row
                        float x = -1.689f;
                        float y = -1.146f;
                        //Rowcount serves as "i"
                        //Only used because i does not start at 0
                        rowCount++;
                        DisplayItem(inventory[i], x, y, rowCount);
                    }
                    //Reset row count
                    rowCount = 0;
                }
                else
                {
                    //if there is only one row, display as normal
                    invX = -1.323f;
                    invY = -0.788f;
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        DisplayItem(inventory[i], invX, invY, i);
                    }
                }                
            }
        }

        prevCount = inventory.Count;
    }

    //Add item to list
    public void AddItem(PickUp item)
    {
        inventory.Add(item);
    }

    public void  RemoveItem(PickUp item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].Name == item.Name)
            {
                inventory.RemoveAt(i);
            }
        }       
    }

    //Remove item from list

    public void DisplayItem(PickUp item, float x, float y, int index)
    {
        //Display at the x value according to the index of where it is in the list
        x += 0.366f * (float)index;

        //Display the item
        item.transform.position = new Vector2(x, y);
    }
}
