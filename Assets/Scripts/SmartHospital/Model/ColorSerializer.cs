using Newtonsoft.Json;
using System;
using UnityEngine;

namespace SmartHospital.ExplorerMode.Rooms.TagSystem {
    public class ColorSerializer : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var color = (Color) value;
            writer.WriteStartObject();
            writer.WritePropertyName("red");
            writer.WriteValue(color.r);
            writer.WritePropertyName("green");
            writer.WriteValue(color.g);
            writer.WritePropertyName("blue");
            writer.WriteValue(color.b);
            writer.WritePropertyName("alpha");
            writer.WriteValue(color.a);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) {
            float r, g, b, a;
            
            if (!ReadValue(reader, "red", out r)) {
                Debug.LogError("Red has no value");
            }
            if (!ReadValue(reader, "green", out g)) {
                Debug.LogError("Green has no value");
            }
            if (!ReadValue(reader, "blue", out b)) {
                Debug.LogError("Blue has no value");
            }
            if (!ReadValue(reader, "alpha", out a)) {
                Debug.LogError("alpha has no value");
            }
            
            reader.Read();
            return new Color(r, g, b, a);
        }

        static bool ReadValue(JsonReader reader, string key, out float value) {
            reader.Read();
            if (reader.Value as string == key) {
                reader.Read();
                var redValue = reader.Value as double?;
                if (redValue.HasValue) {
                    value = (float) redValue.Value;
                    return true;
                }
            }

            value = 0f;
            return false;
        }

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType) => objectType == typeof(Color);
    }
}