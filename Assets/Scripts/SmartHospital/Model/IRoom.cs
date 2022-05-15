using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SmartHospital.ExplorerMode.Rooms.TagSystem;
using UnityEngine;

namespace SmartHospital.Controller.ExplorerMode.Rooms
{
    public interface IRoom
    {
        string RoomName { get; }

        Collider Collider { get; }

        List<Tag> Tags { get; }

        /// <summary>
        ///     Method adds a new tag to this room.
        /// </summary>
        /// <param name="tagToAdd">that gets added.</param>
        void AddTag(Tag tagToAdd);

        /// <summary>
        ///     Method removes a tag from this room.
        /// </summary>
        /// <param name="tagToRemove">that gets removed.</param>
        void RemoveTag(Tag tagToRemove);

        /// <summary>
        /// Replaces all tags with the new tags.
        /// </summary>
        /// <param name="newTags">New tags that this room should have</param>
        void ReplaceTags(IEnumerable<Tag> newTags);

        /// <summary>
        ///     Checks if tags are contained in this room.
        /// </summary>
        /// <param name="tags">that get checked for.</param>
        /// <returns></returns>
        bool ContainsTags(params Tag[] tags);

        /// <summary>
        ///     Checks if only the passed tags are contained in this room.
        /// </summary>
        /// <param name="tags">that are the only ones that should be contained in this room.</param>
        /// <returns></returns>
        bool ContainsTagsExclusive(params Tag[] tags);

        /// <summary>
        /// Returns a string representation of this room.
        /// </summary>
        /// <returns>String representation.</returns>
        string ToString();
    }
}