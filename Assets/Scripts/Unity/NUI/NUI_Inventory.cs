using UnityEngine;

//NUI INVENTORY

///=================================================================================================================================
///=================================================================================================================================
/// 
/// Author: Sebastian de Andrade ( AKA Meister des Codes und Herr der Chiffren)
/// Date: 24.5.2019 ( Im Jahre des Herrn.. Amen)
/// 
/// The Class NUI_Inventory creates UI Elements for the Inventory system. It creates the elements via "Fillpanel", a UI Element factory.
/// 
/// 
/// HISTORY =================================
/// 25.5.19:    Creating Images via Button (SDA)
/// 27.5.19:    Setting Header on Top of "CreateEntry" (SDA)
///             Refactoring with #Regions (SDA)
///=================================================================================================================================
///=================================================================================================================================

public class NUI_Inventory : MonoBehaviour
{

    FillPanel fillPanel;

    public InventoryCreateView inventoryCreateView;
    public InventoryView inventoryView;

    public Sprite testSprite;

    // Start is called before the first frame update
    void Awake()
    {
        fillPanel = GameObject.FindObjectOfType<FillPanel>();
        inventoryCreateView = FindObjectOfType<InventoryCreateView>();
        inventoryView = FindObjectOfType<InventoryView>();
        CreateMasterlist();
        CreateEntry();
        //CreateButtonlist();
    }

