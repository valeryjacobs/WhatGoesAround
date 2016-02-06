using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatGoesAround.Phone.ViewModels;

namespace WhatGoesAround.Phone.Client
{
    public class HubClient
    {
        private const string signalRHub = "http://whatgoesaroundcomesaround.azurewebsites.net/";
        private const string signalRHubProxy = "WGAHub";

        private string deviceId;
        private string playerName;

        private IHubProxy chat;

        public HubClient(MainViewModel viewModel)
        {
            try
            {
                this.deviceId = viewModel.AppSettings.CurrentPlayerId;
                this.playerName = viewModel.AppSettings.CurrentPlayerName;

                var hubConnection = new HubConnection(signalRHub);
                chat = hubConnection.CreateHubProxy(signalRHubProxy);

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task SendButtonCombinationAsync(string deviceId, string playerName, IEnumerable<int> buttonCombination)
        {
            try {
                var message = new Common.PushButtonCombinationMessage();
                message.ButtonIds.AddRange(buttonCombination);
                message.DeviceId = deviceId;
                message.PlayerName = playerName;
                Debug.Write("sending button combination:");
                foreach (var i in message.ButtonIds)
                    Debug.Write(i + ",");
                Debug.WriteLine(String.Empty);
                await chat.Invoke("PushButtonCombination", buttonCombination);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
            }

        }
    }
}
