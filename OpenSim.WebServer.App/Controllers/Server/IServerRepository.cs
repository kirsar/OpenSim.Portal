namespace OpenSim.WebServer.App.Controllers.Server
{
    public interface IServerRepository
    {
        void Add(Server server);
        ServerCollection GetAll();
        Server Get(long id);
        Server Remove(long id);
        void Update(Server server);
    }
}
