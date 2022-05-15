using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


/// <summary>
/// Author SDA + Internet. August 2019
/// 
/// For adding many elements to an building array.
/// </summary>

[System.Serializable]
public class PlaceableObj
{
    [SerializeField]
    private string name;                                // The Name
    
    [SerializeField]
    public GameObject placeableObjectPrefab;            // The Prefab

    [SerializeField]
    public KeyCode newObjectHotkey = KeyCode.A;        // The Hotkey for Keyboard Access

}

/// <summary>
/// Author SDA + Internet. August 2019
/// </summary>

public class GroundPlacementControler : MonoBehaviour
{
    
        [Header("DebugText")]
        public TextMeshProUGUI Display_Pos; // Show Vector 3
        public TextMeshProUGUI DisplayLayer; // Shpw Layer

        [Header("Camera")]
        public Camera BuildCamera;



        [Header("Buildings")]

        public PlaceableObj [] placeableObjects; // The Array features a complete List for possible (builable) Objects
        private GameObject currentPlaceableObject;
        private ObjectActivationManager currentObjectsActivationManager;


        private float mouseWheelRotation;

        public Messanger messanger;
        int lastBuildingIndex;

        



    [Header("Grid")]
    bool PlaceInGrid; // local bool for UI Toggle
    public Toggle toggleForGridBool; // UI Toggle

    [Header("Fine Grid")]
    bool PlaceInFineGrid; // local bool for UI Toggle
    public Toggle toggleForFineGridBool; // UI Toggle

    [Header("Array")]
    bool PlaceInArray; // local bool for UI Toggle
    public Toggle toggleForArrayBool; // UI Toggle


    [Header("Slider")]
    public Slider AngleForCamera; // local bool for UI Toggle
    public Slider AngleForCamera_X; // UI Toggle


    [Header("Effects")]
    public ParticleSystem particleSystem_Placement;




    int CameraMode = 0;
        

        private void Update()
        {
       
            HandleNewObjectHotkey();    // For Hotkeys to build a certain obj - declared in the array
            Options_Hotkeys(); // For Hotkeys of all the possible Options


            if (CameraMode == 0) // Camera Mode 0 = normale Game Camera
            {
                BuildCamera.transform.localRotation = Quaternion.Euler(AngleForCamera.value, AngleForCamera_X.value, 0.0f);
            }


            if (currentPlaceableObject != null && BuildCamera != null)
            {
                MoveCurrentObjectToMouse(); // Sets the current object to the mouse.

                RotateFromMouseWheel(); // Rotate the chosen object.

                ReleaseIfClicked(); // Releases the current object via leftklick

  
            }


        UpdateDisplayData(); // Show Position of laceable object on the top left as Vector 3

    }



    /// <summary>
    /// Take current Bool value from the UI toggle and write it into a local variable.
    /// </summary>
    void Options_Hotkeys()
    {
        PlaceInGrid = toggleForGridBool.isOn;               // Takte the curretn Bool value from UI Toggle
        PlaceInFineGrid = toggleForFineGridBool.isOn;       // Takte the curretn Bool value from UI Toggle
        PlaceInArray = toggleForArrayBool.isOn;             // Takte the curretn Bool value from UI Toggle

        #region Input to toggle cia Hotkey

        if (Input.GetKeyDown(KeyCode.G))
        {

            PlaceInGrid = !PlaceInGrid;
            toggleForGridBool.isOn = PlaceInGrid;
        }



        if (Input.GetKeyDown(KeyCode.F))
        {

            PlaceInFineGrid = !PlaceInFineGrid;
            toggleForFineGridBool.isOn = PlaceInFineGrid;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeBuildCameraView();
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCameraLenseOthroPerspektive();   
        }
        #endregion
    }



    public void ChangeBuildCamera_YPos_Plus()
    {
        BuildCamera.transform.position = BuildCamera.transform.position + new Vector3(0, +1, 0);
    }

    public void ChangeBuildCamera_YPos_Minus()
    {

        BuildCamera.transform.position = BuildCamera.transform.position + new Vector3(0, -1, 0);
    }




