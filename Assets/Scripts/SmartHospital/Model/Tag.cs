using UnityEngine;
using Newtonsoft.Json;

namespace SmartHospital.ExplorerMode.Rooms.TagSystem
{
    /// <summary>
    ///     Class defines a tag with a specified color, name and an id.
    /// </summary>
    [JsonObject]
    public sealed class Tag
    {

        public Tag() { }

        /// <summary>
        ///     The constructor of the tag.
        /// </summary>
        /// <param name="color">The color of the tag.</param>
        /// <param name="name">The name of the tag.</param>
        /// <param name="id">The id of the tag.</param>
        public Tag(long id, Color color, string name)
        {
            Color = color;
            Name = name;
            ID = id;
        }

        [JsonProperty(PropertyName = "id")]
        public long ID { get; set; }

        /// <summary>
        ///     Defines the color for this tag.
        /// </summary>
        /// <returns>The color of this tag.</returns>
        [JsonProperty(PropertyName = "color")]
        [JsonConverter(typeof(ColorSerializer))]
        public Color Color { get; set; }

        /// <summary>
        ///     Defines the name of this tag.
        /// </summary>
        /// <returns>The name of this tag</returns>
        [JsonProperty(PropertyName = "name")]
        public string Name { set; get; }

        public static implicit operator bool(Tag exists) => exists != null;

        public bool Equals(Tag other) => Color.Equals(other.Color) &&
                                         Name.Equals(other.Name);
    }
}