using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class DigitalRoomSignView : MonoBehaviour
{
    // View Only
   

    public TextMeshProUGUI Room_Floor { get; set; }             // "Ebene 99"
    public TextMeshProUGUI Room_Name_up { get; set; }           // "Dienstzimmer"
    public TextMeshProUGUI Room_Name_down { get; set; }         // "Sekretariat"
    public TextMeshProUGUI Room_Description { get; set; }       // Workername or Room Description
    public TextMeshProUGUI Room_Clinical_Number { get; set; }   // 105
    public TextMeshProUGUI Room_KTG_Number { get; set; }        // 6420-01-105
    public TextMeshProUGUI Room_Building_Section { get; set; }  // A,B,C,D,E,F
    public Image Room_Building_Section_Image { get; set; }      // ColorLink

    // Edit Mode

}
