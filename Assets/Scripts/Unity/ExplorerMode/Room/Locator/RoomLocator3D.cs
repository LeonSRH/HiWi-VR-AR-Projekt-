using SmartHospital.ExplorerMode.Rooms.Furniture;
using SmartHospital.ExplorerMode.Services;
using UnityEngine;

namespace SmartHospital.ExplorerMode.Rooms.Locator {
    public sealed class RoomLocator3D : RoomLocator {
        public CollisionChecker cc;
#pragma warning disable CS0649 // Dem Feld "RoomLocator3D.currentCollider" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
        Collider currentCollider;
#pragma warning restore CS0649 // Dem Feld "RoomLocator3D.currentCollider" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
        FocusControl focusControl;
        bool sent;

        void Start() {
            focusControl = transform.parent.parent.GetComponentInChildren<FocusControl>();

            cc.OnEnter += OnEnter;
        }

        void Update() {
            if (!Input.GetMouseButtonDown(0) || sent || !isActive) {
                return;
            }

            FireSelectionEvent(currentCollider, RoomSelectionMode.Single);

            focusControl.FocusCanvas(false, "RoomLocator", true);

            sent = true;
        }

        void OnEnter(Collider other) {
            FireMarkingEvent(other);
            sent = false;
        }
    }
}