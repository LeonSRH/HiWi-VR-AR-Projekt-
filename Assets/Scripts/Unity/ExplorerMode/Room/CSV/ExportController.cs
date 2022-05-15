using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Model;
using UnityEngine;

public class ExportController : MonoBehaviour {

    [SerializeField]
    string pathInput;

    [SerializeField]
    Transform ExportLabelsGO;

    private string Path { get; set; }
    public string documentPathSchooling = "C:/Users/Katharina/Documents/UKHD/Output_Filter.csv";
    private HashSet<string> ExportLabels = new HashSet<string> ();

    HashSet<string> CSV_Titles_Schooling = new HashSet<string> { "Raumnummer", "Anzahl Mitarbeiter", "Vorname, Nachname" };

    private CSVController csvController;

    private void Start () {
        Path = "C:/Users/" + Environment.UserName + "/Desktop/Raumbuch_Export.csv";

        csvController = GameObject.FindObjectOfType<CSVController> ();

        ExportLabels.Add ("Gebäudenummer");
        ExportLabels.Add ("Ebene und Raumnummer");
        ExportLabels.Add ("Raumnummer vor Ort");
        ExportLabels.Add ("Bruttofläche");
        ExportLabels.Add ("Anmerkungen");

        ExportLabels.Add ("Raumart");
        ExportLabels.Add ("Nutzungsart");
        ExportLabels.Add ("Raumnutzungsart");
        ExportLabels.Add ("Funktionsbereich");
        ExportLabels.Add ("Dienststelle");
        ExportLabels.Add ("Nettofläche");
        ExportLabels.Add ("Raumhöhe");
        ExportLabels.Add ("Wandfliesen");
        ExportLabels.Add ("Glasfläche innen");
        ExportLabels.Add ("Bodenbelag");
        ExportLabels.Add ("Keine Fenster");
        ExportLabels.Add ("Einfachglas");
        ExportLabels.Add ("Doppelglas");
        ExportLabels.Add ("Doppelfenster");
        ExportLabels.Add ("Anzahl Abluftanlagen");
        ExportLabels.Add ("Anzahl Fliegengitter");
        ExportLabels.Add ("Fliegengitter abnehmbar");
        ExportLabels.Add ("Waschbecken Anzahl");
        ExportLabels.Add ("Wasseranschluss vorhanden");
        ExportLabels.Add ("Mechanische Be. und Entlueftung vorhanden");
        ExportLabels.Add ("Umluftkühlung vorhanden");
        ExportLabels.Add ("Datenanschluss vorhanden");
        ExportLabels.Add ("Druckluft Medizinische Gase vorhanden");
        ExportLabels.Add ("Möbelierung");
        ExportLabels.Add ("Tageslicht");
        ExportLabels.Add ("Wohnungstyp");
        ExportLabels.Add ("Laborklassifizierung");
        ExportLabels.Add ("Radioaktivität");
        ExportLabels.Add ("Übergeordnete Raumnutzung");
        ExportLabels.Add ("Raumverwaltung");
        ExportLabels.Add ("Übergeordneter Nutzer");
        ExportLabels.Add ("Zusätzlicher Nutzer");
        ExportLabels.Add ("Nutzungsende");

        #region Forschungsverbünde
        ExportLabels.Add ("DZL");
        ExportLabels.Add ("DZHK");
        ExportLabels.Add ("DKTK");
        ExportLabels.Add ("DZIF");
        ExportLabels.Add ("Cellular Networks");
        ExportLabels.Add ("HBIGS");
        ExportLabels.Add ("SFB 488");
        ExportLabels.Add ("SFB 544");
        ExportLabels.Add ("SFB 638");
        ExportLabels.Add ("SFB 873");
        ExportLabels.Add ("SFB 938");
        ExportLabels.Add ("SFB 1036");
        ExportLabels.Add ("SFB-TR77");
        ExportLabels.Add ("SFB-TRR 125");
        ExportLabels.Add ("KFO 214");
        ExportLabels.Add ("KFO 227");
        ExportLabels.Add ("KFO 256");

        #endregion
        ExportLabels.Add ("Zuständigkeit der Raumkommission");
        ExportLabels.Add ("Sitzplatzkapazität");
        WriteSchooling ();
    }

