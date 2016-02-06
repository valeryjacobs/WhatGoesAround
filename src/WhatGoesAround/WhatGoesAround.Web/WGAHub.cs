using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WhatGoesAround.Common;

namespace WhatGoesAround.Web
{
    public class WGAHub : Hub
    {
        Dictionary<string, string> DeviceRegister = new Dictionary<string, string>();
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            Clients.All.Send(name, message);
        }

        public void BroadCast(Message message)
        {
            Clients.All.BroadCast(message);
        }

        public void PushButtonCombination(PushButtonCombinationMessage message)
        {
            // TODO
            Clients.All.BroadCast(message);
        }

        public void SelectPlayer(Common.Action action)
        {
            Clients.All.SelectPlayer(action);
        }

        public void Register(string deviceId)
        {
            if (!DeviceRegister.ContainsKey(deviceId))
                DeviceRegister.Add(deviceId, Context.ConnectionId);
            else
                DeviceRegister[deviceId] = Context.ConnectionId;
        }

        public void RegisterPlayer(string deviceId, string playerName)
        {
            // TODO
        }
    }
}