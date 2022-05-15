using SmartHospital.Common;
using UnityEngine;

namespace SmartHospital.Controller.ExplorerMode.Rooms.Details
{
    public partial class RoomparametersView : BaseController
    {
        float _size;
        float _positionX;
        float _positionY;
        float _positionZ;
        float _rotationX;
        float _rotationY;
        float _rotationZ;
        float _height;
        float _width;
        float _length;

#pragma warning disable CS0649 // Dem Feld "RoomparametersView.roomCollider" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
        Collider roomCollider;
#pragma warning restore CS0649 // Dem Feld "RoomparametersView.roomCollider" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".

        public float Height
        {
            get
            {
                _height = roomCollider.bounds.size.y;
                return _height;
            }
        }

    }
}
