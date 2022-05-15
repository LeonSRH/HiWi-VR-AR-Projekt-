using SmartHospital.Common;
using UnityEngine;

namespace SmartHospital.ExplorerMode.Rooms.Locator {
    public abstract class RoomLocator : BaseController {
        public delegate void Deselect();

        public delegate void MarkRoom(Collider newRoom);

        public delegate void SelectRoom(Collider room, RoomSelectionMode selectionMode);

        protected bool isActive;

        public bool IsActive {
            get { return isActive; }
            set { SetActive(value); }
        }

        public event MarkRoom OnMarkingChange;
        public event SelectRoom OnSelectionChange;
        public event Deselect OnDeselection;

        protected void FireMarkingEvent(Collider newRoom) {
            OnMarkingChange?.Invoke(newRoom);
        }

        protected void FireSelectionEvent(Collider room, RoomSelectionMode selectionMode) {
            OnSelectionChange?.Invoke(room, selectionMode);
        }

        protected void FireDeselectionEvent() {
            OnDeselection?.Invoke();
        }

        public void SetActive(bool isActive) {
            this.isActive = isActive;

            if (!isActive) {
                OnMarkingChange?.Invoke(null);
            }
        }
    }

    public enum RoomSelectionMode {
        Single,
        Additive
    }
}