using UnityEngine;

[CreateAssetMenu(fileName = "New placeable Inventory Item", menuName = "Placeable Inventory Item")]
public class PlaceableInventoryItemSO : MonoBehaviour
{
    public new string name;

    public Sprite image;

    public string item_code;

    public string roomName;

    public InventoryCategory inventoryCategory;

    public float height, width, length;

    public float positionX, positionY, positionZ;

    public PlaceableInventoryItemSO(string name, string item_code, Sprite image, string roomName, InventoryCategory category,
        float height, float width, float length, float positionX, float positionY, float positionZ)
    {

        this.name = name;
        this.item_code = item_code;
        this.image = image;
        this.roomName = roomName;
        this.inventoryCategory = category;
        this.height = height;
        this.width = width;
        this.length = length;
        this.positionX = positionX;
        this.positionY = positionY;
        this.positionZ = positionZ;


    }
}

public enum InventoryCategory
{
    TABLE, CHAIR, PC, MOUSE, INPUT, CLOSET, BED, PATIENTTRANSPORTBED, OTHER
}

