using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item")]
public class InventoryItemSO : ScriptableObject
{
    public new string name;

    public Sprite image;

    public string item_code;

    public bool inRoom;

    public InventoryItemSO(string name, string item_code, Sprite image, bool inRoom)
    {

        this.name = name;
        this.item_code = item_code;
        this.image = image;
        this.inRoom = inRoom;
    }
}