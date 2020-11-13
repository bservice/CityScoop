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
        invY = -0.973f;

        once = false;

        //Allows the inventory to be accessed in other scenes
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.Count > 0)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                DisplayItem(inventory[i], invX, i);
            }
        }
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

    public void DisplayItem(PickUp item, float x, int index)
    {
        x += 0.366f * (float)index;
        item.transform.position = new Vector2(x, invY);
    }
}
