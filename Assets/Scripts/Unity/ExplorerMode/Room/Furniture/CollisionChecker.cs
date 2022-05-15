using SmartHospital.Common;
using UnityEngine;

namespace SmartHospital.ExplorerMode.Rooms.Furniture {
    [RequireComponent(typeof(Collider))]
    public class CollisionChecker : BaseController {
        public delegate void DetectEnter(Collider other);

        public delegate void DetectExit(Collider other);

        public bool Collision { get; set; }

        public event DetectEnter OnEnter;
        public event DetectExit OnExit;

        void OnTriggerEnter(Collider other) {
            Collision = true;

            OnEnter?.Invoke(other);
        }

        void OnTriggerExit(Collider other) {
            Collision = false;

            OnExit?.Invoke(other);
        }
    }
}