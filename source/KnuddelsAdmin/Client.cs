using System.Text;
using System.Net.WebSockets;
using Newtonsoft.Json;

namespace KnuddelsAdmin;

class Client {
    private Thread? _receiveThread;
    private readonly string _url;
    private readonly ClientWebSocket _webSocket;
    public event System.Action? OnConnect;
    public event System.Action? OnClose;
    public event Action<string>? OnError;
    public event Action<string>? OnReceive;

    public Client(string url) {
        _url = url;
        _webSocket = new ClientWebSocket();
        Logger.Log("Init Client");
    }

    public String createID() {
        return ((uint) new Random().Next()).ToString("X8");
    }

    public async void send(object message) {
        if (_webSocket == null || _webSocket.State != WebSocketState.Open) {
            Logger.Log("Send fehlgeschlagen: WebSocket nicht verbunden.");
            return;
        }

        try {
            string jsonMessage = JsonConvert.SerializeObject(message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);

            await _webSocket.SendAsync(new ArraySegment<byte>(messageBytes),
                                       WebSocketMessageType.Text,
                                       true,
                                       CancellationToken.None);

            Logger.Log("Sent: " + jsonMessage);
        } catch (Exception ex) {
            Logger.Log($"Fehler beim Senden: {ex.Message}");
        }
    }

    public void connect() {
        _receiveThread = new Thread(async () => {
            try {
                Logger.Log($"connect({_url})");
                await _webSocket.ConnectAsync(new Uri(_url), CancellationToken.None);
                OnConnect?.Invoke();

                var buffer = new byte[1024];

                while(_webSocket.State == WebSocketState.Open) {
                    WebSocketReceiveResult result   = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    string message                  = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    Logger.Log($"Received: {message}");
                    OnReceive?.Invoke(message);
                }

                Logger.Log("Close");
            } catch(Exception e) {
                Logger.Log($"Error: {e.Message}");
                OnError?.Invoke(e.Message);
            } finally {
                Logger.Log("WebSocket geschlossen.");
                OnClose?.Invoke();
            }
        });

        _receiveThread.IsBackground = true; // Damit der Thread beendet wird, wenn die App stoppt
        _receiveThread.Start();
    }
}
