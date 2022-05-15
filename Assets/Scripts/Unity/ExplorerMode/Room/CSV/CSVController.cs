using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Model;
using UnityEngine;
using UnityEngine.WSA;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CSVRoom {
    public string Gebäudenummer;
    public string EbeneundRaumnummer;
    public string RaumnummervorOrt;
    public string Bruttofläche;
    public string Anmerkungen;
    public string Raumart;
    public string Nutzungsart;
    public string Raumnutzungsart;
    public string Funktionsbereich;
    public string Dienststelle;
    public string Nettofläche;
    public string Raumhöhe;
    public string Wandfliesen;
    public string Glasflächeinnen;
    public string Glasflächeaussen;
    public string Bodenbelag;
    public string KeineFenster;
    public string Einfachglas;
    public string Doppelglas;
    public string Doppelfenster;
    public string AnzahlAbluftanlagen;
    public string AnzahlFliegengitter;
    public string Fliegengitterabnehmbar;
    public string WaschbeckenAnzahl;
    public string Wasseranschlussvorhanden;
    public string MechanischeBeundEntlüftungvorhanden;
    public string Umluftkühlungvorhanden;
    public string Datenanschlussvorhanden;
    public string DruckluftMedizinischeGasevorhanden;
    public string Möblierung;
    public string Tageslicht;
    public string Wohnungstyp;
    public string Laborklassifizierung;
    public string Radioaktivität;
    public string ÜbergeordneteRaumnutzung;
    public string Raumverwaltung;
    public string ÜbergeordneterNutzer;
    public string ZusätzlicherNutzer;
    public string Nutzungsende;
    public string DZL;
    public string DZHK;
    public string DKTK;
    public string DZIF;
    public string CellularNetworks;
    public string HBIGS;
    public string SFB488;
    public string SFB544;
    public string SFB638;
    public string SFB873;
    public string SFB938;
    public string SFB1036;
    public string SFBTR77;
    public string SFBTRR125;
    public string KFO214;
    public string KFO227;
    public string KFO256;
    public string ZuständigkeitderRaumkommission;
    public string Sitzplatzkapazität;
}

public class CSVController : MonoBehaviour {
    [SerializeField]
    CsvFileWriter writer;
    CsvFileReader reader;

    public HashSet<ServerRoom> rooms;
    List<string> CSV_Titles = new List<string> {
        "Gebäudenummer",
        "Ebene und Raumnummer",
        "Raumnummer vor Ort",
        "Bruttofläche",
        "Anmerkungen",
        "Raumart",
        "Nutzungsart",
        "Raumnutzungsart",
        "Funktionsbereich",
        "Dienststelle",
        "Nettofläche",
        "Raumhöhe",
        "Wandfliesen",
        "Glasfläche innen",
        "Glasfläche aussen",
        "Bodenbelag",
        "Keine Fenster",
        "Einfachglas",
        "Doppelglas",
        "Doppelfenster",
        "Anzahl Abluftanlagen",
        "Anzahl Fliegengitter",
        "Fliegengitter abnehmbar",
        "Waschbecken Anzahl",
        "Wasseranschluss vorhanden",
        "Mechanische Be. und Entlüftung vorhanden",
        "Umluftkühlung vorhanden",
        "Datenanschluss vorhanden",
        "Druckluft Medizinische Gase vorhanden",
        "Möblierung",
        "Tageslicht",
        "Wohnungstyp",
        "Laborklassifizierung",
        "Radioaktivität",
        "Übergeordnete Raumnutzung",
        "Raumverwaltung",
        "Übergeordneter Nutzer",
        "Zusätzlicher Nutzer",
        "Nutzungsende",
        "DZL",
        "DZHK",
        "DKTK",
        "DZIF",
        "Cellular Networks",
        "HBIGS",
        "SFB 488",
        "SFB 544",
        "SFB 638",
        "SFB 873",
        "SFB 938",
        "SFB 1036",
        "SFB-TR77",
        "SFB-TRR 125",
        "KFO 214",
        "KFO 227",
        "KFO 256",
        "Zuständigkeit der Raumkommission",
        "Sitzplatzkapazität"
    };
    List<string> CSV_Titles_Schooling = new List<string> { "Raumnummer", "Nachname", "Vorname" };
    public string documentPath = "C:/Users/Katharina/Documents/UKHD/Output.csv";
    public string documentPathSchooling = "C:/Users/Katharina/Documents/UKHD/Output_Filter.csv";

