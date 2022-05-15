using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using SmartHospital.Common;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.ExplorerMode.Database.Handler;
using SmartHospital.ExplorerMode.Rooms.Locator;
using SmartHospital.ExplorerMode.Services;
using SmartHospital.ExplorerMode.Services.JSON;
using SmartHospital.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmartHospital.ExplorerMode.Rooms.TagSystem
{
    /// <summary>
    ///     Class that connects the <see cref="TagSystemView" /> and the <see cref="RoomPainter" />.
    /// </summary>
    [RequireComponent(typeof(TagSystemView))]
    public sealed class TagSystemController : BaseController
    {
        public const float UpdateCycleTime = 10f;

        bool active;
        float passedTime;

        //TODO In Array umwandeln
        List<ClientRoom> rooms;
        List<Tag> tagList;

        RoomPainter roomPainter;
        TagSystemView view;
        RESTHandler restHandler;

        Action<List<Tag>> updateTags;

        public List<Tag> Tags => tagList.ToList();

        public Tag CurrentTag { get; private set; }

        /// <summary>
        ///     Initialization of the sectionlist and the delegates of the uiManager.
        /// </summary>
        void Awake()
        {
            tagList = new List<Tag>();

            roomPainter = GetComponent<RoomPainter>();
            restHandler = GetComponent<RESTHandler>();

            if (tagList.Count > 0)
            {
                CurrentTag = tagList[0];
            }

            SetUpEventHandling();

            updateTags = list =>
            {
                tagList = list;

                tagList.ForEach(tagf => print($"{tagf.Name}, {tagf.ID}, {tagf.Color}"));

                if (!tagList.Any())
                {
                    CurrentTag = null;
                }
                else if (!CurrentTag)
                {
                    CurrentTag = tagList[0];
                }
                else
                {
                    try
                    {
                        var newCurrentTag = tagList.Find(listTag => listTag.ID == CurrentTag.ID);
                        CurrentTag = newCurrentTag;
                    }
                    catch (ArgumentNullException)
                    {
                        CurrentTag = null;
                    }
                }

                GetComponent<TagSystemView>().UpdateViews();
            };
        }

        void Start()
        {
            //TODO
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (!scene.name.ToLower().Contains("floor")) return;

                rooms = FindObjectsOfType<ClientRoom>().ToList();
            };
        }

        void Update()
        {
            passedTime += Time.deltaTime;

            if (passedTime >= UpdateCycleTime)
            {
                restHandler.StartRequest(restHandler.GetTagsRequest(updateTags));
                restHandler.StartRequest(restHandler.GetRoomsRequest(list =>
                {
                    if (!list.Any())
                    {
                        Debug.Log("No Rooms on server");
                    }

                    MergeRooms(list);
                    if (CurrentTag != null)
                    {
                        PaintCurrentTag();
                    }
                }));
                passedTime = 0f;
            }
        }

        void OnEnable()
        {
            var roomLocators = transform.parent.GetComponentsInChildren<RoomLocator>();

            for (var i = 0; i < roomLocators.Length; i++)
            {
                roomLocators[i].OnMarkingChange += OnMarkingChange;
                roomLocators[i].OnSelectionChange += OnSelectionChange;
                roomLocators[i].OnDeselection += OnDeselection;
            }
        }

        void OnDisable()
        {
            var roomLocators = transform.parent.GetComponentsInChildren<RoomLocator>();

            for (var i = 0; i < roomLocators.Length; i++)
            {
                roomLocators[i].OnMarkingChange -= OnMarkingChange;
                roomLocators[i].OnSelectionChange -= OnSelectionChange;
                roomLocators[i].OnDeselection -= OnDeselection;
            }
        }

        /// <summary>
        ///     Sets up the uiManager.
        /// </summary>
        void SetUpEventHandling()
        {
            view = GetComponent<TagSystemView>();
            view.OnActivate += Activate;
            view.OnDeactivate += Deactivate;
            view.OnColorChange += ChangeColor;
            view.OnEndColorChange += EndChangeColor;
            view.OnSelectionChange += ChangeSection;
            view.OnTagAdd += AddTag;
            view.OnTagRemove += RemoveTag;
            view.OnFilter += Filter;
            view.OnModeChange += ChangeMode;
        }

        void AddTag(Tag tagToAdd)
        {
            StartCoroutine(restHandler.CreateTagRequest(tagToAdd));
            CurrentTag = tagToAdd;
            tagList.Add(tagToAdd);
            view.UpdateViews();
        }

        void RemoveTag(Tag tagToRemove)
        {
            tagList.Remove(tagToRemove);

            for (var i = 0; i < rooms.Count; i++)
            {
                if (!rooms[i].ContainsTags(tagToRemove))
                {
                    continue;
                }

                rooms[i].RemoveTag(tagToRemove);

                restHandler.StartRequest(restHandler.UpdateRoomRequest(rooms[i].MyRoom));
            }

            CurrentTag = tagList.Count > 0 ? tagList[0] : null;
            GetComponent<TagSystemView>().UpdateViews();
        }

        void OnMarkingChange(Collider newRoom)
        {
            if (!active)
            {
            }

            //print($"Hovering over {newRoom.name}");
        }

        void OnSelectionChange(Collider newRoom, RoomSelectionMode selectionMode)
        {
            if (!active || !CurrentTag)
            {
                return;
            }

            var room = newRoom.GetComponent<ClientRoom>();

            if (room == null)
            {
                Debug.LogError("Invalid Room. Collider has no room assigned.");
                return;
            }

            if (room.ContainsTags(CurrentTag))
            {
                room.RemoveTag(CurrentTag);
                PaintCurrentTag();
            }
            else
            {
                room.AddTag(CurrentTag);
                roomPainter.PaintRoom(room, CurrentTag.Color);
            }

            print($"TagSystemController: {RoomJsonService.SerializeRoom(room.MyRoom)}");
            restHandler.StartRequest(restHandler.UpdateRoomRequest(room.MyRoom));
        }

        void OnDeselection()
        {
            if (!active)
            {
                return;
            }

            print("Deselect");
        }

        void Filter(List<Tag> tags)
        {
            if (tags.Count < 1)
            {
                roomPainter.PaintRooms(rooms);
            }
            else
            {
                roomPainter.PaintRooms(rooms.FindAll(room => room.ContainsTags(tags.ToArray())),
                    tags.ConvertAll(roomTag => roomTag.Color).ToArray());
            }
        }

        void ChangeMode(Mode mode)
        {
            switch (mode)
            {
                case Mode.Coloring:
                    if (CurrentTag)
                    {
                        roomPainter.PaintRooms(rooms.FindAll(room => room.ContainsTags(CurrentTag)),
                            CurrentTag.Color);
                    }

                    break;
                case Mode.Filtering:
                    Filter(new List<Tag>());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        void MergeRooms(List<ServerRoom> serverRoomList)
        {
            for (var i = 0; i < rooms.Count; i++)
            {
                var currentRoom = rooms[i];
                var serverRoom = serverRoomList.Find(room => room.RoomName == currentRoom.RoomName);

                if (serverRoom)
                {
                    //currentRoom.Tags = serverRoom.Tags;

                    var newTags = tagList.FindAll(tagListTag =>
                        serverRoom.Tags.Find(serverRoomTag => tagListTag.ID == serverRoomTag.ID));

                    currentRoom.ReplaceTags(newTags);
                }
            }
        }

        /// <summary>
        ///     Method gets called when the color mode gets activated.
        /// </summary>
        void Activate()
        {
            var colliderHandler = transform.parent.GetComponentInChildren<ColliderHandler>();
            colliderHandler.enabled = false;
            active = true;

            restHandler.StartRequest(restHandler.GetTagsRequest(updateTags));
            restHandler.StartRequest(restHandler.GetRoomsRequest(MergeRooms));
        }

        /// <summary>
        ///     Method gets called when the color mode gets deactivated.
        /// </summary>
        void Deactivate()
        {
            var colliderHandler = transform.parent.GetComponentInChildren<ColliderHandler>();
            colliderHandler.enabled = true;
            active = false;
        }

        /// <summary>
        ///     Method gets called when a new Section is selected from the dropdown menu.
        /// </summary>
        /// <param name="tagIndex">The index of the selected section that gets passed by the dropdown menu.</param>
        void ChangeSection(int tagIndex)
        {
            if (!active)
            {
                return;
            }

            CurrentTag = tagList[tagIndex];
            PaintCurrentTag();
        }

        /// <summary>
        ///     Method gets called when a new color is selected in the color picker,
        /// </summary>
        /// <param name="newColor">New color of type <see cref="Color" /> that was selected from the color picker.</param>
        void ChangeColor(Color newColor)
        {
            if (!active || CurrentTag == null)
            {
                return;
            }

            CurrentTag.Color = newColor;

            PaintCurrentTag();
        }

        void EndChangeColor(Color finalColor)
        {
            ChangeColor(finalColor);
            restHandler.StartRequest(restHandler.UpdateTagRequest(CurrentTag));
        }

        void PaintCurrentTag()
        {
            if (rooms != null && rooms.Any())
            {
                roomPainter.PaintRooms(rooms.FindAll(room => room.ContainsTags(CurrentTag)), CurrentTag.Color);
            }
        }
    }

    /// <summary>
    ///     Static internal class holds utility methods for strings.
    /// </summary>
    internal static class StringUtility
    {
        static readonly Regex hexStringRegex = new Regex(@"^[\dA-F]{6}$");

        internal static Dictionary<string, Color> ParseString(string colorsString)
        {
            if (colorsString.EndsWith(";", StringComparison.Ordinal))
            {
                colorsString = colorsString.Remove(colorsString.Length - 1);
            }

            return colorsString.Split(';').Select(s => s.Split(','))
                .ToDictionary(arr => arr[0], arr => StringToColor(arr[1]));
        }

        public static string ColorToString(Color color)
        {
            var colorString = string.Empty;
            colorString += Mathf.RoundToInt(color.r * 255).ToString("X2");
            colorString += Mathf.RoundToInt(color.g * 255).ToString("X2");
            colorString += Mathf.RoundToInt(color.b * 255).ToString("X2");

            if (!hexStringRegex.IsMatch(colorString))
            {
                throw new ArgumentException($"Malformed Hex-String {colorString} in ColorToString");
            }

            return colorString;
        }

        /// <summary>
        ///     Public static method converts a string of a hex number to the color the hex number represents.
        /// </summary>
        /// <returns>The six digit hex number of type string that represents a color.</returns>
        /// <param name="colorString">The color of type color that is converted from the input string.</param>
        public static Color StringToColor(string colorString)
        {
            if (!hexStringRegex.IsMatch(colorString))
            {
                throw new ArgumentException($"Malformed Hex-String: {colorString} in StringToColor");
            }

            var r = int.Parse(colorString.Substring(0, 2), NumberStyles.HexNumber) / 255f;
            var g = int.Parse(colorString.Substring(2, 2), NumberStyles.HexNumber) / 255f;
            var b = int.Parse(colorString.Substring(4, 2), NumberStyles.HexNumber) / 255f;

            return new Color(r, g, b);
        }
    }
}