    public void CreateEntry()
    {
        //====================================
        /// Summary: Function creates UI for an additional entry or a change in the Roome Inventory
        /// All functionality is split from this creation process => MVC. ( Modell View Controller)
        /// This is a View.



        //====================================
        // Root Object is parent to all following Objects
        #region Create Root Obj for all UI Elements
        Transform Parent = fillPanel.CreateParentObject(transform);
        Parent.transform.localPosition = new Vector3(0, 0, 0);
        inventoryCreateView.CreateInventarPanel = Parent;
        Parent.name = "EntryForInventory";
        #endregion
        //====================================

        //====================================
        // Create Vertical Panel ( Up = Header, Bottom = Menu)
        #region Split Header from Functions
        // Vertical Panel: Upper Part ist Header, lower part will be splited in 2 vertical panels
        Transform VerticalPanel = fillPanel.CreatePanel(600, 600, Parent, UI_Panel.enumForLayoutGroup.Vertical, true); // this element is important to get a horizontal Header with "Inventory Element"
        VerticalPanel.transform.localPosition = new Vector3(0, 0, 0); // Set it in the center of the Screen
        #endregion
        //====================================

        //====================================
        // Header on Top
        #region Add big Header
        // Set Header in upper Part
        fillPanel.CreateHeaderOnUI("Inventargegenstand Info", VerticalPanel);
        #endregion
        //====================================

        //====================================
        // Split the bottom into a right hand side and a left hand side
        #region Two Vertical Panels Beneath Header

        // now we have a Header and add a Horizontal Panel beneath it, that we have a right hand side and a left hand side.
        Transform VerticalParentPanel = fillPanel.CreatePanel(900, 900, VerticalPanel, UI_Panel.enumForLayoutGroup.Horizontal, true);  // Hier braucht es keine anordnung der KindObjekte

        //======= ADD 2 Vertical Panels for right hand side and left hand side
        Transform LocalElementLeft = fillPanel.CreatePanel(200, 600, VerticalParentPanel, UI_Panel.enumForLayoutGroup.Vertical, true);  // Left Hand Side
        //Transform LocalElementRight = fillPanel.CreatePanel(200, 600, VerticalParentPanel, UI_Panel.enumForLayoutGroup.Vertical, true); // Right Hand Side
        #endregion
        //====================================

        //====================================
        // Add Elements to left hand side
        #region Left Hand Side

        // ====== Childs for Left Hand Side
        inventoryCreateView.ItemNameInput = fillPanel.CreateInputFieldOnUIWithHandle("Name", LocalElementLeft, 200, 100, 0, 0);
        inventoryCreateView.ItemCostInput = fillPanel.CreateInputFieldOnUIWithHandle("Preis", LocalElementLeft, 200, 100, 0, 0);
        inventoryCreateView.ItemDesignationInput = fillPanel.CreateInputFieldOnUIWithHandle("Beschreibung", LocalElementLeft, 200, 100, 0, 0);
        inventoryCreateView.ItemSizeInput = fillPanel.CreateInputFieldOnUIWithHandle("Größe BxHxT", LocalElementLeft, 200, 100, 0, 0);
        inventoryCreateView.ItemCodeInput = fillPanel.CreateInputFieldOnUIWithHandle("Inventarnummer", LocalElementLeft, 200, 100, 0, 0);
        inventoryCreateView.ItemMassInput = fillPanel.CreateInputFieldOnUIWithHandle("Masse", LocalElementLeft, 200, 100, 0, 0);
        inventoryCreateView.ItemResidualValue = fillPanel.CreateInputFieldOnUIWithHandle("Abschreibung", LocalElementLeft, 200, 100, 0, 0);


        //======================================================================================================================================
        // PRODUCER
        Transform producer = fillPanel.CreatePanel(200, 100, LocalElementLeft, UI_Panel.enumForLayoutGroup.Horizontal, false);
        fillPanel.CreateTextElementOnUI("Hersteller", producer);
        inventoryCreateView.ItemProducerDropdown = fillPanel.CreateDropDownOnUI("Hersteller", producer, new string[] {
                "NONE",
                "NONE",
                "NONE",
                "NONE",
                "N2"
            });
        //======================================================================================================================================


        //======================================================================================================================================
        // LINKED ITEM
        Transform linkedItem = fillPanel.CreatePanel(200, 100, LocalElementLeft, UI_Panel.enumForLayoutGroup.Horizontal, false);
        fillPanel.CreateTextElementOnUI("Verknüpftes Objekt", linkedItem);
        inventoryCreateView.LinkedItemDropdown = fillPanel.CreateDropDownOnUI("Verknüpftes Objekt", linkedItem, new string[] {
                "Objekt 1",
                "Objekt 2",
                "Objekt 3",
                "Objekt 4",
                "Objekt 5"
            });
        //======================================================================================================================================

        //======================================================================================================================================
        // PRODUCT GROUP
        Transform productGroup = fillPanel.CreatePanel(200, 100, LocalElementLeft, UI_Panel.enumForLayoutGroup.Horizontal, false);
        fillPanel.CreateTextElementOnUI("Produktgruppe", productGroup);
        inventoryCreateView.ProductGroupDropdown = fillPanel.CreateDropDownOnUI("Product Group", productGroup, new string[] {
                "Medizingerät statisch",
                "Statisch",
                "Medizingerät mobil",
                "Mobil",
                "Mikrologistik",
                "Sonstiges"
            });
        //======================================================================================================================================

        #endregion
        //====================================


        //====================================
        // Add Elements to right hand side
        #region Right Hand Side (Buttons)

        // ==== CHILDS FOR RIGHT HAND SIDE

        //======================================================================================================================================
        // Back and Save button
        inventoryCreateView.CancelButton = fillPanel.CreateButtonOnUIWithHandle("Zurück", LocalElementLeft, 0, 0, 0, 0);
        inventoryCreateView.SaveButton = fillPanel.CreateButtonOnUIWithHandle("Speichern", LocalElementLeft, 0, 0, 0, 0);
        //======================================================================================================================================

        #endregion
        //====================================

        //====================================
        // IGNORE*
        #region Testing Stuff
        //======================================================================================================================================
        //  Just for testing purpose #notImpportant
        //  Transform Parent2 = fillPanel.CreateParentObject(transform); // Hier braucht es eine anordnung der KindObjekte
        //  var ChangeWorkerViewBackButton3 = fillPanel.CreateButtonOnUIWithHandle("Save", Parent2,300,300,0,0,testSprite); Test Button with Image in center of screen
        //======================================================================================================================================
        #endregion
        //====================================
    }

