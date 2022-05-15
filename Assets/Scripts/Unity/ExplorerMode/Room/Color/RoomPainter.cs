using SmartHospital.Common;
using SmartHospital.Model;
using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital.ExplorerMode.Rooms.TagSystem
{
    public sealed class RoomPainter : BaseController
    {
        Dictionary<Renderer, Gradient> gradientDictionary;

        Dictionary<Renderer, Color> solidDictionary;
        public float Alpha = 0.8f;
        public Material MarkupMaterial;
        public float SlowDownFactor = 3f;

        void Start()
        {
            solidDictionary = new Dictionary<Renderer, Color>();
            gradientDictionary = new Dictionary<Renderer, Gradient>();
        }

        void FixedUpdate()
        {
            var enumerator = gradientDictionary.GetEnumerator();

            var time = Mathf.Abs(Mathf.Sin(Time.time / SlowDownFactor));

            while (enumerator.MoveNext())
            {
                var valuePair = enumerator.Current;
                valuePair.Key.material.color = valuePair.Value.Evaluate(time);
            }

            enumerator.Dispose();
        }

        public void PaintRooms(List<ClientRoom> roomList)
        {
            ClearAll();

            for (var i = 0; i < roomList.Count; i++)
            {
                var currentRoom = roomList[i];

                if (currentRoom.Tags.Count == 1)
                {
                    PaintRoom(currentRoom, currentRoom.Tags[0].Color);
                }
                else if (currentRoom.Tags.Count > 1)
                {
                    PaintRoom(currentRoom, currentRoom.Tags.ConvertAll(roomTag => roomTag.Color).ToArray());
                }
            }
        }

        public void PaintRooms(List<ClientRoom> roomList, Color[] colors)
        {
            ClearAll();

            var g = GenerateGradient(colors);

            for (var i = 0; i < roomList.Count; i++)
            {
                PaintRoom(roomList[i], g);
            }
        }

        public void PaintRooms(List<ClientRoom> roomList, Color color)
        {
            ClearSolids();

            for (var i = 0; i < roomList.Count; i++)
            {
                PaintRoom(roomList[i], color);
            }
        }

        public void PaintRoom(ClientRoom room, Color[] colors)
        {
            PaintRoom(room, GenerateGradient(colors));
        }

        void PaintRoom(ClientRoom room, Gradient g)
        {
            room.Collider.GetComponent<Renderer>().enabled = true;
            room.Collider.GetComponent<Renderer>().material = MarkupMaterial;
            gradientDictionary.Add(room.Collider.GetComponent<Renderer>(), g);
        }

        public void PaintRoom(ClientRoom room, Color color)
        {
            solidDictionary.Add(room.Collider.GetComponent<Renderer>(), color);
            UpdateSolids(color);
        }

        Gradient GenerateGradient(IReadOnlyList<Color> colors)
        {
            var g = new Gradient();
            var amount = colors.Count;

            var colorKeys = new GradientColorKey[amount];
            var alphaKeys = new GradientAlphaKey[amount];
            var divider = amount - 1f;

            for (var i = 0; i < amount; i++)
            {
                colorKeys[i].color = colors[i];
                colorKeys[i].time = i / divider;
                alphaKeys[i].alpha = Alpha;
                alphaKeys[i].time = i / divider;
            }

            g.colorKeys = colorKeys;
            g.alphaKeys = alphaKeys;
            g.mode = GradientMode.Blend;

            return g;
        }

        void ClearAll()
        {
            ClearSolids();
            ClearGradients();
        }

        void ClearSolids()
        {
            var enumerator = solidDictionary.GetEnumerator();

            while (enumerator.MoveNext())
            {
                enumerator.Current.Key.material.color = Color.clear;
                enumerator.Current.Key.enabled = false;
            }

            enumerator.Dispose();
            solidDictionary.Clear();
        }

        void ClearGradients()
        {
            var enumerator = gradientDictionary.GetEnumerator();

            while (enumerator.MoveNext())
            {
                enumerator.Current.Key.material.color = Color.clear;
                enumerator.Current.Key.enabled = false;
            }

            enumerator.Dispose();
            gradientDictionary.Clear();
        }

        void UpdateSolids(Color color)
        {
            var enumerator = solidDictionary.GetEnumerator();
            color.a = Alpha;
            while (enumerator.MoveNext())
            {
                enumerator.Current.Key.material = MarkupMaterial;
                enumerator.Current.Key.material.color = color;
                enumerator.Current.Key.enabled = true;
            }

            enumerator.Dispose();
        }
    }
}