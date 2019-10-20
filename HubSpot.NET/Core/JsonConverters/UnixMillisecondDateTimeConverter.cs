using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HubSpot.NET.Core.JsonConverters
{
    /// <summary>
    /// Converts a <see cref="DateTime"/> to and from Unix epoch time
    /// </summary>
    public class UnixMillisecondDateTimeConverter : DateTimeConverterBase
    {
        internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long milliseconds;

            if (value is DateTime dateTime)
            {
                milliseconds = (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
            }
            else if (value is DateTimeOffset dateTimeOffset)
            {
                milliseconds = (long)(dateTimeOffset.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
            }
            else
            {
                throw new JsonSerializationException("Expected date object value.");
            }

            if (milliseconds < 0)
            {
                throw new JsonSerializationException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
            }

            writer.WriteValue(milliseconds);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing property value of the JSON that is being converted.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool nullable = !objectType.IsValueType ||
                (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>));
            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                {
                    throw new JsonSerializationException($"Cannot convert null value to {objectType}.");
                }

                return null;
            }

            long milliseconds;

            if (reader.TokenType == JsonToken.Integer)
            {
                milliseconds = (long)reader.Value;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                if (!long.TryParse((string)reader.Value, out milliseconds))
                {
                    throw new JsonSerializationException($"Cannot convert invalid value to {objectType}.");
                }
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token parsing date. Expected Integer or String, got {reader.TokenType}.");
            }

            if (milliseconds >= 0)
            {
                DateTime d = UnixEpoch.AddMilliseconds(milliseconds);

                Type t = (nullable)
                    ? Nullable.GetUnderlyingType(objectType)
                    : objectType;
                if (t == typeof(DateTimeOffset))
                {
                    return new DateTimeOffset(d, TimeSpan.Zero);
                }

                return d;
            }
            else
            {
                throw new JsonSerializationException($"Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {objectType}.");
            }
        }

    }

}