using System;
using System.Collections.Generic;
using System.Linq;
using SmartHospital.ExplorerMode.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.ExplorerMode {
    public class AutoCompleteInput : MonoBehaviour {
        const int SuggestionLimit = 5;
#pragma warning disable CS0649 // Dem Feld "AutoCompleteInput.text" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
        [SerializeField] TMP_InputField text;
#pragma warning restore CS0649 // Dem Feld "AutoCompleteInput.text" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
        AutoComplete autoComplete;

        List<string> words;
        string lastSearchText;

        GameObject suggestionPanel;
        RectTransform suggestionTransform;

        TMP_Text[] texts;

        public List<string> Words {
            get { return words; }
            set { SetWords(value); }
        }

        void Awake() {
            autoComplete = new AutoComplete();
            suggestionPanel = new GameObject("Suggestions");
            words = new List<string>();
        }

        void Start() {
            suggestionPanel.transform.parent = transform;
            suggestionTransform = suggestionPanel.AddComponent<RectTransform>();
            SetupRectTransform(ref suggestionTransform, 0);

            SetupBackGround();
            SetupLayout();
            Words = new List<string>() {
                "First", "Second", "Third", "Fourth"
            };
        }

        void Update() {
            var searchText = text.text;

            if (searchText == lastSearchText) {
                return;
            }

            int numberOfResults;
            try {
                var suggestions = autoComplete.FindCompletions(searchText);
                numberOfResults = suggestions.Count;
                print($"NumberOfResults {numberOfResults}");
                int i;
                for (i = 0; i < texts.Length && i < numberOfResults; i++) {
                    texts[i].text = suggestions[i];
                    texts[i].enabled = true;
                }

                print(i);
                for (; i < texts.Length; i++) {
                    texts[i].enabled = false;
                }
            }
            catch (Exception e) {
                numberOfResults = 0;
                Debug.LogWarning(e);
                for (var i = 0; i < texts.Length; i++) {
                    texts[i].enabled = false;
                }
            }

            suggestionTransform.sizeDelta = new Vector2Int(220, 30 * numberOfResults);
            suggestionTransform.localPosition =
                new Vector3(0, -(numberOfResults * 15) - 15, suggestionTransform.localPosition.z);

            lastSearchText = searchText;
        }

        void SetupLayout() {
            var layout = suggestionPanel.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 10;
            layout.padding = new RectOffset(10, 10, 5, 5);

            texts = new TMP_Text[SuggestionLimit];

            for (var i = 0; i < texts.Length; i++) {
                var display = new GameObject($"Suggestion {i}");
                display.transform.parent = suggestionPanel.transform;
                texts[i] = display.AddComponent<TMP_Text>();
                texts[i].font = text.textComponent.font;
                texts[i].fontSize = text.textComponent.fontSize;
                texts[i].fontStyle = text.textComponent.fontStyle;
                texts[i].color = text.textComponent.color;
                texts[i].enabled = false;
            }
        }

        void SetupBackGround() {
            var image = suggestionPanel.AddComponent<Image>();
            image.color = GetComponent<Image>().color;
            image.sprite = GetComponent<Image>().sprite;
            image.type = Image.Type.Tiled;
        }

        static void SetupRectTransform(ref RectTransform rectTransform, int suggestionCount) {
            rectTransform.anchorMin = Vector2.up;
            rectTransform.anchorMax = Vector2.up;
            rectTransform.sizeDelta = new Vector2Int(220, suggestionCount * 30);
            rectTransform.localPosition = new Vector3(0, -90, rectTransform.localPosition.z);
        }

        public void SetWords(List<string> value) {
            words = value;
            autoComplete = new AutoComplete();

            for (var i = 0; i < words.Count; i++) {
                autoComplete.InsertWord(words[i]);
            }
        }

        public void AddWord(string word) {
            words.Add(word);
            autoComplete.InsertWord(word);
        }

        public void ClearWords() {
            SetWords(new List<string>());
        }
    }
}