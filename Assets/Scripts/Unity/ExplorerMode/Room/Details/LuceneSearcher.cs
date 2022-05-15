using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Model;
using System.Collections.Generic;

/// <summary>
/// Created 11.2018 by KS
/// Apache Lucene search for the 
/// </summary>
public class LuceneSearcher
{
    RAMDirectory ramDir;
    string[] inventoryAttributes = { "Item_Code", "Name", "Designation", "Product_Group", "Department", "Producer", "Room" };
    string[] roomAttributes = {
            "Room_id","Person_Specialist_Department","AccessStyle","Person_FunctionalAreaName", "Person_FunctionalAreaType","Designation","Door_plate_number", "Workspace_number","Tags","Room_Size" ,"Building_section",
            "Person_CostCentre","Person_ProfessionalGroup","Person_Lastname","Person_Firstname","Person_StaffId", "Person_EmployeeId"};
    /// <summary>
    /// Creates the Lucene index for Room search
    /// </summary>
    /// <param name="list"></param>
    public void createIndex(List<ServerRoom> list)
    {
        ramDir = new RAMDirectory();

        var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
        var writer = new IndexWriter(ramDir, analyzer, IndexWriter.MaxFieldLength.LIMITED);

        //For each room add lucene index info
        foreach (var sampleData in list)
        {
            var doc = new Document();
            //Add the room info to lucene index
            doc.Add(new Field("Room_id", sampleData.RoomName.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("AccessStyle", sampleData.AccessStyle, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Door_plate_number", sampleData.NamePlate.RoomName, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Designation", sampleData.Designation.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Workspace_number", sampleData.NumberOfWorkspaces.ToString(), Field.Store.YES, Field.Index.ANALYZED));

            if (sampleData.NamePlate.BuildingSection != null)
                doc.Add(new Field("Building_section", sampleData.NamePlate.BuildingSection, Field.Store.YES, Field.Index.ANALYZED));
            //Add worker to index
            if (sampleData.WorkersWithAccess != null)
                foreach (Worker p in sampleData.WorkersWithAccess)
                {
                    doc.Add(new Field("Person_Firstname", p.FirstName, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Person_Lastname", p.LastName, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Person_StaffId", p.Staff_Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Person_EmployeeId", p.Employee_Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("Person_CostCentre", p.Department.CostCentre.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    if (p.Professional_Group != null && p.Professional_Group.Name != null)
                        doc.Add(new Field("Person_ProfessionalGroup", p.Professional_Group.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    if (p.Department != null && p.Department.FunctionalAreas != null)
                    {
                        if (p.Department.Name != null)
                            doc.Add(new Field("Person_Specialist_Department", p.Department.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                        foreach (FunctionalArea funcArea in p.Department.FunctionalAreas)
                        {
                            doc.Add(new Field("Person_FunctionalAreaName", funcArea.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                            doc.Add(new Field("Person_FunctionalAreaType", funcArea.Type.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                        }
                    }

                }


            writer.AddDocument(doc);

        }

        writer.Optimize();
        writer.Commit();
        writer.Dispose();
    }
    /// <summary>
    /// Creates the Lucene index for Inventory search
    /// </summary>
    /// <param name="list">Inventory Items for the search list</param>
    public void createIndex(List<InventoryItem> inventoryItemList)
    {
        ramDir = new RAMDirectory();

        var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
        var writer = new IndexWriter(ramDir, analyzer, IndexWriter.MaxFieldLength.LIMITED);

        //For each room add lucene index info
        foreach (var sampleData in inventoryItemList)
        {
            var doc = new Document();
            //Add the room info to lucene index

            doc.Add(new Field("Item_Code", sampleData.Item_Code, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Name", sampleData.Name, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Designation", sampleData.Designation, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Product_Group", sampleData.Product_Group, Field.Store.YES, Field.Index.ANALYZED));

            if (sampleData.Department != null)
                doc.Add(new Field("Department", sampleData.Department.Name, Field.Store.YES, Field.Index.ANALYZED));

            if (sampleData.Producer != null)
                doc.Add(new Field("Producer", sampleData.Producer.Name, Field.Store.YES, Field.Index.ANALYZED));

            if (sampleData.Room != null)
                doc.Add(new Field("Room", sampleData.Room.RoomName, Field.Store.YES, Field.Index.ANALYZED));


            writer.AddDocument(doc);

        }

        writer.Optimize();
        writer.Commit();
        writer.Dispose();
    }

    /// <summary>
    /// search term for lucene - search in the index directory
    /// </summary>
    /// <param name="text"></param>
    public HashSet<string> useRoomIndex(string text)
    {
        var output = new HashSet<string>();
        //File Directory use
        //var directory = FSDirectory.Open(new DirectoryInfo(@"C:/unity_lucene"));
        Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

        //Ram Directory
        IndexReader reader = DirectoryReader.Open(ramDir, true);

        var parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, roomAttributes, analyzer);
        Query query = parser.Parse(text);
        var searcher = new IndexSearcher(reader);
        TopDocs topDocs = searcher.Search(query, 5000);
        int results = topDocs.ScoreDocs.Length;

        for (int i = 0; i < results; i++)
        {
            ScoreDoc scoreDoc = topDocs.ScoreDocs[i];
            float score = scoreDoc.Score;
            int docId = scoreDoc.Doc;
            Document doc = searcher.Doc(docId);
            output.Add("" + doc.Get("Room_id"));
        }

        return output;
    }

    /// <summary>
    /// search term for lucene - search in the index directory for inventory
    /// </summary>
    /// <param name="text"></param>
    public HashSet<string> useInventoryIndex(string text)
    {
        var output = new HashSet<string>();
        //File Directory use
        //var directory = FSDirectory.Open(new DirectoryInfo(@"C:/unity_lucene"));
        Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

        //Ram Directory
        IndexReader reader = DirectoryReader.Open(ramDir, true);

        var parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, inventoryAttributes, analyzer);
        Query query = parser.Parse(text);
        var searcher = new IndexSearcher(reader);
        TopDocs topDocs = searcher.Search(query, 5000);
        int results = topDocs.ScoreDocs.Length;

        for (int i = 0; i < results; i++)
        {
            ScoreDoc scoreDoc = topDocs.ScoreDocs[i];
            float score = scoreDoc.Score;
            int docId = scoreDoc.Doc;
            Document doc = searcher.Doc(docId);
            output.Add("" + doc.Get("Item_Code"));
        }

        return output;
    }



    /// <summary>
    /// search term for lucene - search in the index directory
    /// </summary>
    /// <param name="text"></param>
    public HashSet<string> useIndex(string text, string[] attributes)
    {
        var output = new HashSet<string>();
        //File Directory use
        //var directory = FSDirectory.Open(new DirectoryInfo(@"C:/unity_lucene"));
        Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

        //Ram Directory
        IndexReader reader = DirectoryReader.Open(ramDir, true);

        var parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, attributes, analyzer);
        Query query = parser.Parse(text);
        var searcher = new IndexSearcher(reader);
        TopDocs topDocs = searcher.Search(query, 5000);
        int results = topDocs.ScoreDocs.Length;

        for (int i = 0; i < results; i++)
        {
            ScoreDoc scoreDoc = topDocs.ScoreDocs[i];
            float score = scoreDoc.Score;
            int docId = scoreDoc.Doc;
            Document doc = searcher.Doc(docId);
            output.Add("" + doc.Get("Room_id"));
        }

        return output;
    }
}

