using Microsoft.AspNet.SignalR.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatGoesAround.Common;

namespace ConsoleApplication1
{
    class Program
    {
         static void Main(string[] args)
        {

            var hubConnection = new HubConnection("http://whatgoesaroundcomesaround.azurewebsites.net/");
            //http://localhost:11615/
            //var hubConnection = new HubConnection("http://localhost:11615/");

            var chat = hubConnection.CreateHubProxy("WGAHub");
            chat.On<string, string>("Send", (name, message) =>
            {
                Console.WriteLine(message);
            });

            chat.On<Message>("BroadCast", (message) =>
              {
                  if (message is WhatGoesAround.Common.BeginGameMessage)
                  {
                      CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=whatgoesaround;AccountKey=C8SboSwUJtGB9fglsPZmRduVVGwuab/8zhYaLHQtqzgOTfDYL4kYTGPvHcADGl0VUeRDcwNXiMTZvVuT9BCsAA==");

                      // Create the table client.
                      CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                      // Create the CloudTable object that represents the "people" table.
                      CloudTable table = tableClient.GetTableReference("GameActivities");
                      table.CreateIfNotExists();

                      GameActivity activity = new GameActivity (Guid.NewGuid().ToString(),"Johny"){
                          PiName = "A",
                          ReflexTime = 100,
                          Round = 0,
                          TimeStamp = DateTime.Now,
                          ActivityType = 1
                      };

                      // Create the TableOperation object that inserts the customer entity.
                      TableOperation insertOperation = TableOperation.Insert(activity);

                      // Execute the insert operation.
                      table.Execute(insertOperation);
                  }
              });

            hubConnection.Start().Wait();

            string[] devices = new string[] { "A", "B", "C", "D" };
            string[] actions = new string[] { "red", "blue", "both", "off" };

            //chat.Invoke("Notify", "Console app", hubConnection.ConnectionId);

            Console.WriteLine("WhatGoesAround is starting. Press any key to start");
            Console.ReadLine();

            int rounds = 20;
            Random randDevice = new Random();
            for(int round=1; round < rounds; round++)
            {

    

                string DeviceId = devices[randDevice.Next(3)];
                string action = actions[randDevice.Next(3)];
                Console.WriteLine(string.Format("Round {0} for {1} to press {2} ", round,DeviceId,action));
                switch (action)
                {
                    case "x":
                        chat.Invoke("BroadCast", new WhatGoesAround.Common.EndRoundMessage(1));
                        break;
                    case "s":
                        chat.Invoke("BroadCast", new WhatGoesAround.Common.BeginRoundMessage(new Random().Next(0,10)));
                        break;
                    case "red":
                        chat.Invoke("SelectPlayer", new WhatGoesAround.Common.Action(){DeviceId = DeviceId.ToUpper(), Red = true, Blue = false });
                        break;
                    case "blue":
                        chat.Invoke("SelectPlayer", new WhatGoesAround.Common.Action() { DeviceId = DeviceId.ToUpper(), Red = false, Blue = true });
                        break;
                    case "both":
                        chat.Invoke("SelectPlayer", new WhatGoesAround.Common.Action() { DeviceId = DeviceId.ToUpper(), Red = true, Blue = true });
                        break;
                    case "off":
                        chat.Invoke("SelectPlayer", new WhatGoesAround.Common.Action() { DeviceId = DeviceId.ToUpper(), Red = false, Blue = false });
                        break;
                }
                System.Threading.Tasks.Task.Delay(3000).Wait();
                //chat.Invoke("Send", "Console app", msg).Wait();
            }
        }
    }

    public class GameActivity : TableEntity
    {
        public GameActivity(string gameId, string playerName)
        {
            this.PartitionKey = gameId;
            this.RowKey = playerName;
        }

        public string PlayerName { get; set; }
        public string PiName { get; set; }
        public int Round { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ActivityType { get; set; }
        public int ReflexTime { get; set; }
    }
}
