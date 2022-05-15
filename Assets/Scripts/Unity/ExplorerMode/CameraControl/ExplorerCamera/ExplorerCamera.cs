using System.Collections;
using SmartHospital.Extensions;
using SmartHospital.Model;
using UnityEngine;

namespace SmartHospital.ExplorerMode.CameraControl
{
    public partial class ExplorerCamera : CameraController
    {
        [Header("Keys")] [SerializeField] KeyCode forwardButton = KeyCode.W;
        [SerializeField] KeyCode backwardButton = KeyCode.S;
        [SerializeField] KeyCode leftButton = KeyCode.A;
        [SerializeField] KeyCode rightButton = KeyCode.D;
        [SerializeField] KeyCode upButton = KeyCode.Space;
        [SerializeField] KeyCode downButton = KeyCode.LeftShift;

        [Header("Settings")] [SerializeField] float initialSpeed = 10f;
        [SerializeField] float increaseSpeed = 1.25f;
        [SerializeField] float lerpAmplifier = 1.1f;
        [SerializeField] float maxHeight = 150;
        [SerializeField] float minHeight;


        [Header("Other Properties")] [SerializeField]
        Renderer groundRenderer;

        [SerializeField] Vector3 corner000;
        [SerializeField] Vector3 corner111;

        bool dimension2D;
        bool moving;
        bool initialized;

        float currentSpeed;

        partial void MoveIn3DSpace();

        partial void RotateIn3DSpace();

        partial void MoveIn2DSpace();

        void Start()
        {
            LimitMovementSpace(null);
        }

        protected override void Move()
        {
            if (dimension2D)
            {
                MoveIn2DSpace();
            }
            else
            {
                MoveIn3DSpace();
            }
        }

        protected override void Rotate()
        {
            if (!dimension2D)
            {
                RotateIn3DSpace();
            }
        }

        public void Toggle3D(bool toggle)
        {
            dimension2D = !toggle;
            if (toggle)
            {
                if (!myManager.HasSelectedCollider())
                {
                    return;
                }

                var newPosition = myManager.GetSelectedCollider().GetComponent<Renderer>().bounds.center;

                StartCoroutine(LerpToRoom(GetObserverPosition(newPosition), newPosition, true));
            }
            else
            {
                StartCoroutine(LerpToDefault());
            }
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

        static Vector3 GetObserverPosition(Vector3 roomPosition)
        {
            roomPosition.y += 20;
            roomPosition.z += 20;

            return roomPosition;
        }

        public void InitLerpToRoom(Vector3 position)
        {
            if (initialized)
            {
                return;
            }

            initialized = true;
            StartCoroutine(LerpToRoom(GetObserverPosition(position), position, false));
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

        void Interpolate(out float interpolant)
        {
            interpolant = Time.deltaTime * lerpAmplifier;
        }

        IEnumerator LerpToDefault()
        {
            var newPosition = myManager.GetCameraPositionOnFloor();

            AllowInteraction = false;

            while (!transform.position.CheckInRange(newPosition, 0.1f) &&
                   !transform.eulerAngles.CheckInRange(myManager.GetCameraRotation(), 0.1f))
            {
                float interpolant;
                Interpolate(out interpolant);
                AllowInteraction = false;

                transform.position = Vector3.Lerp(transform.position, newPosition, interpolant);
                transform.eulerAngles = transform.eulerAngles.LerpRotation(myManager.GetCameraRotation(), interpolant);

                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator LerpToRoom(Vector3 newPosition, Vector3 lookAt, bool allowInteractionAfterwards)
        {
            AllowInteraction = false;

            while (!transform.position.CheckInRange(newPosition, 0.1f))
            {
                float interpolant;
                Interpolate(out interpolant);
                AllowInteraction = false;

                transform.position = Vector3.Lerp(transform.position, newPosition, interpolant);
                transform.LookAt(lookAt);

                yield return new WaitForEndOfFrame();
            }

            AllowInteraction = allowInteractionAfterwards;
        }
    }
}