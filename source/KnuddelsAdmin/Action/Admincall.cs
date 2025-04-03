using KnuddelsAdmin.Knuddels;
using KnuddelsAdmin.Protocol.Send;

namespace KnuddelsAdmin.Action;

[PluginAction("com.github.bizarrus.knuddelsadmin.admincall")]
class Admincall : IAction {
    public void Execute(Client client, Applet knuddels) {
        Logger.Log("ADMINCALL!!!!");

        knuddels.sendCommand("/info Flirt");

        client.send(new Event { EventName = "showOk", Context = client.createID() });
    }
}