    private void Start () {
        //SaveRoomsInfo ();
    }

    public void SaveRoomsInfo () {

        var gameObjectRooms = GameObject.FindGameObjectsWithTag ("RoomCollider");

        rooms = new HashSet<ServerRoom> ();

        foreach (GameObject g in gameObjectRooms) {

            if (g.GetComponent<ClientRoom> ().MyRoom != null) {
                ServerRoom nwR = g.GetComponent<ClientRoom> ().MyRoom;

                if (nwR.NamePlate != null)
                    rooms.Add (nwR);
            }
        }

        if (File.Exists (documentPath)) {

            using (FileStream fs = File.Open (documentPath, FileMode.Open, FileAccess.Write, FileShare.None)) {
                try {

                    writer = new CsvFileWriter (fs);
                    writer.WriteLine ("CSV_Titles");
                    Debug.Log ("Wrote lines" + CSV_Titles.Count);
                } catch {
                    Debug.LogError ("Couldnt write file");
                }

            }

        } else {
            try {
                using (FileStream fs = File.Create (documentPath)) {
                    writer = new CsvFileWriter (fs);
                    writer.WriteLine (CSV_Titles);
                }

            } catch {
                print ("Couldn't write line");
            }
        }

    }

}
/// <summary>
/// Class to store one CSV row
/// </summary>
public class CsvRow : List<string> {
    public string LineText { get; set; }
}

/// <summary>
/// Class to write data to a CSV file
/// </summary>
public class CsvFileWriter : StreamWriter {
    public CsvFileWriter (Stream stream) : base (stream) { }

    public CsvFileWriter (string filename) : base (filename) { }

    /// <summary>
    /// Writes a single row to a CSV file.
    /// </summary>
    /// <param name="row">The row to be written</param>
    public void WriteRow (CsvRow row) {
        StringBuilder builder = new StringBuilder ();
        bool firstColumn = true;
        foreach (string value in row) {
            // Add separator if this isn't the first value
            if (!firstColumn)
                builder.Append (',');
            // Implement special handling for values that contain comma or quote
            // Enclose in quotes and double up any double quotes
            if (value.IndexOfAny (new char[] { '"', ',' }) != -1)
                builder.AppendFormat ("\"{0}\"", value.Replace ("\"", "\"\""));
            else
                builder.Append (value);
            firstColumn = false;
        }
        row.LineText = builder.ToString ();
        WriteLine (row.LineText);
    }
}

/// <summary>
/// Class to read data from a CSV file
/// </summary>
public class CsvFileReader : StreamReader {
    public CsvFileReader (Stream stream) : base (stream) { }

    public CsvFileReader (string filename) : base (filename) { }

    /// <summary>
    /// Reads a row of data from a CSV file
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public bool ReadRow (CsvRow row) {
        row.LineText = ReadLine ();
        if (String.IsNullOrEmpty (row.LineText))
            return false;

        int pos = 0;
        int rows = 0;

        while (pos < row.LineText.Length) {
            string value;

            // Special handling for quoted field
            if (row.LineText[pos] == '"' || row.LineText[pos] == ',') {
                // Skip initial quote
                pos++;

                // Parse quoted value
                int start = pos;
                while (pos < row.LineText.Length) {
                    // Test for quote character
                    if (row.LineText[pos] == '"' || row.LineText[pos] == ',') {
                        // Found one
                        pos++;

                        // If two quotes together, keep one
                        // Otherwise, indicates end of value
                        if (pos >= row.LineText.Length || row.LineText[pos] != '"' || row.LineText[pos] == ',') {
                            pos--;
                            break;
                        }
                    }
                    pos++;
                }
                value = row.LineText.Substring (start, pos - start);
                value = value.Replace ("\"\"", "\"");
            } else {
                // Parse unquoted value
                int start = pos;
                while (pos < row.LineText.Length && row.LineText[pos] != '\n')
                    pos++;
                value = row.LineText.Substring (start, pos - start);
            }

            // Add field to list
            if (rows < row.Count)
                row[rows] = value;
            else
                row.Add (value);
            rows++;

            // Eat up to and including next comma
            while (pos < row.LineText.Length && row.LineText[pos] != '\n')
                pos++;
            if (pos < row.LineText.Length)
                pos++;
        }
        // Delete any unused items
        while (row.Count > rows)
            row.RemoveAt (rows);

        // Return true if any columns read
        return (row.Count > 0);
    }
}