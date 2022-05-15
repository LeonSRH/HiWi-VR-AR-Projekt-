using UnityEngine;

namespace SmartHospital.ExplorerMode.CameraControl {
    public class TopDownCamera : CameraController {
        bool alreadyRotated;
        public Transform attacher;

        public int heightIncrement = 40;

        public void AttachTo(Transform attacher) {
            this.attacher = attacher;
        }

        public override void Tick() {
            if (AllowMovement) {
                Move();
            }
            else {
                transform.position = myManager.GetCameraPositionOnFloor();
            }

            if (AllowRotation) {
                Rotate();
                alreadyRotated = false;
            }
            else if (!alreadyRotated) {
                transform.eulerAngles = myManager.GetCameraRotation();
                alreadyRotated = true;
            }
        }

        protected override void Move() {
            if (!attacher) {
                return;
            }

            var secondCameraPosition = attacher.position;

            secondCameraPosition.y += heightIncrement;

            transform.position = secondCameraPosition;
        }

        protected override void Rotate() {
            if (!attacher) {
                return;
            }

            var secondCameraRotation = attacher.eulerAngles;

            secondCameraRotation.y = transform.eulerAngles.y;

            transform.eulerAngles = secondCameraRotation;
        }
    }
}