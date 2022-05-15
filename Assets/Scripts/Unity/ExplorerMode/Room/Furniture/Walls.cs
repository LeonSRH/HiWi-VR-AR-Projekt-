using UnityEngine;

namespace SmartHospital.ExplorerMode.Rooms.Furniture {
    internal class Walls {
        public Walls(Collider top, Collider bottom, Collider left, Collider right, Collider front, Collider back) {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
            Front = front;
            Back = back;
        }

        public Collider Top { get; }

        public Collider Bottom { get; }

        public Collider Left { get; }

        public Collider Right { get; }

        public Collider Front { get; }

        public Collider Back { get; }
    }
}