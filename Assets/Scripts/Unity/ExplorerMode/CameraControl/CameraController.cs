using SmartHospital.Common;
using UnityEngine;

namespace SmartHospital.ExplorerMode.CameraControl {
    [RequireComponent(typeof(Camera))]
    public abstract class CameraController : BaseController {
        protected bool allowMovement;

        protected bool allowRotation;
        protected CameraManager myManager;

        protected bool AllowRotation {
            get { return allowRotation; }
            set { allowRotation = value; }
        }

        protected bool AllowMovement {
            get { return allowMovement; }
            set { allowMovement = value; }
        }

        public bool AllowInteraction {
            get { return AllowMovement && AllowRotation; }
            set { AllowMovement = true;
                AllowRotation = true;
            }
        }

        public virtual float X => transform.position.x;

        public virtual float Y => transform.position.y;

        public virtual float Z => transform.position.z;

        protected abstract void Move();
        protected abstract void Rotate();

        public virtual void Init(CameraManager myManager) {
            this.myManager = myManager;
        }

        public virtual void Tick() {
            if (AllowMovement) {
                Move();
            }

            if (AllowRotation) {
                Rotate();
            }
        }
    }
}