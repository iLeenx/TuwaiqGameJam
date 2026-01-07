using UnityEngine;

[System.Serializable]
public class ShoppingItem
{
    public int id;
    public Sprite icon;
    public string itemName;
    public int price;
    public bool isStolen = false;
    public bool isCollected = false;
}
