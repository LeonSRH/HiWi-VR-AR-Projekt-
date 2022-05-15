using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.ExplorerMode.CameraControl;
using SmartHospital.Extensions;
using SmartHospital.Model;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace SmartHospital.Test_Debug
{
    public class TestCameraController : CameraController
    {
        public KeyCode backwardButton = KeyCode.S;

        public Vector3 corner000;
        public Vector3 corner111;

        Vector3 currentRotation;
        float currentSpeed;
        public float cursorSensitivity = 0.025f;
        public KeyCode downButton = KeyCode.LeftShift;

        public KeyCode forwardButton = KeyCode.W;
        public Renderer groundRenderer;
        public float increaseSpeed = 1.25f;

        public float initialSpeed = 10f;
        public KeyCode leftButton = KeyCode.A;
        public float maxHeight = 150;
        public float minHeight;
        bool moving;
        public KeyCode rightButton = KeyCode.D;
        public KeyCode upButton = KeyCode.Space;

        public uint xFOV = 180;
        float xFOVNegative;
        float xFOVPositive;

        void Start()
        {
            xFOVNegative = 0 - (float)(xFOV / 2);
            xFOVPositive = 0 + xFOV / 2;

            LimitMovementSpace(null);


            allowMovement = true;
            allowRotation = true;
        }

        void Update()
        {
            Tick();
        }

        protected override void Move()
        {
            var movingSave = moving;
            var deltaPosition = Vector3.zero;

            if (moving)
            {
                currentSpeed += increaseSpeed * Time.deltaTime;
            }

            moving = false;

            CheckMove(forwardButton, ref deltaPosition, transform.forward);
            CheckMove(backwardButton, ref deltaPosition, -transform.forward);
            CheckMove(rightButton, ref deltaPosition, transform.right);
            CheckMove(leftButton, ref deltaPosition, -transform.right);
            CheckMove(upButton, ref deltaPosition, transform.up);
            CheckMove(downButton, ref deltaPosition, -transform.up);

            if (moving)
            {
                if (moving != movingSave)
                {
                    currentSpeed = initialSpeed;
                }

                transform.position =
                    (transform.position + deltaPosition * currentSpeed * Time.deltaTime).Clamp(corner000, corner111);
            }
            else
            {
                currentSpeed = 0f;
            }
        }

        protected override void Rotate()
        {
            var xAngleDelta = -Input.GetAxis("Mouse Y") * 359f * cursorSensitivity;
            var yAngleDelta = Input.GetAxis("Mouse X") * 359f * cursorSensitivity;

            currentRotation = transform.eulerAngles;

            currentRotation.x = Mathf.Clamp(-Mathf.DeltaAngle(currentRotation.x + xAngleDelta, 360f), xFOVNegative,
                xFOVPositive);
            currentRotation.y += yAngleDelta;
            transform.eulerAngles = currentRotation;
        }

        void CheckMove(KeyCode keyCode, ref Vector3 deltaPosition, Vector3 directionVector)
        {
            if (!Input.GetKey(keyCode))
            {
                return;
            }

            moving = true;
            deltaPosition += directionVector;
        }

        public void LimitMovementSpace(ClientRoom room)
        {
            if (room != null)
            {
                //corner000 = room.Corner000;
                //corner111 = room.Corner111;
            }
            else
            {
                if (groundRenderer)
                {
                    //minHeight = groundRenderer.bounds.min.y;

                    corner000 = groundRenderer.bounds.min;
                    corner000.y = corner000.y + 1;
                    corner111 = groundRenderer.bounds.max;
                    corner111.y = maxHeight;
                }
                else
                {
                    corner000 = new Vector3(-100, minHeight, -100);
                    corner111 = new Vector3(100, maxHeight, 100);
                }
            }
        }
    }
}