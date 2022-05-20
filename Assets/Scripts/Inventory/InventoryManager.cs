using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This script is attached to the Inventory panel
public class InventoryManager : MonoBehaviour
{

    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    private bool isOpen = false;
    public GameObject inventoryCanvas;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public InventoryItem currentItem;
    
    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }
    void MakeInventorySlots()
    {   
        //Go through everything in inventory and set up a slot.
        if (playerInventory != null)
        {
            //Go over everything in the inventory.
            //Checks all the items, if the items exists then it creates a slot.
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                if(playerInventory.myInventory[i].amount > 0 || playerInventory.myInventory[i].unique == true)
                {
                    //Make a temp 
                    GameObject temp =
                        Instantiate(blankInventorySlot,
                        inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);//this is the scrolls view 
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot != null)
                    {
                        newSlot.Setup(playerInventory.myInventory[i], this);
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
    }

    void Update()
    {
        if(Input.GetKeyDown("i"))
        {
            Debug.Log("Pressed I");
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        isOpen = !isOpen;
        if(isOpen)
        {
            inventoryCanvas.SetActive(true);
        }
        else
        {
            inventoryCanvas.SetActive(false);
        }
    }

    public void SetupDescriptionAndButton(string newDescriptionString, 
        bool isButtonUsable, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescriptionString;
        useButton.SetActive(isButtonUsable);
    }
    void ClearInventorySlots()
    {
        for(int i = 0; i < inventoryPanel.transform.childCount; i ++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }
    public void UseButtonPressed()
    {
        if(currentItem)
        {
            currentItem.Use();
            //Clear all of the inventory slots
            ClearInventorySlots();
            //Refill all slots with new numbers
            MakeInventorySlots();
            if (currentItem.amount == 0)
            {
                SetTextAndButton("", false);
            }
        }
    }
}