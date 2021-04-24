




using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch.swisstxt.mh3.externaltask.extension
{
    class JsonDateConverter : JsonConverter<DateTime>
    {
        // 2021-03-25T01:43:14.534+0000
        public override System.DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
       => DateTime.ParseExact(reader.GetString(),
                    //   2021 - 03 - 25 T 01 : 43 : 14 . 534+0000
                    "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'SSSZ", CultureInfo.InvariantCulture);


        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
       => writer.WriteStringValue(value.ToString(
                    "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'SSSZ", CultureInfo.InvariantCulture));
    }
}




