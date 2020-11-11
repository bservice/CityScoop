using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Inventory[] inventories;
    private List<string> inventory;

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

        //Allows the inventory to be accessed in other scenes
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(string item)
    {
        inventory.Add(item);
    }
}
