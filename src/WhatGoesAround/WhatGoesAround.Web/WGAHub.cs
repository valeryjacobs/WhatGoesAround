using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WhatGoesAround.Web
{
    public class WGAHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            Clients.All.Send(name, message);
        }

        //public void BroadCast(message)
        //{
        //    Clients.All.BroadCast(message);
        //}
    }
}