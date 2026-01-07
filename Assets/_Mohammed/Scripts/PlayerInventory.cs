using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ShoppingItem> itemlist = new List<ShoppingItem>();
    [SerializeField] private AudioSource _inventoryAudioSource;
    [SerializeField] private AudioClip[] _inventoryAudioClips;
    private bool isOpen = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //Open/Close Inventory UI and play the respective sound
        {
            //UIManger.instance.ToggleCollectibles();

            if (isOpen)
            {
                _inventoryAudioSource.PlayOneShot(_inventoryAudioClips[0]);
                GameUIManager.instance.ShowBagContainer();
                GameUIManager.instance.HideListContainer();
            }
            else
            {
                _inventoryAudioSource.PlayOneShot(_inventoryAudioClips[1]);
                GameUIManager.instance.HideBagContainer();
                GameUIManager.instance.ShowListContainer();
            }

            isOpen = isOpen ? false : true;
        }

        
    }



    public void Additem(ShoppingItem item) //Add item to the inventory and update UI
    {
        itemlist.Add(item);
        if (!item.isStolen)
        {
            _inventoryAudioSource.PlayOneShot(_inventoryAudioClips[2]);
        }
        else
        {
            _inventoryAudioSource.PlayOneShot(_inventoryAudioClips[3]);
        }
        
        //UIManger.instance.AddCollectible(item.icon);
    }

    public List<ShoppingItem> getInventoryList() // return the inventory.
    {
        return itemlist;
    }
}
