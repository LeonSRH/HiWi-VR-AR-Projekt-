using System;
using SmartHospital.Common;
using SmartHospital.ExplorerMode.Services;
using UnityEngine;

namespace SmartHospital.ExplorerMode.Rooms.Furniture {
    public class FurniturePlacer : BaseController {
        static readonly Vector3 screenMiddle = new Vector3(0.5f, 0.5f, 0.0f);

        GameObject furniture;
        Renderer furnitureRenderer;
        RaycastHit hit;
        bool inRoom;
        bool placeMode;
        CollisionChecker pseudoCollisionChecker;
        GameObject pseudoFurniture;
        Ray ray;
        Camera rayCamera;
        Collider room;
        Walls walls;

        void Start() {
            rayCamera = GetComponent<Camera>();

            transform.parent.GetComponentInChildren<DimensionToggle>()
                     .AddUpdateListener(OnDimensionUpdate);
        }

        void Update() {
            if (!placeMode) {
                return;
            }

            ray = rayCamera.ViewportPointToRay(screenMiddle);

            if (!Physics.Raycast(ray, out hit)) {
                return;
            }

            pseudoFurniture.transform.position = DeterminePlacementAlgorithm()(hit.point, furniture.transform);

            if (!pseudoCollisionChecker.Collision) {
                furniture.transform.position = pseudoFurniture.transform.position;
            }
        }

        void OnGUI() {
            if (!inRoom) {
                return;
            }

            var e = Event.current;

            if (e.type != EventType.KeyDown) {
                return;
            }

            if (e.keyCode != KeyCode.M || !e.alt) {
                return;
            }

            if (!placeMode) {
                var roomRenderer = room.GetComponent<Renderer>();

                //GetComponent<FPSCamera>().LimitMovementSpace(new Room(roomRenderer.bounds.min, roomRenderer.bounds.max));
                StartPlaceMode();
                placeMode = true;
            }
            else {
                //GetComponent<FPSCamera>().LimitMovementSpace(null);
                StopPlaceMode();
                placeMode = false;
            }

            e.Use();
        }

        void OnTriggerEnter(Collider other) {
            room = other;
            inRoom = true;
        }

        void OnTriggerExit(Collider other) {
            room = null;
            inRoom = false;
        }

        //TODO 
        void OnDimensionUpdate(bool mode3d) {
            print("Toggled");
        }

        CalculateFurniturePosition DeterminePlacementAlgorithm() {
            //TODO User gesetzte Moebel muessen noch abgedeckt werden
            print(hit.collider.name);
            switch (hit.collider.name) {
                case WallNames.Top:
                    return Vector3Operations.CalculateTop;
                case WallNames.Bottom:
                    return Vector3Operations.CalculateBottom;
                case WallNames.Left:
                    return Vector3Operations.CalculateLeft;
                case WallNames.Right:
                    return Vector3Operations.CalculateRight;
                case WallNames.Front:
                    return Vector3Operations.CalculateFront;
                case WallNames.Back:
                    return Vector3Operations.CalculateBack;
                default:
                    throw new InvalidProgramException("Illegal name in furniture collider");
            }
        }

        void StartPlaceMode() {
            GenerateWalls();

            room.GetComponent<Collider>().enabled = false;
            room.GetComponent<Renderer>().enabled = false;

            furniture = GameObject.CreatePrimitive(PrimitiveType.Cube);
            furniture.name = "Furniture";
            furniture.layer = LayerMask.NameToLayer("Ignore Raycast");
            furniture.transform.parent = room.transform;
            furnitureRenderer = furniture.GetComponent<Renderer>();
            furniture.transform.localScale = new Vector3(1f / 3f, 1f / 3f, 1f / 3f);
            pseudoFurniture = Instantiate(furniture);
            pseudoFurniture.name = "PseudoFurniture";
            pseudoFurniture.layer = LayerMask.NameToLayer("Ignore Raycast");
            pseudoFurniture.transform.parent = room.transform;
            pseudoCollisionChecker = pseudoFurniture.AddComponent<CollisionChecker>();
            Destroy(pseudoFurniture.GetComponent<Renderer>());
        }

        void StopPlaceMode() {
            DeleteWalls();

            room.GetComponent<Collider>().enabled = true;
            room.GetComponent<Renderer>().enabled = true;

            Destroy(furniture);
            Destroy(pseudoFurniture);
        }

