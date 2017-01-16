using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {
    GameObject inventoryPanel;
    GameObject slotPanel;
    public ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    int slotAmount,currentSlot;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    public void Start()
    {
        database = GetComponent<ItemDatabase>();
        slotAmount = 20;
        inventoryPanel = GameObject.Find("Inventory");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        for(int i = 0;i<slotAmount;i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
        }
        AddItem(0);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
            AddItem(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
            AddItem(1);
        }
    }
    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        //for (int q = 0; q < slots.Count; q++)
        //{
        //    ItemData data = slots[q].transform.GetChild(0).GetComponent<ItemData>();
        //    if (data.amount != itemToAdd.MaxStackable)
        //    {

        //    }
        //}
        itemToAdd.Stackable = true;
        Debug.Log(itemToAdd.Stackable);
        if (itemToAdd.Stackable && CheckIfItemExist(itemToAdd) && CheckIfMaxStacks(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
        
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    if (data.amount >= items[i].MaxStackable)
                    {
                        items[i].Stackable = false;
                        Debug.Log(i);
                    }
                    break;
                }

            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;
                    itemObj.transform.position = itemObj.transform.parent.position;
                    break;
                }
            }
        }
  
    }
    bool CheckIfItemExist(Item item)
    {
        for(int i =0;i<items.Count;i++)
        {
            if (items[i].ID == item.ID)
                return true;
        }
        return false;
    }
    bool CheckIfMaxStacks(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID && items[i].Stackable)
            {
                ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                if (data.amount < items[i].MaxStackable)
                {
                    return true;
                }
             
            }
        }
        Debug.Log("False");
        return false;
    }
}
