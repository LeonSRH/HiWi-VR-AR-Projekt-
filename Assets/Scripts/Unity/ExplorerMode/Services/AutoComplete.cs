using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHospital.ExplorerMode.Services {

    public sealed class AutoComplete {
        readonly TrieNode rootNode = new TrieNode('$', null);

        public void InsertWord(string word) {
            rootNode.InsertWord(word.Trim().ToLower());
        }

        public List<string> FindCompletions(string prefix) {
            if (prefix == null) {
                throw new ArgumentNullException(nameof(prefix), "Parameter must not be null");
            }

            if (prefix.Length < 1) {
                throw new ArgumentException("Parameter must not be empty", nameof(prefix));
            }

            if (IsEmpty()) {
                throw new InvalidOperationException("Autocomplete is empty");
            }
            return rootNode.FindCompletions(prefix.Trim().ToLower())
                            .Select(s => $"{prefix.Trim().ToLower()}{s}")
                            .ToList();
        }

        public bool IsEmpty() => rootNode.IsLastNode;
    }

    internal sealed class TrieNode {
        readonly char value;
        readonly ISet<TrieNode> childNodes;

        internal bool IsLastNode => !childNodes.Any();

        internal TrieNode(char value, TrieNode parentNode) {
            this.value = value;
            childNodes = new HashSet<TrieNode>();
            parentNode?.childNodes.Add(this);
        }

        internal void InsertWord(string word) {
            if (word.Length != 0) {
                var trieNode = childNodes.Where(node => node.value == word[0])
                                          .Select(node => node)
                                          .FirstOrDefault() ?? new TrieNode(word[0], this);
                trieNode.InsertWord(word.Substring(1));
            }
        }

        internal ISet<string> FindCompletions(string prefix) {
            if (prefix.Length != 0) {
                var trieNode = childNodes.Where(node => node.value == prefix[0])
                                          .Select(node => node)
                                          .FirstOrDefault();
                return trieNode != null ? trieNode.FindCompletions(prefix.Substring(1)) : new HashSet<string>();
            }

            if (IsLastNode) {
                return new HashSet<string> {""};
            }

            var strings = new HashSet<string>();
            foreach (var childNode in childNodes) {
                childNode.FindCompletions(prefix).Select(s => $"{childNode.value}{s}").ToList()
                         .ForEach(s => strings.Add(s));
            }

            return strings;
        }

        bool Equals(TrieNode other) {
            return value == other.value;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var node = obj as TrieNode;
            return node != null && Equals(node);
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }
    }
}