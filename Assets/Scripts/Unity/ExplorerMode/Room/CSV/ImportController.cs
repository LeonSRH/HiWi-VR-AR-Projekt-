using SmartHospital.Controller.ExplorerMode.Rooms;
using System.Windows.Forms;
using UnityEngine;

public class ImportController : MonoBehaviour
{

    private int roomName, visibleRoomName, accessStyle, comments, size, designation, dayLight, labor, radioactivity, roomUsability, roomUser, endOfUse, sitPossibilities;

    private string Path { get; set; }

    private CSVController csvController;


    private void Start()
    {
        csvController = GameObject.FindObjectOfType<CSVController>();

    }
    public void ShowDialog()
    {
        OpenFileDialog openDialog = new OpenFileDialog();
        openDialog.Title = "Select A File";
        openDialog.Filter = "CSV Files (*.csv)|*.csv" + "|" +
                            "All Files (*.*)|*.*";
        if (openDialog.ShowDialog() == DialogResult.OK)
        {
            string file = openDialog.FileName;
            //pathInput.text = file;
            Path = file;
        }

    }

    void ReadTest()
    {
        // Read sample data from CSV file
        using (CsvFileReader reader = new CsvFileReader(Path))
        {
            CsvRow row = new CsvRow();
            while (reader.ReadRow(row))
            {
                foreach (string s in row)
                {
                    //String spalten und über rows schleifen
                }
            }
        }
    }




}



