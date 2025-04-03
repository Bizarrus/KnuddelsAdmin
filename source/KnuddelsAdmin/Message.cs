using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KnuddelsAdmin;

class Message : JsonConverter {
    public override bool CanConvert(Type type) {
        return typeof(Message).IsAssignableFrom(type);
    }

    public override Protocol.Receive.Event? ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
        JObject json = JObject.Load(reader);

        if(json.ContainsKey("event")) {
            switch(StringValue.GetEnum<Protocol.Receive.Event.Type>((String?) json?["event"])){
                case Protocol.Receive.Event.Type.Key_UP:
                    return json?.ToObject<Protocol.Receive.Key>(serializer);
                case Protocol.Receive.Event.Type.DeviceConnected:
                    return json?.ToObject<Protocol.Receive.DeviceConnected>(serializer);
            }
        }

        return json?.ToObject<Protocol.Receive.Event>(serializer);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
        throw new NotImplementedException();
    }
}