        void GenerateWalls() {
            var roomBounds = room.GetComponent<Renderer>().bounds;

            var top = Instantiate(room, roomBounds.center + 2 * new Vector3(0, roomBounds.extents.y, 0),
                room.transform.rotation);
            var bottom = Instantiate(room, roomBounds.center - 2 * new Vector3(0, roomBounds.extents.y, 0),
                room.transform.rotation);
            var left = Instantiate(room, roomBounds.center - 2 * new Vector3(roomBounds.extents.x, 0, 0),
                room.transform.rotation);
            var right = Instantiate(room, roomBounds.center + 2 * new Vector3(roomBounds.extents.x, 0, 0),
                room.transform.rotation);
            var front = Instantiate(room, roomBounds.center + 2 * new Vector3(0, 0, roomBounds.extents.z),
                room.transform.rotation);
            var back = Instantiate(room, roomBounds.center - 2 * new Vector3(0, 0, roomBounds.extents.z),
                room.transform.rotation);


            top.name = WallNames.Top;
            top.transform.parent = room.transform;
            bottom.name = WallNames.Bottom;
            bottom.transform.parent = room.transform;
            left.name = WallNames.Left;
            left.transform.parent = room.transform;
            right.name = WallNames.Right;
            right.transform.parent = room.transform;
            front.name = WallNames.Front;
            front.transform.parent = room.transform;
            back.name = WallNames.Back;
            back.transform.parent = room.transform;

            walls = new Walls(top, bottom, left, right, front, back);
        }

        void DeleteWalls() {
            Destroy(walls.Top.gameObject);
            Destroy(walls.Bottom.gameObject);
            Destroy(walls.Left.gameObject);
            Destroy(walls.Right.gameObject);
            Destroy(walls.Front.gameObject);
            Destroy(walls.Back.gameObject);

            walls = null;
        }

        delegate Vector3 CalculateFurniturePosition(Vector3 hitLocation, Transform furniture);
    }

    internal struct WallNames {
        internal const string Top = "top";
        internal const string Bottom = "bottom";
        internal const string Left = "left";
        internal const string Right = "right";
        internal const string Front = "front";
        internal const string Back = "back";
    }

    internal static class Vector3Operations {
        const float padding = 0.01f;
        static Vector3 incrementVector = ResetToZero();
        static Vector3 returnVector;

        internal static Vector3 CalculateTop(Vector3 hitLocation, Transform furniture) {
            incrementVector.y = furniture.GetComponent<Renderer>().bounds.extents.y / 2 + padding;
            returnVector = hitLocation - incrementVector;
            incrementVector = ResetToZero();

            return returnVector;
        }

        internal static Vector3 CalculateBottom(Vector3 hitLocation, Transform furniture) {
            incrementVector.y = furniture.GetComponent<Renderer>().bounds.extents.y / 2 + padding;
            returnVector = hitLocation + incrementVector;
            incrementVector = ResetToZero();

            return returnVector;
        }

        internal static Vector3 CalculateLeft(Vector3 hitLocation, Transform furniture) {
            incrementVector.x = furniture.GetComponent<Renderer>().bounds.extents.x / 2 + padding;
            returnVector = hitLocation + incrementVector;
            incrementVector = ResetToZero();

            return returnVector;
        }

        internal static Vector3 CalculateRight(Vector3 hitLocation, Transform furniture) {
            incrementVector.x = furniture.GetComponent<Renderer>().bounds.extents.x / 2 + padding;
            returnVector = hitLocation - incrementVector;
            incrementVector = ResetToZero();

            return returnVector;
        }

        internal static Vector3 CalculateFront(Vector3 hitLocation, Transform furniture) {
            incrementVector.z = furniture.GetComponent<Renderer>().bounds.extents.z / 2 + padding;
            returnVector = hitLocation - incrementVector;
            incrementVector = ResetToZero();

            return returnVector;
        }

        internal static Vector3 CalculateBack(Vector3 hitLocation, Transform furniture) {
            incrementVector.z = furniture.GetComponent<Renderer>().bounds.extents.z / 2 + padding;
            returnVector = hitLocation + incrementVector;
            incrementVector = ResetToZero();

            return returnVector;
        }

        static Vector3 ResetToZero() => Vector3.zero;
    }
}