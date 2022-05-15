using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonController : MonoBehaviour
{
    private Button InventoryItemButton;

    private InventoryCreateView CreateView;

    private InventoryItemSO item;

    private void Start()
    {
        InventoryItemButton = GetComponent<Button>();
        CreateView = FindObjectOfType<InventoryCreateView>();
        item = ScriptableObject.CreateInstance<InventoryItemSO>();
        item = GetComponentInParent<InventoryItemUIController>().InventoryItemPlaceholder;

        InventoryItemButton.onClick.AddListener(() =>
        {

            CreateView.CreateInventarPanel.gameObject.SetActive(true);

            if (CreateView.CreateInventarPanel.gameObject.activeSelf)
            {
                CreateView.ItemCode = item.item_code;
                CreateView.ItemName = item.name;
            }


        });
    }
}
