using System.Collections.Generic;
using UnityEngine;

public class CSVTuerliste : MonoBehaviour
{

    public TextAsset file;

    private void Start()
    {
        Load(file);
        var rowlist = GetRowList();
    }

    public class Row
    {
        public string TechnischeRaunmummer;
        public string Bauteil;
        public string TextErsteZeile;
        public string TextZweiteZeile;
        public string TextDritteZeile;
        public string Style;
        public string LeitendeRaumnummerKlein;
        public string LeitendeRaumnummerGross;
        public string Piktogramm;
        public string ZusatzAufTuerblatt;

    }

    List<Row> rowList = new List<Row>();
    bool isLoaded = false;

    public bool IsLoaded()
    {
        return isLoaded;
    }

    public List<Row> GetRowList()
    {
        return rowList;
    }

    public void Load(TextAsset csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv.text);
        for (int i = 1; i < grid.Length; i++)
        {
            Row row = new Row();
            row.TechnischeRaunmummer = grid[i][0];
            row.Bauteil = grid[i][1];
            row.TextErsteZeile = grid[i][2];
            row.TextZweiteZeile = grid[i][3];
            row.TextDritteZeile = grid[i][4];
            row.Style = grid[i][5];
            row.LeitendeRaumnummerKlein = grid[i][6];
            row.LeitendeRaumnummerGross = grid[i][7];
            row.Piktogramm = grid[i][8];
            row.ZusatzAufTuerblatt = grid[i][9];

            rowList.Add(row);
        }
        isLoaded = true;
    }

    public int NumRows()
    {
        return rowList.Count;
    }

    public Row GetAt(int i)
    {
        if (rowList.Count <= i)
            return null;
        return rowList[i];
    }

    public Row Find_TechnischeRaunmummer(string find)
    {
        return rowList.Find(x => x.TechnischeRaunmummer == find);
    }
    public List<Row> FindAll_TechnischeRaunmummer(string find)
    {
        return rowList.FindAll(x => x.TechnischeRaunmummer == find);
    }
    public Row Find_Bauteil(string find)
    {
        return rowList.Find(x => x.Bauteil == find);
    }
    public List<Row> FindAll_Bauteil(string find)
    {
        return rowList.FindAll(x => x.Bauteil == find);
    }
    public Row Find_TextErsteZeile(string find)
    {
        return rowList.Find(x => x.TextErsteZeile == find);
    }
    public List<Row> FindAll_TextErsteZeile(string find)
    {
        return rowList.FindAll(x => x.TextErsteZeile == find);
    }
    public Row Find_TextZweiteZeile(string find)
    {
        return rowList.Find(x => x.TextZweiteZeile == find);
    }
    public List<Row> FindAll_TextZweiteZeile(string find)
    {
        return rowList.FindAll(x => x.TextZweiteZeile == find);
    }
    public Row Find_TextDritteZeile(string find)
    {
        return rowList.Find(x => x.TextDritteZeile == find);
    }
    public List<Row> FindAll_TextDritteZeile(string find)
    {
        return rowList.FindAll(x => x.TextDritteZeile == find);
    }
    public Row Find_Style(string find)
    {
        return rowList.Find(x => x.Style == find);
    }
    public List<Row> FindAll_Style(string find)
    {
        return rowList.FindAll(x => x.Style == find);
    }
    public Row Find_LeitendeRaumnummerKlein(string find)
    {
        return rowList.Find(x => x.LeitendeRaumnummerKlein == find);
    }
    public List<Row> FindAll_LeitendeRaumnummerKlein(string find)
    {
        return rowList.FindAll(x => x.LeitendeRaumnummerKlein == find);
    }
    public Row Find_LeitendeRaumnummerGross(string find)
    {
        return rowList.Find(x => x.LeitendeRaumnummerGross == find);
    }
    public List<Row> FindAll_LeitendeRaumnummerGross(string find)
    {
        return rowList.FindAll(x => x.LeitendeRaumnummerGross == find);
    }
    public Row Find_Piktogramm(string find)
    {
        return rowList.Find(x => x.Piktogramm == find);
    }
    public List<Row> FindAll_Piktogramm(string find)
    {
        return rowList.FindAll(x => x.Piktogramm == find);
    }
    public Row Find_ZusatzAufTuerblatt(string find)
    {
        return rowList.Find(x => x.ZusatzAufTuerblatt == find);
    }
    public List<Row> FindAll_ZusatzAufTuerblatt(string find)
    {
        return rowList.FindAll(x => x.ZusatzAufTuerblatt == find);
    }
}
