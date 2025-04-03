namespace KnuddelsAdmin;

[AttributeUsage(AttributeTargets.Field)]
class StringValue : Attribute {
    public String Value { get; private set; }

    public StringValue(String value) {
        Value = value;
    }
       
    public static T? GetEnum<T>(string? stringValue) where T : struct, Enum {
        var type = typeof(T);

        if(stringValue == null) {
            return null;
        }

        if(!type.IsEnum) {
            throw new ArgumentException("T must be an enumerated type");
        }

        foreach(var field in type.GetFields()) {
            if(Attribute.GetCustomAttribute(field, typeof(StringValue)) is StringValue attribute) {
                if(attribute.Value == stringValue) {
                    return (T)field.GetValue(null);
                }
            } else if(field.Name == stringValue) {
                return (T) field.GetValue(null);
            }
        }

        throw new ArgumentException($"'{stringValue}' is not a valid value for {type.Name}");
    }
}
