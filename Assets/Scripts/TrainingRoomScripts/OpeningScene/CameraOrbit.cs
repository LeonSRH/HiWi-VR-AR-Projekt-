using UnityEngine;

namespace SmartHospital.TrainingRoom
{
    public class CameraOrbit : MonoBehaviour
    {

        protected Transform _XForm_Camera;
        protected Transform _XForm_Parent;

        [Header("Main Camera")]
        public GameObject mainCamera;

        protected Vector3 _LocalRotation;
        public float _CameraDistance = 170f;
        public float _MaxCameraDistance = 500f;
        public float _MinCameraDistance = 10f;

        public float MouseSensitivity = 4f;
        public float ScrollSensitvity = 2f;
        public float OrbitDampening = 10f;
        public float ScrollDampening = 6f;

        public bool CameraDisabled = true;

        private Mode mode;

        // Use this for initialization
        void Start()
        {
            this._XForm_Camera = this.transform;
            this._XForm_Parent = this.transform.parent;


            _LocalRotation.y += 5;
            _LocalRotation.x += 5;

            setDeviceMode();
        }

        private void setDeviceMode()
        {
#if UNITY_IOS
     mode = Mode.IOS;
#endif

#if UNITY_STANDALONE_OSX
    mode = Mode.OSX;
#endif

#if UNITY_STANDALONE_WIN
            mode = Mode.WINDOWS;
#endif
        }
        enum Mode
        {
            WINDOWS, IOS, OSX
        }

        void LateUpdate()
        {
            if (mode == Mode.IOS)
            {
                MobileInputHandler();
            }
            else if (mode == Mode.WINDOWS)
            {
                WindowsInputHandler();
            }
        }

        void activateRotateMode()
        {
            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.transform.tag.Equals("Building"))
                {
                    CameraDisabled = false;
                }
            }
        }


        void WindowsInputHandler()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                activateRotateMode();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
                CameraDisabled = true;

            if (!CameraDisabled)
            {
                //Rotation of the Camera based on Mouse Coordinates
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                    _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                    //Clamp the y Rotation to horizon and not flipping over at the top
                    if (_LocalRotation.y < 0f)
                        _LocalRotation.y = 0f;
                    else if (_LocalRotation.y > 90f)
                        _LocalRotation.y = 90f;
                }

            }

            //Zooming Input from our Mouse Scroll Wheel
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

                ScrollAmount *= (this._CameraDistance * 0.3f);

                this._CameraDistance += ScrollAmount * -1f;

                this._CameraDistance = Mathf.Clamp(this._CameraDistance, _MinCameraDistance, _MaxCameraDistance);
            }


            //Actual Camera Rig Transformations
            Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
            this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

            if (this._XForm_Camera.localPosition.z != this._CameraDistance * -1f)
            {
                this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
            }
        }



        void MobileInputHandler()
        {
            switch (Input.touchCount)
            {
                case 0:
                    CameraDisabled = true;
                    break;
                case 1:
                    activateRotateMode();
                    break;
                case 2:
                    float ScrollAmount = Input.touches[0].deltaPosition.x * ScrollSensitvity;

                    ScrollAmount *= (this._CameraDistance * 0.3f);

                    this._CameraDistance += ScrollAmount * -1f;

                    this._CameraDistance = Mathf.Clamp(this._CameraDistance, _MinCameraDistance, _MaxCameraDistance);
                    break;

            }

            if (!CameraDisabled)
            {
                //Rotation of the Camera based on Mouse Coordinates
                if (Input.touches[0].deltaPosition.x != 0 || Input.touches[0].deltaPosition.y != 0)
                {
                    _LocalRotation.x += Input.touches[0].deltaPosition.x;
                    _LocalRotation.y -= Input.touches[0].deltaPosition.y;

                    //Clamp the y Rotation to horizon and not flipping over at the top
                    if (_LocalRotation.y < 0f)
                        _LocalRotation.y = 0f;
                    else if (_LocalRotation.y > 90f)
                        _LocalRotation.y = 90f;
                }

            }

            //Actual Camera Rig Transformations
            Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
            this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

            if (this._XForm_Camera.localPosition.z != this._CameraDistance * -1f)
            {
                this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
            }

        }
    }
}


