using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Inventory[] inventories;
    public List<PickUp> inventory;

    private float invX;
    private float invY;

    private bool once;
    private bool once2;

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

        once = false;
        once2 = true;

        //Allows the inventory to be accessed in other scenes
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (prevCount != inventory.Count)
        {
            if (inventory.Count > 0)
            {
                if(inventory.Count > 8)
                {
                    rowCount++;
                }
                for(int i  = 0; i < inventory.Count; i++)
                {
                    DisplayItem(inventory[i], invX, invY, i);
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
        rowCount--;
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

        //If the index is higher than 8, go to new row and reset x 
        if(index > 7)
        {
            y = -1.146f;
            x = -1.323f;
            int count = inventory.Count - 1;
            x -= 0.366f * (float)(index - count);            
        }

        //Display the item
        item.transform.position = new Vector2(x, y);
    }
}