    public void CreateMasterlist()
    {

        /// Summary: Creates the master list for all objects;


        //===============================================
        // CREATE ROOT OBJECT
        #region Root Object InventoryMasterList
        Transform ParentTransform = fillPanel.CreatePanel(300, 900, UI_OrientationOnScreen.enumForOrientations.Right, UI_Panel.enumForLayoutGroup.Vertical, true);  // Hier braucht es keine anordnung der KindObjekte
        ParentTransform.name = "Inventory Master List";
        inventoryView.InventarPanel = ParentTransform;
        #endregion
        //===============================================

        //===============================================
        // HEADER
        fillPanel.CreateHeaderOnUI("Rauminventar", ParentTransform);
        //===============================================

        //===============================================
        // STATIC (NOT CHANGEABLE) DISPLAY IF ELEMENT VALUES
        #region Display of Elements


        // This Dropdown shows all elements in a Room
        inventoryView.ItemDropdown = fillPanel.CreateDropDownOnUI("Inventarart", ParentTransform, new string[] {
            "Medizingerät statisch",
                "Statisch",
                "Medizingerät mobil",
                "Mobil",
                "Mikrologistik",
                "Sonstiges"
        });

        // STATIC Display Elements
        inventoryView.ItemNameText = fillPanel.CreateTextElementOnUIWithHandle("Name:", ParentTransform);

        inventoryView.ItemCostText = fillPanel.CreateTextElementOnUIWithHandle("Preis", ParentTransform);

        inventoryView.ItemDesignationText = fillPanel.CreateTextElementOnUIWithHandle("Beschreibung", ParentTransform);

        inventoryView.ItemSizeText = fillPanel.CreateTextElementOnUIWithHandle("Größe", ParentTransform);

        inventoryView.ItemCodeText = fillPanel.CreateTextElementOnUIWithHandle("Inventarnummer", ParentTransform);

        inventoryView.ItemMassText = fillPanel.CreateTextElementOnUIWithHandle("Masse", ParentTransform);

        inventoryView.ItemProducerNameText = fillPanel.CreateTextElementOnUIWithHandle("Hersteller", ParentTransform);

        inventoryView.ItemLinkedItemText = fillPanel.CreateTextElementOnUIWithHandle("Verknüpftes Objekt", ParentTransform);

        inventoryView.ItemProductGroupText = fillPanel.CreateTextElementOnUIWithHandle("Produkt Gruppe", ParentTransform);

        // THIS button leads to Create an Entry => CreateEntry()
        inventoryView.EditButton =
        fillPanel.CreateButtonOnUIWithHandle("Ändern", ParentTransform, 0, 0, 0, 0);
        inventoryView.CancelButton = fillPanel.CreateButtonOnUIWithHandle("Zurück", ParentTransform, 0, 0, 0, 0);
        #endregion
        //===============================================

        ParentTransform.gameObject.SetActive(false);

    }

    public void CreateButtonlist()
    {

        /// Summary: Creates the master list for all objects;


        //===============================================
        // CREATE ROOT OBJECT
        #region Root Object InventoryMasterList
        Transform ParentTransform = fillPanel.CreatePanel(300, 900, UI_OrientationOnScreen.enumForOrientations.Left, UI_Panel.enumForLayoutGroup.Vertical, true);  // Hier braucht es keine anordnung der KindObjekte
        ParentTransform.name = "Inventory Master List";
        #endregion
        //===============================================

        //===============================================
        // HEADER
        fillPanel.CreateHeaderOnUI("Rauminventar", ParentTransform);
        //===============================================

        //===============================================
        // STATIC (NOT CHANGEABLE) DISPLAY IF ELEMENT VALUES
        #region Display of Elements


        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);
        fillPanel.CreateButtonOnUIWithHandle("bla", ParentTransform, 0, 0, 0, 0);



        // THIS button leads to Create an Entry => CreateEntry()
        var ChangeWorkerViewBackButton = fillPanel.CreateButtonOnUIWithHandle("Ändern", ParentTransform, 0, 0, 0, 0);
        #endregion
        //===============================================

    }

}
