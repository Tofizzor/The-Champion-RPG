using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject {

    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;

    // Check if certain object is in inventory
    public bool CheckForItem(Item itm)
    {
        if (items.Contains(itm)){
            return true;
        }
        return false;
    }

    public void AddItem(Item itemToAdd)
    {
        // Is the item a key?.

        if (itemToAdd.isKey)
        {
            numberOfKeys++;

        }
        else
        {
            // If item is not already in the inventory then add it
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

}
