using System.Reflection;
using System.Text;
using KnuddelsAdmin.Knuddels;
using Newtonsoft.Json;

namespace KnuddelsAdmin;

class Program {
    private Client client;
    private Applet knuddels;
    private Dictionary<string, Type> actions = new Dictionary<string, Type>();
    private Thread thread;
    private Dictionary<string, string> arguments = new Dictionary<string, string>();
    string? port, uuid, eventName, info;

    public Program(string[] args) {
        knuddels    = new Applet();

        /* Load Actions */
        var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IAction).IsAssignableFrom(t) && t.IsClass);

        foreach(var type in types) {
            var attribute = type.GetCustomAttribute<PluginActionAttribute>();
            Logger.Log($"Load Action: '{attribute}'");

            if (attribute != null) {
                actions.Add(attribute.ActionString, type);
            }
        }

        /* Parse Arguments */
        for (int i = 0; i < args.Length; i++) {
            if(args[i].StartsWith("-")) {
                string key = args[i].TrimStart('-');

                if(key == "info") {
                    StringBuilder jsonBuilder = new StringBuilder();

                    for(int j = i + 1; j < args.Length; j++) {
                        if (args[j].StartsWith("-")) break;
                        jsonBuilder.Append(args[j]).Append(" ");
                    }

                    this.arguments[key] = jsonBuilder.ToString().Trim();
                    i += jsonBuilder.Length > 0 ? jsonBuilder.ToString().Split(' ').Length : 0;
                } else if (i + 1 < args.Length && !args[i + 1].StartsWith("-")) {
                    this.arguments[key] = args[i + 1];
                    i++;
                } else {
                    this.arguments[key] = "true";
                }
            }
        }

        foreach(var key in new string[] { "info", "port", "pluginUUID", "registerEvent" }) {
            if(!this.arguments.ContainsKey(key)) {
                Logger.Log($"Error: Missing required argument '-{key}'");
                return;
            }
        }

        this.arguments.TryGetValue("port", out port);
        this.arguments.TryGetValue("pluginUUID", out uuid);
        this.arguments.TryGetValue("registerEvent", out eventName);
        this.arguments.TryGetValue("info", out info);

        client = new Client($"ws://localhost:{port}");
        thread = new Thread(Run);
        thread.Start();
    }

    public void send(object data) {
        client.send(data);
    }

    public void Run() {
        this.connect(port, uuid, eventName, info);
    }

    protected async void connect(string? port, string? uuid, string? eventName, string? info) {
        Logger.Log($"Connecting to Port {port}");


        client.OnConnect += () => {
            Logger.Log("Connected to WebSocket");
            
            client.send(new Protocol.Send.RegisterPlugin { EventName = eventName, UUID = uuid });
        };

        client.OnClose += () => Logger.Log("WebSocket connection closed");
        client.OnError += (error) => Logger.Log($"WebSocket Error: {error}");
        client.OnReceive += (json) => {
            Protocol.Receive.Event? evt = JsonConvert.DeserializeObject<Protocol.Receive.Event>(json);

            switch(evt?.EventName) {
                case "keyUp":
                    Protocol.Receive.Key? key = JsonConvert.DeserializeObject<Protocol.Receive.Key>(json);
                    this.HandleAction(key?.Action);
                break;
            }
        };

        client.connect();
    }
    public void HandleAction(string? name) {
        if(actions.TryGetValue(name, out Type? actionType)) {
            ((IAction?) Activator.CreateInstance(actionType))?.Execute(client, knuddels);
        } else {
            Console.WriteLine($"Action '{name}' nicht gefunden.");
        }
    }

    static async Task Main(string[] args) {
        Logger.Log("STARTING: " + string.Join(" ", args));
        new Program(args);

        await Task.Delay(Timeout.Infinite);
    }
}