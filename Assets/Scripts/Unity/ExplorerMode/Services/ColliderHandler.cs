using System.Runtime.InteropServices;
using SmartHospital.Common;
using SmartHospital.ExplorerMode.Rooms.Locator;
using UnityEngine;

namespace SmartHospital.ExplorerMode.Services {
    public class ColliderHandler : BaseController {
        DimensionToggle _dt;
        FloorControl _fc;
        MaterialAnimator _ma;

        public float Frequency;
        public Material MarkupMaterial;
        public Color SelectedColor = Color.green;

        public Collider CurrentCollider { get; set; }

        public Collider SelectedCollider { get; private set; }

        public bool HasCurrentCollider => CurrentCollider;

        public bool HasSelectedCollider => SelectedCollider;
        
        void Awake() {
            _ma = new MaterialAnimator(Frequency);
            _fc = transform.parent.GetComponentInChildren<FloorControl>();
            _dt = transform.parent.GetComponentInChildren<DimensionToggle>();
        }

        void Update() {
            if (HasCurrentCollider) {
                _ma.Animate(CurrentCollider, Color.clear);
            }

            if (HasSelectedCollider) {
                _ma.Animate(SelectedCollider, SelectedColor);
            }
        }

        void OnEnable() {
            var roomLocators = transform.parent.GetComponentsInChildren<RoomLocator>();

            for (var i = 0; i < roomLocators.Length; i++) {
                roomLocators[i].OnMarkingChange += SetCurrentCollider;

                roomLocators[i].OnSelectionChange += SelectCurrentCollider;

                roomLocators[i].OnDeselection += Deselect;
            }
        }

        void OnDisable() {
            var roomLocators = transform.parent.GetComponentsInChildren<RoomLocator>();

            for (var i = 0; i < roomLocators.Length; i++) {
                roomLocators[i].OnMarkingChange -= SetCurrentCollider;

                roomLocators[i].OnSelectionChange -= SelectCurrentCollider;

                roomLocators[i].OnDeselection -= Deselect;
            }

            SetCurrentCollider(null);
            SelectCurrentCollider(null, RoomSelectionMode.Single);
        }

        public void SetCurrentCollider(Collider newCollider) {
            if (newCollider) {
                if (SelectedCollider == newCollider || CurrentCollider == newCollider) {
                    return;
                }

                if (HasCurrentCollider) {
                    CurrentCollider.GetComponent<Renderer>().enabled = false;
                }

                CurrentCollider = newCollider;
                CurrentCollider.GetComponent<Renderer>().enabled = true;
                CurrentCollider.GetComponent<Renderer>().material = MarkupMaterial;
            }
            else if (HasCurrentCollider) {
                CurrentCollider.GetComponent<Renderer>().enabled = false;
                CurrentCollider = null;
            }
        }

        public void SelectCurrentCollider(Collider room, RoomSelectionMode selectionMode) {
//            if (GameObject.Find("Room_Detailed_Info") == null)
//            {
            if (CurrentCollider == SelectedCollider) {
                return;
            }

            if (!HasCurrentCollider) {
                return;
            }

            if (HasSelectedCollider) {
                SelectedCollider.GetComponent<Renderer>().enabled = false;
                SelectedCollider = null;
            }

            CurrentCollider.GetComponent<Renderer>().material = MarkupMaterial;
            SelectedCollider = CurrentCollider;

            /*if (Values.CurrentPlatform == Values.Platform.WebGL) {
                ShowRoomDetails(_fc.Floor, _currentCollider?.name);
            }*/

            CurrentCollider = null;

            //}
        }

        void Deselect() {
            //if (GameObject.Find("Room_Detailed_Info") == null) {
            CurrentCollider = null;
            SelectedCollider = null;

            /*if (Values.CurrentPlatform == Values.Platform.WebGL) {
                ShowRoomDetails(_fc.Floor, "-1");
            }*/
            //}
        }


        class MaterialAnimator {
            bool _increasing = true;

            internal MaterialAnimator(float frequency) {
                Frequency = frequency;
            }

            public float Frequency { get; }

            internal void Animate(Component collider, Color color) {
                var renderer = collider.GetComponent<Renderer>();
                var currentColor = renderer.material.color;

                if (color != Color.clear) {
                    var currentAlpha = currentColor.a;
                    currentColor = color;
                    currentColor.a = currentAlpha;
                }

                if (_increasing) {
                    currentColor.a = currentColor.a + Frequency;
                }
                else {
                    currentColor.a = currentColor.a - Frequency;
                }

                if (currentColor.a <= 0f) {
                    _increasing = true;
                }
                else if (currentColor.a > 1f) {
                    _increasing = false;
                }

                renderer.material.color = currentColor;
            }
        }
    }
}