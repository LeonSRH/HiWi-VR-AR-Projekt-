using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SmartHospital.Common;
using SmartHospital.UI.ColorPicker;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.ExplorerMode.Rooms.TagSystem {
    /// <summary>
    ///     Class defines the color UI manager for the room coloring and tagging UI.
    ///     It requires a <see cref="TagSystemController" /> and displays the results .
    /// </summary>
    [RequireComponent(typeof(TagSystemController))]
    public sealed class TagSystemView : BaseController {
        public delegate void ActivateColorMode();

        public delegate void AddTag(Tag tag);

        public delegate void ChangeColorOfTag(Color newColor);

        public delegate void EndChangeColorOfTag(Color finalColor);

        public delegate void ChangeSelectedTag(int tagIndex);

        public delegate void DeactivateColorMode();

        public delegate void Filter(List<Tag> tags);

        public delegate void ModeChange(Mode mode);

        public delegate void RemoveTag(Tag tag);

        readonly List<Toggle> listOfToggles = new List<Toggle>();

        bool activated;

        TagSystemController tagSystemController;
        //public ColorPicker ColorPicker;
        public ColorDropdown ColorDropdown;
        public GameObject FilterPanel;
        //public Sprite PixelSprite;
        public Dropdown TagDropdown;
        public InputField TagInput;
        public Texture2D Texture;

        public Toggle TogglePrefab;

        public event ActivateColorMode OnActivate;
        public event DeactivateColorMode OnDeactivate;
        public event ChangeColorOfTag OnColorChange;
        public event EndChangeColorOfTag OnEndColorChange;
        public event ChangeSelectedTag OnSelectionChange;
        public event AddTag OnTagAdd;
        public event RemoveTag OnTagRemove;
        public event Filter OnFilter;
        public event ModeChange OnModeChange;

        /// <summary>
        ///     Method removes a tag from the taglist of the <see cref="tagSystemController" /> based on the selected tag of the
        ///     <see cref="TagDropdown" />.
        ///     The <see cref="TagDropdown" /> and the <see cref="FilterPanel" /> will get updated.
        /// </summary>
        [UsedImplicitly]
        public void RemoveSelectedTag() {
            OnTagRemove?.Invoke(tagSystemController.Tags[TagDropdown.value]);
        }

        /// <summary>
        ///     Method adds a new tag to the taglist of the <see cref="tagSystemController" /> based on the <see cref="InputField" />.
        ///     The <see cref="TagDropdown" /> and the <see cref="FilterPanel" /> will get updated.
        /// </summary>
        [UsedImplicitly]
        public void AddNewTag() {
            OnTagAdd?.Invoke(new Tag(-1, Color.white, TagInput.text));
            ColorDropdown.Color = Color.white;
        }

        /// <summary>
        ///     Method toggles the the Room Color between isActive and not isActive.
        ///     <see cref="OnActivate" />
        ///     <see cref="OnDeactivate" />
        /// </summary>
        [UsedImplicitly]
        public void ToggleActive() {
            if (activated) {
                OnDeactivate?.Invoke();
                activated = false;
            }
            else {
                OnActivate?.Invoke();
                activated = true;
            }
        }

        /// <summary>
        ///     Method selects the mode the color UI is in. It uses the int orthogonal from the <see cref="Mode" /> enumeration.
        /// </summary>
        /// <param name="intMode">The orthogonal of the mode that should get selected.</param>
        [UsedImplicitly]
        public void SelectMode(int intMode) {
            OnModeChange?.Invoke(Enum.IsDefined(typeof(Mode), intMode) ? (Mode) intMode : Mode.Coloring);
        }

        /// <summary>
        ///     Method initiializes this component.
        /// </summary>
        void Awake() {
            tagSystemController = GetComponent<TagSystemController>();

            GenerateContentPanel();
            RedrawTogglePanel();

            /*
            ColorPicker.OnColorChange += newColor => OnColorChange?.Invoke(newColor);
            ColorPicker.OnEndColorChange += finalColor => OnEndColorChange?.Invoke(finalColor);
            TagDropdown.onValueChanged.AddListener(index => {
                OnSelectionChange?.Invoke(index);
                ColorPicker.Color = tagSystemController.Tags[index].Color;
            });
            */

            ColorDropdown.OnColorChange += newColor => OnColorChange?.Invoke(newColor);
            ColorDropdown.OnEndColorChange += finalColor => OnEndColorChange?.Invoke(finalColor);
            TagDropdown.onValueChanged.AddListener(index => {
                OnSelectionChange?.Invoke(index);
                ColorDropdown.Color = tagSystemController.Tags[index].Color;
            });
        }

        /// <summary>
        ///     Method sets up the content panel.
        /// </summary>
        void GenerateContentPanel() {
            var layout = FilterPanel.AddComponent<VerticalLayoutGroup>();
            layout.childAlignment = TextAnchor.MiddleCenter;
        }

        /// <summary>
        ///     Method sets the size of the content panel based on the number of tags in the tag list of the
        ///     <see cref="tagSystemController" />.
        ///     The panel holds the toggles.
        /// </summary>
        void SetContentPanelSize() {
            var rectTransform = FilterPanel.GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = new Vector2(10, 0);
            rectTransform.offsetMax = new Vector2(rectTransform.rect.width - 10,
                30 * tagSystemController.Tags.Count);
        }

        /// <summary>
        ///     Method redraws the toggle panel based on the tag list of the <see cref="tagSystemController" />.
        /// </summary>
        void RedrawTogglePanel() {
            ClearToggleList();
            SetContentPanelSize();
            for (var i = 0; i < tagSystemController.Tags.Count; i++) {
                GenerateToggleAt(i);
            }
        }

        /// <summary>
        ///     Method clears the toggle list.
        /// </summary>
        void ClearToggleList() {
            listOfToggles.ForEach(DestroyToggle);
            listOfToggles.Clear();
        }

        /// <summary>
        ///     Static method destroys the toggle that is passed in.
        /// </summary>
        /// <param name="toggle">that should get destroyed.</param>
        static void DestroyToggle(Component toggle) {
            toggle.transform.SetParent(null);
            Destroy(toggle.gameObject);
        }

        /// <summary>
        ///     Method generates a toggle at a specific index in the tag list.
        /// </summary>
        /// <param name="index">The index on which the toggle for the tag is created.</param>
        void GenerateToggleAt(int index) {
            var toggle = Instantiate(TogglePrefab);
            toggle.transform.SetParent(FilterPanel.transform);
            toggle.name = "Toggle";
            toggle.isOn = false;

            var toggleTransform = toggle.GetComponent<RectTransform>();
            toggleTransform.anchorMin = Vector2.zero;
            toggleTransform.anchorMax = Vector2.one;
            toggleTransform.offsetMin = Vector2.zero;
            toggleTransform.offsetMax = Vector2.zero;

            var text = toggle.GetComponentInChildren<Text>();
            text.text = tagSystemController.Tags[index].Name;

            toggle.onValueChanged.AddListener(delegate { OnFilter?.Invoke(GenerateFilteredTagList()); });

            listOfToggles.Add(toggle);
        }

        /// <summary>
        ///     Method generates the filtered list of tags for the tags that are selected in the filter view.
        /// </summary>
        /// <returns>The filtered Tag list.</returns>
        List<Tag> GenerateFilteredTagList() {
            var listOfTags = new List<Tag>();
            for (var i = 0; i < listOfToggles.Count; i++) {
                if (listOfToggles[i].isOn) {
                    listOfTags.Add(tagSystemController.Tags[i]);
                }
            }

            return listOfTags;
        }

        /// <summary>
        ///     Method updates the tag dropdown with the current tag list from the <see cref="tagSystemController" />.
        /// </summary>
        void UpdateTagDropDown() {
            TagDropdown.ClearOptions();
            TagDropdown.AddOptions(tagSystemController.Tags.ConvertAll(listTag => {
                Texture.SetPixels(new[] {listTag.Color});
                var sprite = Sprite.Create(Texture, new Rect(0, 0, Texture.width, Texture.height),
                    new Vector2(0.5f, 0.5f), 100.0f);

                var optionData = new Dropdown.OptionData {
                    text = listTag.Name,
                    image = sprite
                };
                return optionData;
            }));
            TagDropdown.value = tagSystemController.Tags.IndexOf(tagSystemController.CurrentTag);
        }

        public void UpdateViews() {
            RedrawTogglePanel();
            UpdateTagDropDown();
        }
    }

    /// <summary>
    ///     The mode the UI is in.
    /// </summary>
    public enum Mode {
        Coloring = 0,
        Filtering = 1
    }
}