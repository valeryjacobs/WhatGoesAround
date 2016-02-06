using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var hubConnection = new HubConnection("http://localhost:11615/");
            //http://whatgoesaroundweb.azurewebsites.net/

            var hubConnection = new HubConnection("http://whatgoesaroundweb.azurewebsites.net/");

            var chat = hubConnection.CreateHubProxy("WGAHub");
            chat.On<string, string>("Send", (name, message) =>
            {
                Console.Write(name + ": ");
                Console.WriteLine(message);
            });

            hubConnection.Start().Wait();

            //chat.Invoke("Notify", "Console app", hubConnection.ConnectionId);
            string msg = null;

            while ((msg = Console.ReadLine()) != null)
            {
                chat.Invoke("Send", "Console app", msg).Wait();
            }
        }
    }
}