    public void ShowDialog () {
        FolderBrowserDialog fbd = new FolderBrowserDialog ();
        fbd.Description = "Wählen Sie einen Ablageort";

        if (fbd.ShowDialog () == DialogResult.OK) {
            string sSelectedPath = fbd.SelectedPath;

            //TODO : Path Input
            //            pathInput.text = sSelectedPath;

            Path = "C:/Users/Katharina/Documents/UKHD/Output.csv";
        }
    }
    public void WriteSchooling () {
        string Path = documentPathSchooling;

        var gameObjectRooms = GameObject.FindGameObjectsWithTag ("RoomCollider");

        var rooms = new HashSet<ServerRoom> ();

        foreach (GameObject g in gameObjectRooms) {

            if (g.GetComponent<ClientRoom> ().MyRoom != null) {
                ServerRoom nwR = g.GetComponent<ClientRoom> ().MyRoom;

                rooms.Add (nwR);
            }
        }

        // Write sample data to CSV file
        using (FileStream sw = new FileStream (Path, FileMode.OpenOrCreate)) {
            using (CsvFileWriter writer = new CsvFileWriter (sw)) {
                List<string> columns = new List<string> ();

                foreach (string exportLabel in CSV_Titles_Schooling) {
                    columns.Add (exportLabel);
                }

                writer.WriteLine (string.Join (";", columns), Encoding.UTF8);

                foreach (ServerRoom room in rooms) {
                    List<string> columnsRoom = new List<string> ();
                    if (room.NumberOfWorkspaces > 0 && room.WorkersWithAccess != null) {
                        columnsRoom.Add (room.RoomName);
                        columnsRoom.Add ("" + room.NumberOfWorkspaces);
                        foreach (var worker in room.WorkersWithAccess) {
                            columnsRoom.Add (worker.FirstName + ", " + worker.LastName);
                        }

                        writer.WriteLine (string.Join (";", columnsRoom), Encoding.UTF8);
                    }
                }

            }
        }

    }
    public void WriteTest () {
        string Path = @"C:/Users/Katharina/Documents/UKHD/Raumbuch_Export_UTF8.csv";
        var gameObjectRooms = GameObject.FindGameObjectsWithTag ("RoomCollider");

        var rooms = new HashSet<ServerRoom> ();

        foreach (GameObject g in gameObjectRooms) {

            if (g.GetComponent<ClientRoom> ().MyRoom != null) {
                ServerRoom nwR = g.GetComponent<ClientRoom> ().MyRoom;

                rooms.Add (nwR);
            }
        }

        // Write sample data to CSV file
        using (FileStream sw = new FileStream (Path, FileMode.OpenOrCreate)) {
            using (CsvFileWriter writer = new CsvFileWriter (sw)) {
                List<string> columns = new List<string> ();

                foreach (string exportLabel in ExportLabels) {
                    columns.Add (exportLabel);
                }

                writer.WriteLine (string.Join (";", columns), Encoding.UTF8);

                foreach (ServerRoom room in rooms) {
                    List<string> columnsRoom = new List<string> ();
                    foreach (string s in ExportLabels) {
                        switch (s) {
                            case "Gebäudenummer":
                                columnsRoom.Add (String.Format ("6420"));
                                break;
                            case "Ebene und Raumnummer":
                                string substringRoomName = room.RoomName.Substring (4);
                                substringRoomName = substringRoomName.Replace (".", "");
                                columnsRoom.Add (String.Format (substringRoomName, Encoding.UTF8));
                                break;
                            case "Raumnummer vor Ort":
                                if (!room.NamePlate.VisibleRoomName.Equals ("0"))
                                    columnsRoom.Add (String.Format (room.NamePlate.VisibleRoomName, Encoding.UTF8));
                                else
                                    columnsRoom.Add (String.Format (room.RoomName.Substring (8), Encoding.UTF8));
                                break;
                            case "Anmerkungen":
                                var array = room.NamePlate.Designation.ToArray ();
                                columnsRoom.Add (String.Format (array[0]));
                                break;
                            case "Nutzungsart":
                                switch (room.Department.CostCentre) {
                                    case 9230: //Allg
                                        columnsRoom.Add (String.Format ("9230017", Encoding.UTF8));

                                        break;
                                    case 9231: //VCh
                                        columnsRoom.Add (String.Format ("9231013", Encoding.UTF8));

                                        break;
                                    case 9232: //HCh
                                        columnsRoom.Add (String.Format ("9232010", Encoding.UTF8));

                                        break;
                                    case 9234: //GefCh
                                        columnsRoom.Add (String.Format ("9234012", Encoding.UTF8));

                                        break;
                                    case 9235: //Uro
                                        columnsRoom.Add (String.Format ("9235019", Encoding.UTF8));

                                        break;
                                    case 9321: //AN
                                        columnsRoom.Add (String.Format ("9321012", Encoding.UTF8));

                                        break;
                                    case 9355: //DIR
                                        columnsRoom.Add (String.Format ("9355014", Encoding.UTF8));

                                        break;
                                    default:
                                        if (room.RoomName.Equals ("6420.00.305")) {
                                            columnsRoom.Add (String.Format ("9012001", Encoding.UTF8));
                                        } else if (room.RoomName.Equals ("6420.01.347") || room.RoomName.Equals ("6420.01.349")) {
                                            columnsRoom.Add (String.Format ("9010017", Encoding.UTF8));
                                        }
                                        columnsRoom.Add (String.Format ("", Encoding.UTF8));
                                        break;
                                }
                                break;
                            case "Übergeordnete Raumnutzung":
                                {
                                    switch (room.NamePlate.Floor) {
                                        case 02:
                                            if (room.NamePlate.BuildingSection.Equals ("A")) {

                                                columnsRoom.Add (String.Format ("Forschung", Encoding.UTF8));
                                            } else {
                                                columnsRoom.Add (String.Format ("Krankenversorgung", Encoding.UTF8));
                                            }
                                            break;
                                        case 00:
                                            if (room.NamePlate.BuildingSection.Equals ("A")) {

                                                columnsRoom.Add (String.Format ("Lehre", Encoding.UTF8));
                                            } else {
                                                columnsRoom.Add (String.Format ("Krankenversorgung", Encoding.UTF8));
                                            }
                                            break;
                                        default:
                                            if (room.RoomName.Equals ("6420.01.305") || room.RoomName.Equals ("6420.01.349")) {

                                                columnsRoom.Add (String.Format ("Klinikverwaltung", Encoding.UTF8));
                                            } else {

                                                columnsRoom.Add (String.Format ("Krankenversorgung", Encoding.UTF8));
                                            }
                                            break;
                                    }

                                }
                                writer.WriteLine (string.Join (";", columnsRoom), Encoding.UTF8);
                                break;
                        }

                    }
                }

            }
        }
    }
    public string GetEnumDescription (Enum enumValue) {
        var fieldInfo = enumValue.GetType ().GetField (enumValue.ToString ());

        var descriptionAttributes = (DescriptionAttribute[]) fieldInfo.GetCustomAttributes (typeof (DescriptionAttribute), false);

        return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString ();
    }

}