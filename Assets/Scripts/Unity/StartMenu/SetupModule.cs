using SmartHospital.ExplorerMode.CameraControl;
using SmartHospital.ExplorerMode.Rooms.Locator;
using SmartHospital.ExplorerMode.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SmartHospital.StartMenu
{
    public class SetupModule : MonoBehaviour
    {
        public enum SetupMode
        {
            Debug,
            Release
        }

        //public readonly string floorScenesPath = "Assets/_Scenes/Floors";
        //public readonly string sceneNamePattern = "_Floor";
        const string ColliderNamePattern = @"F[\d]+W[\d]+R[\d]+";

        Dictionary<int, string> floorSceneNames;

        public SetupMode Mode = SetupMode.Debug;

        public string UnityId = "-1";

#if UNITY_WEBGL
        [DllImport("__Internal")]
        static extern void UnityReady();
#endif

        void Awake()
        {
            if (Values.CurrentPlatform == Values.Platform.WebGL)
            {
                Mode = SetupMode.Release;
            }

            DontDestroyOnLoad(gameObject);

            Debug.Log("Entering unity modul in " + Mode + " mode");

            //floorSceneNames = BuildDictionary();
            floorSceneNames = Values.FloorSceneNames;

            SceneManager.sceneLoaded += AutoDestroy;
        }

        void Start()
        {
            if (Mode == SetupMode.Debug)
            {
                if (UnityId == "-2")
                {
                    StartCoroutine(StartSceneLoadingSequence(int.MinValue));
                }
                else
                {
                    FocusOnObject(UnityId);
                }
            }
            //TODO
            else if (Mode == SetupMode.Release)
            {
                try
                {
#if UNITY_WEBGL
                    UnityReady();
#endif
                }
                catch (Exception e)
                {
                    Debug.LogError("UnityReady function not on webpage" + e);
                }
            }
        }

        void AutoDestroy(Scene scene, LoadSceneMode lsm)
        {
            if (!new Regex("Assets/_Scenes/Floors").IsMatch(scene.path))
            {
                return;
            }

            SceneManager.sceneLoaded -= AutoDestroy;

            var room = GameObject.Find(UnityId);

            if (room)
            {
                var roomPosition = room.GetComponent<Renderer>().bounds.center;
                var cam = GameObject.Find("CameraManager").GetComponentInChildren<CameraManager>().MainCamera;

                cam.InitLerpToRoom(roomPosition);

                var scriptHolder = GameObject.Find("ScriptHolder");
                var ma = scriptHolder.GetComponentInChildren<ColliderHandler>();
                ma.SetCurrentCollider(room.GetComponent<Collider>());
                ma.SelectCurrentCollider(room.GetComponent<Collider>(), RoomSelectionMode.Single);

                var dt = scriptHolder.GetComponent<DimensionToggle>();
                dt.Toggle3D(true);

                var fc = GameObject.Find("FocusControl").GetComponent<FocusControl>();
                fc.FocusCanvas(false, "Setup", true);
            }

            Destroy(gameObject);
        }

        public void FocusOnObject(string unityID)
        {
            UnityId = unityID;

            if (unityID == "-1")
            {
                SceneManager.LoadScene("StartMenu");
            }
            else
            {
                var rx = new Regex(ColliderNamePattern);

                if (!rx.IsMatch(unityID))
                {
                    return;
                }

                var number = ConvertFloorNumber(int.Parse(unityID.Substring(1, unityID.IndexOf('W') - 1)));
                print("Floor number is: " + number);
                StartCoroutine(StartSceneLoadingSequence(number));
            }
        }

        //TODO Temporaer
        public void TakeUserInput(int floor)
        {
            StartCoroutine(StartSceneLoadingSequence(floor));
        }

        IEnumerator StartSceneLoadingSequence(int floorID)
        {
            var loading = SceneManager.LoadSceneAsync("ExplorerMode");

            while (!loading.isDone)
            {
                yield return null;
            }

            SetUpSlider();
            GameObject.Find("Controller").GetComponentInChildren<FloorControl>().Initialize(floorID, floorSceneNames);
        }

        void SetUpSlider()
        {
            var slider = GameObject.Find("FloorSliderPane").GetComponentInChildren<Slider>();

            slider.minValue = floorSceneNames.Keys.Min();
            slider.maxValue = floorSceneNames.Keys.Max();
        }

        //    Dictionary<int, string> BuildDictionary() {
        //
        //        DirectoryInfo directoryInfo = new DirectoryInfo(floorScenesPath);
        //        Dictionary<int, string> floorDictionary = new Dictionary<int, string>();
        //
        //        Regex rx = new Regex(@"[\d]+[" + sceneNamePattern + "]");
        //
        //        foreach (FileInfo fi in directoryInfo.GetFiles()) {
        //
        //            if (fi.Extension == ".unity") {
        //
        //                int startIndex = fi.FullName.LastIndexOf("\\") + 1;
        //                int length = fi.FullName.LastIndexOf(".") - startIndex;
        //                string name = fi.FullName.Substring(startIndex, length);
        //
        //                if (rx.IsMatch(name)) {
        //
        //                    int number = ConvertFloorNumber(int.Parse(name.Replace(sceneNamePattern, "")));
        //
        //                    floorDictionary.Add(number, name);
        //                }
        //            }
        //        }
        //
        //        return floorDictionary;
        //    }

        int ConvertFloorNumber(int number)
        {
            //TODO Verbessern, Generalisieren usw.
            if (number > 50 && number < 100)
            {
                number = number - 100;
            }

            return number;
        }
    }
}