    /// <summary>
    /// Change Camera view from classic 3D to a Top Down view. Extra complexity because the gap from the viewable.
    /// </summary>
    public void ChangeBuildCameraView()
    {
        if (CameraMode == 0)
        {
            
           Ray ray = BuildCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2,0));
           RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.name == "Ground")
                {
                    BuildCamera.transform.position = new Vector3(hitInfo.point.x, BuildCamera.transform.position.y, hitInfo.point.z);
                }
            }


            BuildCamera.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
            CameraMode += 1;
            
            return;
        }

        if (CameraMode == 1)
        {
            CameraMode = 0;
            return;

        }


        //new Vector3(45.0f, 0.0f, 0.0f);

    }

    /// <summary>
    /// Toggle Camera (orthographic to perspective)
    /// </summary>
    public void ToggleCameraLenseOthroPerspektive()
    {
        BuildCamera.orthographic = !BuildCamera.orthographic;
    }


    /// <summary>
    /// Convert the Position of the currentplaceable object as a string  to the screen and shows the Vector3(X;Y;Z)
    /// </summary>
    void UpdateDisplayData()
    {
        if(currentPlaceableObject != null)
        Display_Pos.text = currentPlaceableObject.transform.position.ToString();
    }   


    /// <summary>
    /// Creates the Object ( instantiate) and writes it to "currentplaceable objects". Also we get a handle to the currentObjectActiovationManager, that is responsible for the ghosting phase.
    /// </summary>
    /// <param name="indexOfArray"></param>
        public void CreateBuilding(int indexOfArray)
        {

            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
            }
            else
            {
                lastBuildingIndex = indexOfArray;
                currentPlaceableObject = Instantiate(placeableObjects[indexOfArray].placeableObjectPrefab);
                currentObjectsActivationManager = currentPlaceableObject.GetComponent<ObjectActivationManager>();
            }
         } 

    /// <summary>
    /// Hotkey Handling, defined in the inspector
    /// </summary>
        private void HandleNewObjectHotkey()
        {
            for (int i = 0; i < placeableObjects.Length; i++)
            {
                if(Input.GetKeyDown(placeableObjects[i].newObjectHotkey))
                {   
                    CreateBuilding(i);
                }
            }
        }


    /// <summary>
    /// Externally creating an object ( type) with pos and setting it on the space with an vector 3.
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_pos"></param>
    public void CreateObjectExternal(int _type, Vector3 _pos)
    {

        currentObjectsActivationManager = null;
        currentPlaceableObject = null;

        //Destroy(currentPlaceableObject);
            

        CreateBuilding(_type);

        currentPlaceableObject.transform.position = _pos;

        SetDownBuilding();

        currentObjectsActivationManager = null;
        currentPlaceableObject = null;
    }



    /// <summary>
    /// Strays out a ray to find the groundplane, where the objects can be placed.
    /// </summary>
    private void MoveCurrentObjectToMouse()
    {
        Ray ray = BuildCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.name == "Ground")
            {
                if (PlaceInGrid)
                {
                    Vector3 RoundedVec;

                    if (PlaceInFineGrid)
                        RoundedVec = hitInfo.point.Round(1);
                    else
                        RoundedVec = hitInfo.point.Round(0);

                    currentPlaceableObject.transform.position = new Vector3(RoundedVec.x, hitInfo.point.y, RoundedVec.z);
                }
                else
                {
                    currentPlaceableObject.transform.position = hitInfo.point;
                }

                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                Debug.Log(currentPlaceableObject.transform.position);
            }
        }

    }


    /// <summary>
    /// Rotates the currentplaceableobject with the mousewheel.
    /// </summary>
        private void RotateFromMouseWheel()
        {
            Debug.Log(Input.mouseScrollDelta);

            mouseWheelRotation += Input.mouseScrollDelta.y;
            currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        }


    /// <summary>
    /// Sets down the building. displaying the name, shooting the particles.
    /// When player holds "Shift", The System creates another building of the last type for array placement.
    /// </summary>


    private void SetDownBuilding()
    {
        // = PARTICLE SYSTEM
        particleSystem_Placement.transform.position = currentPlaceableObject.transform.position;
        particleSystem_Placement.Play();


        // Activatemanager - Activate Object from Ghosting Phase and release it.
        currentObjectsActivationManager.SetObjectIntoWorld();
        currentObjectsActivationManager = null;

        //Display
        messanger.DisplayMessage(currentPlaceableObject.name + " Set", 1.0f);

        // Releases RAM.
        currentPlaceableObject = null;


        // creat Objects in Array
        if (Input.GetKey(KeyCode.LeftShift) || PlaceInArray)
        {

            CreateBuilding(lastBuildingIndex);
        }

    }

        /// <summary>
        ///  Release the building.
        /// </summary>
        private void ReleaseIfClicked()
        {
            // by leftklicking the mouse, user creates the ibject if......
            if (Input.GetMouseButtonDown(0))
            {
                //.......... There is no colission.
                // Check if object has contact with other building
                // when yes => Dont allow Object
                // when no => Place Object

                if (currentObjectsActivationManager.AllowPlacement)
                {
                    SetDownBuilding();
                }
                else
                {
                    // MESSAGE
                    if (messanger != null)
                        messanger.DisplayMessage("NO!", 1.0f);
                }
            }

            // EXIT: Get out of situation by rightclicking the mouse
            if (Input.GetMouseButtonDown(1))
            {
                // Dont place object and destroy it
                Destroy(currentPlaceableObject.gameObject);

                currentObjectsActivationManager = null;
                 currentPlaceableObject = null;

             }

        }

}


static class ExtensionMethods
{
    /// <summary>
    /// Rounds Vector3.
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="decimalPlaces"></param>
    /// <returns></returns>
    public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }
}
