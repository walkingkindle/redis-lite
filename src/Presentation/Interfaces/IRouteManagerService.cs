using System.Net.Sockets;

namespace Presentation.Interfaces
{
   public interface IRouteManagerService
    {
        public Task HandleClient(TcpClient handler, NetworkStream stream);
    }
}