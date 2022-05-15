using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHospital.Common.Collections {
    /// <summary>
    /// Wraps multiple queues into one and assigns a priority level to each queue.
    /// </summary>
    /// <typeparam name="T">Type of elements this queue holds.</typeparam>
    public class PriorityQueue<T> {
        readonly Queue<T>[] queues;

        /// <summary>
        /// Constructs the queue.
        /// </summary>
        /// <param name="priorityLevels">Levels of priority this queue should provide.</param>
        public PriorityQueue(uint priorityLevels) {
            PriorityLevels = priorityLevels;
            queues = new Queue<T>[PriorityLevels];

            for (var i = 0; i < queues.Length; i++) {
                queues[i] = new Queue<T>();
            }
        }

        /// <summary>
        /// Count of elements in this queue.
        /// </summary>
        public uint Count => GetCount();

        /// <summary>
        /// Levels of priority this queue has.
        /// </summary>
        public uint PriorityLevels { get; }

        uint MaxIndex => PriorityLevels - 1;

        public bool Any() {
            return Count > 0;
        }
        
        /// <summary>
        /// Returns the count of elements in this queue from the specified level.
        /// </summary>
        /// <param name="startingPriorityLevel">Priority level from which to count the elements.</param>
        /// <returns>Count of elements.</returns>
        /// <exception cref="ArgumentException">If the starting level is higher than the amount of priority levels this queue has.</exception>
        public uint GetCount(uint startingPriorityLevel = 0) {
            if (startingPriorityLevel > MaxIndex) {
                throw new ArgumentException("Invalid priority level", nameof(startingPriorityLevel));
            }

            return (uint) queues.Skip((int) startingPriorityLevel).Select(queue => queue.Count).Sum();
        }


        public void Enqueue(T item, uint priorityLevel = 0) {
            if (priorityLevel > MaxIndex) {
                throw new ArgumentException("Invalid priority level", nameof(priorityLevel));
            }

            queues[priorityLevel].Enqueue(item);
        }

        public T Dequeue(uint startingPriorityLevel = 0) {
            if (startingPriorityLevel > MaxIndex) {
                throw new ArgumentException("Invalid priority level", nameof(startingPriorityLevel));
            }

            if (GetCount(startingPriorityLevel) == 0) {
                throw new InvalidOperationException("Queue is empty");
            }

            return queues.Skip((int) startingPriorityLevel).First(queue => queue.Any()).Dequeue();
        }

        public T Peek(uint startingPriorityLevel = 0) {
            if (startingPriorityLevel > MaxIndex) {
                throw new ArgumentException("Invalid priority level", nameof(startingPriorityLevel));
            }

            if (GetCount(startingPriorityLevel) == 0) {
                throw new InvalidOperationException("Queue is empty");
            }

            return queues.Skip((int) startingPriorityLevel).First(queue => queue.Any()).Peek();
        }

        public void Clear() {
            for (var i = 0; i < queues.Length; i++) {
                queues[i].Clear();
            }
        }

        /*
        public override bool Equals(object obj) {

            if (obj == null) {
                return false;
            }
            if (ReferenceEquals(obj, this)) {
                return true;
            }
            if (obj.GetType() == typeof(PriorityQueue<T>)) {
                var otherQueue = (PriorityQueue<T>) obj;

                if (otherQueue.PriorityLevels != PriorityLevels || otherQueue.Count != Count) {
                    return false;
                }
                
                return !queues.Where((t, i) => !t.SequenceEqual(otherQueue.queues[i])).Any();
            }

            return false;
        }
        */

        public override string ToString() {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('[');

            for (var i = 0; i < queues.Length; i++) {
                stringBuilder.Append('[');

                if (queues[i].Any()) {
                    var queue = queues[i];

                    for (var j = 0; j < queue.Count; j++) {
                        stringBuilder.Append(queue.ElementAt(j));

                        if (j < queue.Count - 1) {
                            stringBuilder.Append(',');
                        }
                    }
                }

                stringBuilder.Append(']');

                if (i < queues.Length - 1) {
                    stringBuilder.Append(';');
                }
            }

            stringBuilder.Append(']');

            return stringBuilder.ToString();
        }
    }
}