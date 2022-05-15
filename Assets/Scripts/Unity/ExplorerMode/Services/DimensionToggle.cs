using ExplorerMode.UI.View;
using SmartHospital.Common;
using SmartHospital.ExplorerMode.CameraControl;
using SmartHospital.ExplorerMode.Rooms.Locator;
using SmartHospital.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SmartHospital.ExplorerMode.Services
{
    public class DimensionToggle : BaseController
    {
        public delegate void UpdateMode(bool mode3D);

        public DimensionToggleView view;

        public CameraManager cameraControl;
        public GameObject compass;
        FocusControl focusControl;
        RoomLocator2D roomLocator2D;

        RoomLocator3D roomLocator3D;
        public SlidePanel secondCameraPanel;
        UpdateMode update;

        public bool Is3D { get; private set; }

        void Start()
        {
            view.OnButton2DClick += ToggleFalse;
            view.OnButton3DClick += ToggleTrue;

            roomLocator3D = transform.parent.GetComponentInChildren<RoomLocator3D>();
            roomLocator2D = transform.parent.GetComponentInChildren<RoomLocator2D>();
            focusControl = transform.parent.GetComponentInChildren<FocusControl>();
            ToggleFalse(null);
        }

        public void AddUpdateListener(UpdateMode method)
        {
            update += method;
        }

        void ToggleTrue(PointerEventData eventData)
        {
            Toggle3D(true);
        }

        void ToggleFalse(PointerEventData eventData)
        {
            Toggle3D(false);
        }

        public void Toggle3D(bool toggle)
        {
            Is3D = toggle;

            update?.Invoke(toggle);

            cameraControl.MainCamera.Toggle3D(toggle);

            cameraControl.SecondCamera.gameObject.SetActive(toggle);
            secondCameraPanel.Toggle(toggle);
            roomLocator2D.IsActive = !toggle;
            roomLocator3D.IsActive = toggle;
            compass.SetActive(toggle);

            var sceneLight = GameObject.Find("Light").GetComponent<Light>();

            if (toggle)
            {
                sceneLight.intensity = Values.LightIntensity3D;
                sceneLight.transform.eulerAngles = Values.LightRotation3D;
                sceneLight.shadows = LightShadows.None;

                focusControl.FocusCanvas(true, "3D-Mode");
            }
            else
            {
                sceneLight.intensity = Values.LightIntensity2D;
                sceneLight.transform.eulerAngles = Values.LightRotation2D;
                sceneLight.shadows = LightShadows.Soft;

                focusControl.FocusCanvas(false, "2D-Mode");
            }
        }
    }
}