using UnityEngine;
using UnityEngine.EventSystems;

namespace SmartHospital.ExplorerMode.Rooms.Locator
{
    public sealed class RoomLocator2D : RoomLocator, IPointerClickHandler
    {

        RaycastHit hit;
        Ray ray;

        public Camera raycastCamera;
        Collider target;

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        void Update()
        {
            ray = raycastCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }

            if (!(hit.collider == target))
            {
                FireMarkingEvent(hit.collider);
            }

            if (Input.GetMouseButtonDown(0))
            {
                FireSelectionEvent(hit.collider,
                    Input.GetKey(KeyCode.LeftControl) ? RoomSelectionMode.Additive : RoomSelectionMode.Single);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                FireDeselectionEvent();
            }

            target = hit.collider;
        }
    }
}