using SmartHospital.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Inventory Item UI Controller
/// @Author: KS
/// </summary>
public class InventoryItemUIController : MonoBehaviour
{
    public InventoryItemSO InventoryItemPlaceholder
    {
        get { return inventoryItemSO; }
        set { inventoryItemSO = value; }
    }

    private InventoryItemSO inventoryItemSO;

    public InventoryItem InventoryItem
    {
        get { return inventoryItem; }
        set { inventoryItem = value; }
    }

    private InventoryItem inventoryItem;

    [Header("Prefab Text")]
    private TMP_Text Text;

    [Header("Prefab Image")]
    public Image objSprite;


    [Header("Inventory Icons")]
    public Sprite chair;
    public Sprite table;
    public Sprite plug;
    public Sprite deflt;




    private void Awake()
    {
        Text = transform.GetComponentInChildren<TMP_Text>();
        inventoryItemSO = ScriptableObject.CreateInstance<InventoryItemSO>();
        inventoryItem = new InventoryItem();
        Display();
    }

    public void Display()
    {
        if (inventoryItem.Item_Code != null)
        {
            if (inventoryItem.Name != null)
                Text.SetText(inventoryItem.Name);
            else
                Text.SetText("Leer");

            Sprite imageSprite = Resources.Load("defaultWhite", typeof(Sprite)) as Sprite;

            switch (inventoryItem.Name)
            {
                case "Tisch":
                    imageSprite = table;
                    break;
                case "Stuhl":
                    imageSprite = chair;
                    break;
                case "Steckdose":
                    imageSprite = plug;
                    break;
                case "Sonstiges":
                    imageSprite = deflt;
                    break;
            }

            objSprite.sprite = imageSprite;
        }
        else
        {
            if (inventoryItemSO != null)
            {
                Text.SetText(inventoryItemSO.name);

                Sprite imageSprite = Resources.Load("defaultWhite", typeof(Sprite)) as Sprite;

                switch (inventoryItemSO.name)
                {
                    case "Tisch":
                        imageSprite = table;
                        break;
                    case "Stuhl":
                        imageSprite = chair;
                        break;
                    case "Steckdose":
                        imageSprite = plug;
                        break;
                    case "Sonstiges":
                        imageSprite = deflt;
                        break;
                }
                objSprite.sprite = imageSprite;

            }
        }
    }
}
