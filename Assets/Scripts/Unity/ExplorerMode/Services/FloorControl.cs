using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SmartHospital.Common;
using SmartHospital.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SmartHospital.ExplorerMode.Services {
    public class FloorControl : BaseController {
        public Button downButton;

        Dictionary<int, string> floorSceneNames;
        readonly bool initialized = false;

        public LoadingPanel loadingPanel;
        int maxFloor;
        int minFloor;
        string currentSceneName;

        public Slider slider;
        public Button upButton;

        public int Floor { get; set; }

        void Awake() {
            upButton.onClick.AddListener(OnUpClick);
            downButton.onClick.AddListener(OnDownClick);
        }

        void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (scene.name.ToLower().Contains("floor")) {
                print("Loading finished, Scene IsActive: " + scene.name);
                SceneManager.SetActiveScene(scene);
                slider.value = Floor;
            }
        }

        public void Initialize(int floorID, Dictionary<int, string> floorSceneNames) {
            if (initialized) {
                return;
            }

            this.floorSceneNames = floorSceneNames;

            minFloor = this.floorSceneNames.Keys.Min();
            maxFloor = this.floorSceneNames.Keys.Max();

            Floor = floorID;

            //TODO
            Camera.main.transform.position = Values.CameraPositions[Floor];

            ChangeFloor(0);
        }

        void OnUpClick() {
            ChangeFloor(1);
        }

        void OnDownClick() {
            ChangeFloor(-1);
        }

        void ChangeFloor(int increment) {
            Floor += increment;

            upButton.enabled = Floor <= maxFloor - 1;

            downButton.enabled = Floor >= minFloor + 1;

            Debug.Log("FloorID: " + Floor);

            StartCoroutine(LoadAsynchronously(floorSceneNames[Floor]));
        }

        IEnumerator LoadAsynchronously(string sceneName) {

            if (currentSceneName != null && currentSceneName.Any()) {
                SceneManager.UnloadSceneAsync(currentSceneName);
            }

            currentSceneName = sceneName;
            var operation = SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
            operation.allowSceneActivation = true;
            loadingPanel.SetEnabled(true);

            while (!operation.isDone) {
                var progress = Mathf.Clamp01(operation.progress / 0.9f);

                loadingPanel.SetText("Loading: " + progress * 100 + "%");
                loadingPanel.SetProgress(progress);

                loadingPanel.EnableAnimation(true);

                yield return null;
            }

            loadingPanel.SetText("Loading: ");
            loadingPanel.SetProgress(0);
            loadingPanel.EnableAnimation(false);
            loadingPanel.SetEnabled(false);
        }
    }
}