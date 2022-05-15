using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.Controller.ExplorerMode.Rooms.Details
{
    public class DetailSearch : MonoBehaviour
    {
        SearchDetails searchDetails = new SearchDetails();
        List<SearchDetails> roomSearchDetails = new List<SearchDetails>();

        TMP_InputField room_Id_Input;
        TMP_InputField cost_centre_Input;
        TMP_Dropdown floor;
        TMP_Dropdown specialist_department;
        GameObject[] designation_button_container;
        GameObject[] floor_label_button_container;
        TMP_Dropdown workspace_number;

        [Header("Detail Search Panel")]
        public GameObject detail_Search_Panel;

        private RoomSearch searcher;

        /// <summary>
        /// Initialise the button container/ inputfields of the detail search
        /// </summary>
        private void Awake()
        {
            /**
            floor = detail_Search_Panel.transform.Find("EbeneDropDown").GetComponent<TMP_Dropdown>();
            specialist_department = detail_Search_Panel.transform.Find("FunctionDropDown").GetComponent<TMP_Dropdown>();
            workspace_number = detail_Search_Panel.transform.Find("WorkingStationsDropdown").GetComponent<TMP_Dropdown>();

            room_Id_Input = detail_Search_Panel.transform.Find("RaumNummer").GetComponent<TMP_InputField>();
            cost_centre_Input = detail_Search_Panel.transform.Find("Kostenstelle").GetComponent<TMP_InputField>();

            var buildingSectionPanel = detail_Search_Panel.transform.Find("BuildingsectionPanel");
            var designationPanel = detail_Search_Panel.transform.Find("Raumbezeichnungspanel");

            var childCountDesignation = designationPanel.transform.childCount;
            var childCountBuildingSection = buildingSectionPanel.transform.childCount;

            designation_button_container = new GameObject[childCountDesignation];
            floor_label_button_container = new GameObject[childCountBuildingSection];


            for (int i = 0; i < childCountDesignation; i++)
            {
                designation_button_container[i] = designationPanel.GetChild(i).gameObject;
            }

            for (int j = 0; j < childCountBuildingSection; j++)
            {
                floor_label_button_container[j] = buildingSectionPanel.GetChild(j).gameObject;
            }
    **/
        }

        private void Start()
        {
            searcher = FindObjectOfType<RoomSearch>();
        }

        /// <summary>
        /// starts the detail search
        /// </summary>
        public void detailSearch()
        {
            List<string> searchInput = new List<string>();
            List<string> searchDoubleInput = new List<string>();

            string roomID = room_Id_Input.text;
            if (!roomID.Equals(""))
                searchInput.Add(roomID);

            string cost_centre = cost_centre_Input.text;
            if (!cost_centre.Equals(""))
                searchInput.Add(cost_centre);

            switch (floor.value)
            {
                case 0:
                    searchInput.Add("6420");
                    break;
                default:
                    searchInput.Add("6420." + floor.options[floor.value].text);
                    break;
            }

            switch (workspace_number.value)
            {
                case 0:
                    searchInput.Add("6420");
                    break;
                default:
                    searchInput.Add(workspace_number.options[workspace_number.value].text + "AP");
                    break;
            }

            switch (specialist_department.value)
            {
                case 0:
                    searchInput.Add("6420");
                    break;
                default:
                    searchInput.Add(specialist_department.options[specialist_department.value].text);
                    break;

            }

            bool[] toggleBuildingSection = new bool[floor_label_button_container.Length + 1];
            for (int buildingSectionCount = 0; buildingSectionCount < floor_label_button_container.Length; buildingSectionCount++)
            {
                if (floor_label_button_container[buildingSectionCount].GetComponent<Toggle>().isOn)
                {
                    toggleBuildingSection[buildingSectionCount] = true;
                }
                else
                {
                    toggleBuildingSection[buildingSectionCount] = false;
                }
            }
            //Adds the floor label to the searchDoubleInputs
            for (int count = 0; count < toggleBuildingSection.Length; count++)
            {
                if (toggleBuildingSection[count])
                {
                    searchDoubleInput.Add(floor_label_button_container[count].GetComponentInChildren<Text>().text);
                }
            }

            bool[] toggleRoomDesignation = new bool[designation_button_container.Length + 1];

            for (int roomDesignationCount = 0; roomDesignationCount < designation_button_container.Length; roomDesignationCount++)
            {
                if (designation_button_container[roomDesignationCount].GetComponent<Toggle>().isOn)
                {
                    toggleRoomDesignation[roomDesignationCount] = true;
                }
                else
                {
                    toggleRoomDesignation[roomDesignationCount] = false;
                }
            }
            for (int countDesignation = 0; countDesignation < toggleRoomDesignation.Length; countDesignation++)
            {
                if (toggleRoomDesignation[countDesignation])
                {
                    searchDoubleInput.Add(designation_button_container[countDesignation].GetComponentInChildren<Text>().text);
                }
            }
            var output = searcher.MoreWordsLuceneSearch(searchInput);
            HashSet<string> output2 = new HashSet<string>();

            if (searchDoubleInput.Count >= 1)
            {
                foreach (string listitem in searchDoubleInput)
                {
                    var searchResult = searcher.useLuceneIndexSearch(listitem);
                    output2.UnionWith(searchResult);
                }


                HashSet<string>[] lists = { output, output2 };
                output = searcher.GetMatchingItemsInHashSetLists(lists);
            }
            searcher.markFoundObjects(output);

        }
    }
}

