using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.UI {
    /// <inheritdoc />
    /// <summary>
    ///     A SwitcherPanel or tabbed panel is a panel with multiple tabs or views.
    ///     <para>
    ///         In order to use this component the gamobject needs to have a <see cref="CanvasRenderer" /> and
    ///         <see cref="RectTransform" /> Component.
    ///     </para>
    ///     <list type="bullet">
    ///         <listheader>
    ///             <description>description</description>
    ///         </listheader>
    ///         <item>
    ///             <see cref="ButtonSprite" /> of type <see cref="Sprite" /> is used as a shape for the tab buttons.
    ///         </item>
    ///         <item>
    ///             <see cref="ButtonFont" /> of type <see cref="Font" /> is used as the displayed font of the buttons label.
    ///         </item>
    ///         <item>
    ///             <see cref="SelectedColor" /> of type <see cref="Color" /> is used as the button background color for a
    ///             selected tab.
    ///         </item>
    ///         <item>
    ///             <see cref="SelectedFontColor" /> of type <see cref="Color" /> is used as the button font color for a
    ///             selected tab.
    ///         </item>
    ///         <item>
    ///             <see cref="DeselectedColor" /> of type <see cref="Color" /> is used as the button background color for a
    ///             deselected tab.
    ///         </item>
    ///         <item>
    ///             <see cref="DeselectedFontColor" /> of type <see cref="Color" /> is used as the button font color for a
    ///             deselected tab.
    ///         </item>
    ///         <item>
    ///             <see cref="TabList" /> of type <see cref="List{T}" /> contains all tabs of type <see cref="Tab" /> that get
    ///             displayed.
    ///         </item>
    ///     </list>
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    [RequireComponent(typeof(RectTransform))]
    public sealed class SwitcherPanel : MonoBehaviour {
        const int _buttonHeight = 30;

        readonly List<GameObject> _listOfButtons = new List<GameObject>();
        GameObject _buttonContainer;
        GameObject _panelContainer;

        GameObject _parentContainer;

        float _startXPosition;

        /// <summary>
        ///     Property is of type <see cref="Font" /> and is used as the displayed font of the buttons label.
        /// </summary>
        public Font ButtonFont;

        /// <summary>
        ///     Property is of type <see cref="Sprite" /> and is used as a shape for the tab buttons.
        /// </summary>
        public Sprite ButtonSprite;

        /// <summary>
        ///     Property is of type <see cref="Color" /> and is used as the button background color for a deselected tab.
        /// </summary>
        public Color DeselectedColor = Color.white;

        /// <summary>
        ///     Property is of type <see cref="Color" /> and is used as the button font color for a deselected tab.
        /// </summary>
        public Color DeselectedFontColor = Color.black;

        /// <summary>
        ///     Property is of type <see cref="Color" /> and is used as the button background color for a selected tab.
        /// </summary>
        public Color SelectedColor = Color.red;

        /// <summary>
        ///     Property is of type <see cref="Color" /> and is used as the button font color for a selected tab.
        /// </summary>
        public Color SelectedFontColor = Color.white;

        /// <summary>
        ///     Property is of type <see cref="List{T}" /> and contains all tabs of type <see cref="Tab" /> that get displayed.
        /// </summary>
        public List<Tab> TabList = new List<Tab>(2);

        /// <summary>
        ///     Method generates all the necesary containers buttons and builds the layout.
        /// </summary>
        void Start() {
            GenerateParentContainer();
            GenerateButtons();
            BuildMask();
            BuildPanelsPanel();
            AddPanels();

            _startXPosition = _panelContainer.GetComponent<RectTransform>().localPosition.x;

            SetSelectedTab(0);
        }

        /// <summary>
        ///     Method generates the parent container for the two sub containers.
        ///     <see cref="_buttonContainer" />
        ///     <see cref="_panelContainer" />
        /// </summary>
        void GenerateParentContainer() {
            _parentContainer = new GameObject("ParentContainer");
            _parentContainer.transform.parent = transform;

            var rectTransform = _parentContainer.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            _parentContainer.AddComponent<CanvasRenderer>();

            var image = _parentContainer.AddComponent<Image>();
            //image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            image.type = Image.Type.Sliced;
            image.color = Color.white;
        }

        /// <summary>
        ///     Method generates the container in which the buttons live.
        /// </summary>
        void GenerateButtonContainer() {
            _buttonContainer = new GameObject("ButtonContainer");
            _buttonContainer.transform.parent = _parentContainer.transform;
            _buttonContainer.AddComponent<CanvasRenderer>();

            _buttonContainer.transform.localPosition = Vector3.zero;

            var rectTransform = _buttonContainer.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.up;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.sizeDelta = new Vector2Int(0, _buttonHeight);

            _buttonContainer.AddComponent<HorizontalLayoutGroup>();
        }

        /// <summary>
        ///     Generates the buttons for the switcher.
        /// </summary>
        void GenerateButtons() {
            GenerateButtonContainer();

            for (var i = 0; i < TabList.Count; i++) {
                var panelName = TabList[i].Label;
                var buttonIndex = i;
                var buttonObject = new GameObject($"Button_{panelName}");
                buttonObject.transform.parent = _buttonContainer.transform;

                SetUpRectTransform(buttonObject);
                SetUpBackground(buttonObject);
                SetUpButton(buttonObject, buttonIndex);
                GenerateTextForButton(buttonObject, panelName);

                _listOfButtons.Add(buttonObject);
            }
        }

        /// <summary>
        ///     Sets up the rect transform for the passed in button GameObject.
        /// </summary>
        /// <param name="buttonObject">Button GameObject on which the rect transform is added.</param>
        static void SetUpRectTransform(GameObject buttonObject) {
            var rectTransform = buttonObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0.5f);
            rectTransform.anchorMax = new Vector2(1, 0.5f);
            rectTransform.pivot = new Vector2(0, 0.5f);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.sizeDelta = new Vector2Int(0, _buttonHeight);
        }

        /// <summary>
        ///     Sets up the background of the button.
        /// </summary>
        /// <param name="buttonObject">Button GameObject on which the background is added,</param>
        void SetUpBackground(GameObject buttonObject) {
            var image = buttonObject.AddComponent<Image>();
            image.sprite = ButtonSprite;
            image.type = Image.Type.Sliced;
        }

        /// <summary>
        ///     Method sets up the button at a specified index.
        /// </summary>
        /// <param name="buttonObject">The gameobject on which the button is created.</param>
        /// <param name="index">The index of the button.</param>
        /// <see cref="SetSelectedTab" />
        /// <see cref="GenerateButtons" />
        void SetUpButton(GameObject buttonObject, int index) {
            var button = buttonObject.AddComponent<ToggleButton>();
            button.SelectedColor = SelectedColor;
            button.SelectedFontColor = SelectedFontColor;
            button.DeselectedColor = DeselectedColor;
            button.DeselectedFontColor = DeselectedFontColor;

            button.onClick.AddListener(() => {
                SetSelectedTab(index);
                TabList[index].onChange.Invoke();
            });
        }

        /// <summary>
        ///     Method generates the text for the button with the specified label.
        /// </summary>
        /// <param name="buttonObject">The gamobject on which the child text object is applied.</param>
        /// <param name="label">The text that is displayed.</param>
        void GenerateTextForButton(GameObject buttonObject, string label) {
            var buttonText = new GameObject("Text");
            buttonText.transform.parent = buttonObject.transform;

            var textTransform = buttonText.AddComponent<RectTransform>();
            textTransform.anchorMin = Vector2.zero;
            textTransform.anchorMax = Vector2.one;
            textTransform.offsetMin = Vector2.zero;
            textTransform.offsetMax = Vector2.zero;

            var text = buttonText.AddComponent<Text>();
            text.text = label;
            text.font = ButtonFont;
            text.color = Color.black;
            text.alignment = TextAnchor.MiddleCenter;
        }

        /// <summary>
        ///     Sets the selected tab for a specified index.
        /// </summary>
        /// <param name="buttonIndex">The index of the button that needs to get selected.</param>
        void SetSelectedTab(int buttonIndex) {
            for (var i = 0; i < _listOfButtons.Count; i++) {
                _listOfButtons[i].GetComponent<ToggleButton>().Selected = buttonIndex == i;
            }

            var panelContainerTransform = _panelContainer.GetComponent<RectTransform>();

            var x = _startXPosition - buttonIndex * _startXPosition * 2;
            panelContainerTransform.localPosition = new Vector3(x, panelContainerTransform.localPosition.y,
                panelContainerTransform.localPosition.z);
        }

        void BuildMask() {
            _parentContainer.AddComponent<Mask>();
        }

        /// <summary>
        ///     This method builds the container for all the panels.
        /// </summary>
        void BuildPanelsPanel() {
            var width = GetComponent<RectTransform>().rect.width * TabList.Count;

            _panelContainer = new GameObject("PanelContainer");
            _panelContainer.transform.parent = _parentContainer.transform;

            var rectTransform = _panelContainer.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.up;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = new Vector2(width, -_buttonHeight);

            _panelContainer.AddComponent<CanvasRenderer>();
            _panelContainer.AddComponent<HorizontalLayoutGroup>();
        }

        /// <summary>
        ///     Adds the panels from the tabs to the panel container.
        /// </summary>
        void AddPanels() {
            foreach (var tab in TabList) {
                if (tab.Panel) {
                    var panel = tab.Panel;
                    panel.transform.SetParent(_panelContainer.transform);

                    var rectTransform = panel.GetComponent<RectTransform>();
                    rectTransform.anchorMin = Vector2.zero;
                    rectTransform.anchorMax = Vector2.one;
                    rectTransform.offsetMin = Vector2.zero;
                    rectTransform.offsetMax = Vector2.zero;
                }
                else {
                    Debug.LogError($"No panel for tab: {tab.Label}.");
                }
            }
        }
    }
}