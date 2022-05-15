using SmartHospital.Extensions;
using UnityEngine;

namespace SmartHospital.ExplorerMode.CameraControl {
    public partial class ExplorerCamera {
        
        [Header("3D Settings")]
        [Tooltip("Sensitivity of the cursor")]
        [SerializeField] float cursorSensitivity = 0.025f;
        [Tooltip("Limit of the vertical rotation (up/down looking) in degree")]
        [SerializeField] uint verticalRotationLimit = 180;
        
        Vector3 currentRotation;
        
        uint VerticalPositiveRotationLimit => verticalRotationLimit / 2;
        int VerticalNegativeRotationLimit => (int) -VerticalPositiveRotationLimit;
        
        partial void MoveIn3DSpace() {
            var movingSave = moving;
            var deltaPosition = Vector3.zero;

            if (moving) {
                currentSpeed += increaseSpeed * Time.deltaTime;
            }

            moving = false;

            CheckMove(forwardButton, ref deltaPosition, transform.forward);
            CheckMove(backwardButton, ref deltaPosition, -transform.forward);
            CheckMove(rightButton, ref deltaPosition, transform.right);
            CheckMove(leftButton, ref deltaPosition, -transform.right);
            CheckMove(upButton, ref deltaPosition, transform.up);
            CheckMove(downButton, ref deltaPosition, -transform.up);

            if (moving) {
                if (moving != movingSave) {
                    currentSpeed = initialSpeed;
                }

                transform.position =
                    (transform.position + deltaPosition * currentSpeed * Time.deltaTime).Clamp(corner000, corner111);
            }
            else {
                currentSpeed = 0f;
            }
        }

        partial void RotateIn3DSpace() {
            var xAngleDelta = -Input.GetAxis("Mouse Y") * 359f * cursorSensitivity;
            var yAngleDelta = Input.GetAxis("Mouse X") * 359f * cursorSensitivity;

            currentRotation = transform.eulerAngles;

            currentRotation.x = Mathf.Clamp(-Mathf.DeltaAngle(currentRotation.x + xAngleDelta, 360f), VerticalNegativeRotationLimit,
                VerticalPositiveRotationLimit);
            currentRotation.y += yAngleDelta;
            transform.eulerAngles = currentRotation;
        }
    }
}