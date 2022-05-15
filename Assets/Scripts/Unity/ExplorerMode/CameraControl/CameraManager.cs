using System.Collections.Generic;
using SmartHospital.Common;
using SmartHospital.ExplorerMode.Services;
using UnityEngine;

namespace SmartHospital.ExplorerMode.CameraControl {

    public class CameraManager : BaseController {
        List<CameraController> cameraList;
        public GameObject crossHair;

        Vector3 crossHairPosition;

        FloorControl floorControl;

        ColliderHandler materialAnimator;

        public ExplorerCamera MainCamera { get; set; }

        public TopDownCamera SecondCamera { get; set; }

        void Awake() {
            floorControl = GameObject.Find("Controller").GetComponentInChildren<FloorControl>();
            materialAnimator = GameObject.Find("Controller").GetComponentInChildren<ColliderHandler>();

            MainCamera = GetComponentInChildren<ExplorerCamera>();
            SecondCamera = GetComponentInChildren<TopDownCamera>();

            SecondCamera.AttachTo(MainCamera.transform);

            cameraList = new List<CameraController> {
                MainCamera,
                SecondCamera
            };

            foreach (var cc in cameraList) {
                cc.Init(this);
            }
        }

        void Update() {
            foreach (var cc in cameraList) {
                cc.Tick();
            }

            if (!crossHair) {
                return;
            }

            crossHairPosition = MainCamera.transform.position;

            crossHairPosition.y = 10;

            crossHair.transform.position = crossHairPosition;
        }

        public Vector3 GetCameraPositionOnFloor() => Values.CameraPositions[floorControl.Floor];

        public Vector3 GetCameraRotation() => Values.CameraNorthRotation;

        public bool HasSelectedCollider() => materialAnimator.HasSelectedCollider;

        public Collider GetSelectedCollider() => materialAnimator.SelectedCollider;
    }

}