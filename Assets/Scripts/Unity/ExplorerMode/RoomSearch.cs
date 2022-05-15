using SmartHospital.Controller;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Model;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public partial class RoomSearch
{
    LuceneSearcher searcher = new LuceneSearcher();

    private GameObject[] roomTransformCollider;
    private List<GameObject> newRoomsOutput = new List<GameObject>();

    [SerializeField]
    public TMP_InputField searchInputFieldInput;

    private Material searchRendMaterial;
    private Material unselectedMaterial;

    private void Start()
    {
        //All rooms
        roomTransformCollider = GameObject.FindGameObjectsWithTag("RoomCollider");

        searchRendMaterial = (Material)Resources.Load("SearchMaterial", typeof(Material));
        unselectedMaterial = (Material)Resources.Load("UnselectedMaterial", typeof(Material));

        foreach (GameObject roomCollider in roomTransformCollider)
        {
            newRoomsOutput.Add(roomCollider);
        }

        searcher.createIndex(GetAllIngameRooms());

        if (Input.GetKey("enter"))
        {
            StartDetailSearch();
        }

    }

    /// <summary>
    /// String comparison - only start when both strings has more than 2 chars
    /// </summary>
    /// <param name="fistString">first string to compare with</param>
    /// <param name="secondString">second string to compare with the first</param>
    /// <returns>true if both string are equal, false if they are not equal</returns>
    private bool stringEqualComparison(string fistString, string secondString)
    {
        var charFirstString = fistString.ToLower().ToCharArray();
        var charSecondString = secondString.ToLower().ToCharArray();

        if (charSecondString.Length < 1 && charFirstString.Length < 1)
        {
            return false;
        }

        for (int i = 0; i < charFirstString.Length; i++)
        {
            if (charFirstString[i] != charSecondString[i])
            {
                return false;
            }
        }

        return true;

    }


    /// <summary>
    /// compares if one of the strings is in the other one
    /// </summary>
    /// <param name="firstString"></param>
    /// <param name="secondString"></param>
    /// <returns>return true if first string is in secondstring or secondstring is in the firststring</returns>
    private bool circaStringEqualComparison(string firstString, string secondString)
    {

        if (firstString.ToLower().Contains(secondString) || secondString.ToLower().Contains(firstString))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// checks four digits of the room number strings and checks if it exists in the other string
    /// </summary>
    /// <param name="fistString"></param>
    /// <param name="secondString"></param>
    /// <returns>returns true if four digits of the room number is existing in the other roomnumber string</returns>
    private bool roomNumberComparison(string fistString, string secondString)
    {
        var charFirstString = fistString.ToLower().ToCharArray();
        var charSecondString = secondString.ToLower().ToCharArray();

        if (charSecondString.Length < 4)
        {
            return false;
        }
        else if (charFirstString.Length < 4)
        {
            return true;
        }
        else if (fistString.ToLower().Contains(secondString.ToLower()) || secondString.ToLower().Contains(fistString.ToLower()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool checkInRoomStrings(ServerRoom room, string input)
    {
        var roomStrings = getAllRoomTagStrings(room);

        if (roomStrings.Contains(input))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// checks 2 lists and return one list with only newloadedrooms which are in both lists
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    private HashSet<string> GetMatchingRoomsListInTwoLists(HashSet<string> list1, HashSet<string> list2)
    {
        var output = new HashSet<string>();
        var foundListObjects = new List<string>();

        foreach (string obj in list1)
        {
            List<string> list2ToList = list2.ToList<string>();
            var list = list2ToList.FindAll(x => x == obj);
            foundListObjects.AddRange(list2ToList.FindAll(x => x == obj));
        }
        foreach (string listObj in foundListObjects)
        {
            output.Add(listObj);
        }

        return output;
    }

    /// <summary>
    /// Gets matching items in a listset of string items
    /// Limited to 3 Lists
    /// </summary>
    /// <param name="lists"></param>
    /// <returns>A Hashset with sorted objects, which matched in all lists</returns>
    public HashSet<string> GetMatchingItemsInHashSetLists(HashSet<string>[] lists)
    {
        HashSet<string> output = new HashSet<string>();

        List<string> foundListObjects = new List<string>();
        List<string> foundListObjects2 = new List<string>();

        //Check the first two lists for matches
        if (lists.Count() > 1)
        {
            foreach (string obj in lists[0])
            {
                var listToCheck = lists[1].ToList<string>();
                foundListObjects.AddRange(listToCheck.FindAll(x => x == obj));
            }

            if (lists.Count() > 2)
            {
                List<string> listToCompare = lists[2].ToList<string>();

                foreach (string found in foundListObjects)
                {
                    var foundObjects = listToCompare.FindAll(x => x == found);
                    foundListObjects2.AddRange(foundObjects);
                }

                output.UnionWith(foundListObjects2);
            }
            else
            {
                output.UnionWith(foundListObjects);
            }
        }

        return output;
    }

    /// <summary>
    /// Adds a hashset list array into one hashset list
    /// </summary>
    /// <param name="lists"></param>
    /// <returns></returns>
    private HashSet<string> AddTwoHashSetsToOne(HashSet<string>[] lists)
    {
        HashSet<string> output = new HashSet<string>();

        foreach (HashSet<string> list in lists)
        {
            output.UnionWith(list);
        }

        return output;
    }

    /// <summary>
    /// gets all room information of a room and returns a list of strings in the roominfo
    /// </summary>
    /// <param name="room">the room which should be split into strings</param>
    /// <returns>a list of strings of the room information</returns>
    private List<string> getAllRoomTagStrings(ServerRoom room)
    {
        List<string> roomStrings = new List<string>();
        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

        if (room.RoomName != null)
        {
            roomStrings.Add(room.RoomName.ToString());
            for (int i = 0; i < room.NumberOfWorkspaces; i++)
            {
                foreach (Workspace workspace in room.Workspaces)
                {
                    if (!workspace.Worker.Department.CostCentre.Equals(""))
                    {
                        roomStrings.Add(workspace.Worker.Department.CostCentre.ToString());
                    }
                }

            }
            if (room.NamePlate.BuildingSection != null)
                roomStrings.Add(room.NamePlate.BuildingSection.ToString());
            roomStrings.Add(room.NumberOfWorkspaces.ToString());

            string[] roomDesignationSplitwords = room.Designation.Split(delimiterChars);
            //string[] roomFunctionSplitwords = room.Functional_area.Name.Split(delimiterChars);
            //string[] specialistDepartmentwords = room.Department.Name.Split(delimiterChars);

            foreach (string word in roomDesignationSplitwords)
            {
                roomStrings.Add(word);
            }
            //TODO: Add departmets
            /**
            foreach (string specialistdepartmentWord in specialistDepartmentwords)
            {
                roomStrings.Add(specialistdepartmentWord);
            }

            foreach (string functionWord in roomFunctionSplitwords)
            {
                roomStrings.Add(functionWord);
            }**/
        }

        return roomStrings;
    }

    /// <summary>
    /// Room id search
    /// </summary>
    /// <param name="stringInput">input in the search field</param>
    /// <returns></returns>
    private HashSet<ServerRoom> searchRoomsByRoomID(string stringInput)
    {
        HashSet<ServerRoom> output = new HashSet<ServerRoom>();
        List<ServerRoom> loadedRooms = GetAllIngameRooms();

        string numberPattern = "^\\d{4}.\\d{2}.\\d{3}$";

        foreach (ServerRoom room in loadedRooms)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(room.RoomName, numberPattern))
            {
                if (room.RoomName.Equals(stringInput))
                {
                    output.Add(room);
                }
            }
        }

        return output;
    }

    public List<string> splitInputSearchWordsIntoList(string input)
    {
        List<string> inputWords = new List<string>();

        char[] charSplitter = { ' ', ',', ';', '*' };
        string[] textInputStrings = input.Split(charSplitter);

        HashSet<string> roomIds = new HashSet<string>();

        foreach (string l in textInputStrings)
        {
            if (l != null)
            {
                if (!l.Equals("") && !l.Equals(" "))
                {
                    inputWords.Add(l);
                }
            }

        }

        return inputWords;
    }


    /// <summary>
    /// Gets the text of the search input field and starts the lucene search
    /// </summary>
    public void StartDetailSearch()
    {
        //Get Text of InputField
        string textInput = searchInputFieldInput.text;
        List<string> inputWords = new List<string>();

        char[] charSplitter = { ' ', ',', ';', '*' };
        string[] textInputStrings = textInput.Split(charSplitter);



        foreach (string l in textInputStrings)
        {
            if (l != null)
            {
                if (!l.Equals("") && !l.Equals(" "))
                {
                    inputWords.Add(l);
                }
            }

        }

        HashSet<string> roomIds = new HashSet<string>();

        if (inputWords.Count <= 0)
        {
            markFoundObjects(null);
        }
        else
        {
            if (inputWords.Count <= 1)
            {
                if (!textInput.Equals(" "))
                    roomIds = useLuceneIndexSearch(textInput + "*");
                if (roomIds.Count <= 0)
                {
                    roomIds = internSearch(textInput);
                }

            }
            else
            {
                roomIds = MoreWordsLuceneSearch(inputWords);
            }

        }

        //marks the found objects

        markFoundObjects(roomIds);
    }

    public HashSet<string> useLuceneIndexSearch(string input)
    {
        HashSet<string> output = new HashSet<string>();

        output = searcher.useRoomIndex(input);

        return output;
    }

    public HashSet<string> MoreWordsLuceneSearch(List<string> words)
    {
        var wordsArray = words.ToArray();
        HashSet<string> output = new HashSet<string>();
        HashSet<string>[] lists = new HashSet<string>[words.Count];

        for (int i = 0; i < wordsArray.Length; i++)
        {
            if (!wordsArray[i].Equals("") && !wordsArray[i].Equals(" "))
            {
                var roomIds = useLuceneIndexSearch(wordsArray[i] + "*");
                if (roomIds.Count <= 0)
                {
                    roomIds = internSearch(wordsArray[i]);
                }
                lists[i] = roomIds;
            }
        }
        output = GetMatchingItemsInHashSetLists(lists);

        return output;
    }

    /// <summary>
    /// search if lucene search fails
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private HashSet<string> internSearch(string input)
    {
        HashSet<string> output = new HashSet<string>();
        List<ServerRoom> loadedRooms = GetAllIngameRooms();

        foreach (ServerRoom room in loadedRooms)
        {
            var roomSplitStrings = getAllRoomTagStrings(room);
            foreach (string s in roomSplitStrings)
            {
                if (s != null && !s.Equals(""))
                {
                    if (s.ToLower().Contains(input.ToLower()) && s.Length > 1)
                    {
                        output.Add(room.RoomName);
                    }
                }

            }
        }

        return output;
    }

    /// <summary>
    /// Gets and returns all rooms in the Scene with the Collider "RoomCollider"
    /// </summary>
    /// <returns>A list of rooms in the scene</returns>
    public static List<ServerRoom> GetAllIngameRooms()
    {
        var gameObjectsWithRoomInfo = GameObject.FindGameObjectsWithTag("RoomCollider");
        List<ServerRoom> rooms = new List<ServerRoom>();

        foreach (GameObject gameObjectWithRoomInfo in gameObjectsWithRoomInfo)
        {
            if (gameObjectWithRoomInfo.GetComponent<ClientRoom>() != null
                && gameObjectWithRoomInfo.GetComponent<ClientRoom>().RoomName != null)
            {
                rooms.Add(gameObjectWithRoomInfo.GetComponent<ClientRoom>().MyRoom);
            }
        }
        return rooms;
    }

    public static HashSet<GameObject> GetAllGORooms()
    {
        var gameObjectsWithRoomInfo = GameObject.FindGameObjectsWithTag("RoomCollider");
        HashSet<GameObject> output = new HashSet<GameObject>();

        foreach (GameObject go in gameObjectsWithRoomInfo)
        {
            output.Add(go);
        }

        return output;
    }


    /// <summary>
    /// Changes the Material of the Collider
    /// </summary>
    /// <param name="foundObjectIds">found Object ids</param>
    public void markFoundObjects(HashSet<string> foundObjectIds)
    {
        var roomColl = GameObject.FindGameObjectsWithTag("RoomCollider");

        MainSceneUIController.ResetAllRoomColliderMaterials();

        if (foundObjectIds != null)
        {
            //marked state
            foreach (GameObject room in roomColl)
            {
                var roomCollider = room.GetComponent<RoomColliderMaterialController>();
                if (room.GetComponent<ClientRoom>() != null)
                {
                    if (foundObjectIds.Contains(room.GetComponent<ClientRoom>().RoomName))
                    {
                        room.GetComponent<Renderer>().material = searchRendMaterial;
                        roomCollider.setCurrentMaterial(searchRendMaterial);
                        roomCollider.setSearchResultActive(true);
                    }
                }

            }
        }


    }
}
