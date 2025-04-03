using KnuddelsAdmin.Knuddels;

namespace KnuddelsAdmin;
interface IAction {
    void Execute(Client client, Applet knuddels);
}