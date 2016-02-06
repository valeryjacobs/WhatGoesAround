using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatGoesAround.Phone.Client
{
    public class HubClient
    {
        private static HubClient _current;

        private const string signalRHub = "http://whatgoesaroundcomesaround.azurewebsites.net/";
        private const string signalRHubProxy = "WGAHub";

        public static HubClient Current
        {
            get
            {
                if (_current == null)
                {
                    var hubConnection = new HubConnection(signalRHub);
                    var chat = hubConnection.CreateHubProxy(signalRHubProxy);
                    chat.On<Common.Action>("SelectPlayer", (message) =>
                    {
                    });

                    hubConnection.Start().Wait();
                    _connectionid = hubConnection.ConnectionId;
                    chat.Invoke<string>("Register", _piID);
                }
                return _current;
            }
        }
    }
}
