using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatGoesAround.Phone.ViewModels;

namespace WhatGoesAround.Phone.Client
{
    public class HubClient
    {
        private static HubClient _current;

        private const string signalRHub = "http://whatgoesaroundcomesaround.azurewebsites.net/";
        private const string signalRHubProxy = "WGAHub";

        private const string deviceId = "A";
        private const string playerName = "Sorin";

        public HubClient(MainViewModel viewModel)
        {

            var hubConnection = new HubConnection(signalRHub);
            var chat = hubConnection.CreateHubProxy(signalRHubProxy);

            // event registrations
            chat.On<Common.BeginGameMessage>("BeginGame", (message) =>
            {
                viewModel.OnGameBeginning(message);  
            });

            chat.On<Common.BeginRoundMessage>("BeginRound", (message) =>
            {
                viewModel.OnRoundBeginning(message);
            });

            chat.On<Common.EndRoundMessage>("EndRound", (message) =>
            {
                viewModel.OnRoundEnding(message);
            });

            chat.On<Common.UpdatePlayerScoresMessage>("UpdatePlayerScores", (message) =>
            {
                viewModel.OnPlayerScoresUpdating(message);
            });

            chat.On<Common.BeginPlaySequenceMessage>("BeginPlaySequence", (message) =>
            {
                viewModel.OnBeginPlaySequence(message);
            });

            chat.On<Common.EndPlaySequenceMessage>("EndPlaySequence", (message) =>
            {
                viewModel.OnEndPlaySequence(message);
            });

            hubConnection.Start().Wait();
            chat.Invoke<string>("RegisterPlayer", deviceId, playerName);

        }
    }
}
