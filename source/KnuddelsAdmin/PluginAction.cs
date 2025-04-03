namespace KnuddelsAdmin;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PluginActionAttribute : Attribute {
    public string ActionString { get; }

    public PluginActionAttribute(string name) {
        ActionString = name;
    }
}