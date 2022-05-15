using System;
using SmartHospital.Extensions;
using UnityEngine;

namespace SmartHospital.ExplorerMode.CameraControl {
    public partial class ExplorerCamera {
        [Header("2D Settings")] [SerializeField]
        float scrollSensitivity = 1f;

#pragma warning disable CS0414 // Dem Feld "ExplorerCamera.scrollDampening" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        [SerializeField] float scrollDampening = 6f;
#pragma warning restore CS0414 // Dem Feld "ExplorerCamera.scrollDampening" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        [SerializeField] float velocityAmplifier = 3f; 
#pragma warning disable CS0414 // Dem Feld "ExplorerCamera.cameraDistance" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        float cameraDistance = 20f;
#pragma warning restore CS0414 // Dem Feld "ExplorerCamera.cameraDistance" wurde ein Wert zugewiesen, der aber nie verwendet wird.
#pragma warning disable CS0108 // "ExplorerCamera.camera" blendet den vererbten Member "Component.camera" aus. Verwenden Sie das new-Schlüsselwort, wenn das Ausblenden vorgesehen war.
        Camera camera;
#pragma warning restore CS0108 // "ExplorerCamera.camera" blendet den vererbten Member "Component.camera" aus. Verwenden Sie das new-Schlüsselwort, wenn das Ausblenden vorgesehen war.

        void Awake() {
            camera = GetComponent<Camera>();
        }

        partial void MoveIn2DSpace() {
            var movingSave = moving;
            var deltaPosition = Vector3.zero;

            if (moving) {
                currentSpeed += increaseSpeed * Time.deltaTime;
            }

            moving = false;

            CheckMove(forwardButton, ref deltaPosition, transform.up);
            CheckMove(backwardButton, ref deltaPosition, -transform.up);
            CheckMove(rightButton, ref deltaPosition, transform.right);
            CheckMove(leftButton, ref deltaPosition, -transform.right);

            var scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (Math.Abs(scrollWheel) > 0.001f) {
                moving = true;
                var scrollAmount = scrollWheel * scrollSensitivity;

                /*
                scrollAmount *= (cameraDistance * 0.3f);

                cameraDistance += scrollAmount * -1f;
                */
                deltaPosition.y -= scrollAmount;
            }

            /*
            if (_XForm_Camera.localPosition.z != this.cameraDistance * -1f) {
                _XForm_Camera.localPosition = new Vector3(0f, 0f,
                    Mathf.Lerp(this._XForm_Camera.localPosition.z, this.cameraDistance * -1f,
                        Time.deltaTime * ScrollDampening));
            }
            */
            
            if (moving) {
                if (moving != movingSave) {
                    currentSpeed = initialSpeed;
                }

                transform.position =
                    (transform.position + deltaPosition * currentSpeed * velocityAmplifier * Time.deltaTime).Clamp(corner000, corner111);
            }
            else {
                currentSpeed = 0f;
            }
        }
    }
}