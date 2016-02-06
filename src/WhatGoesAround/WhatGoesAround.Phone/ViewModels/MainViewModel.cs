﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatGoesAround.Common;
using Windows.UI;
using Windows.UI.Core;

namespace WhatGoesAround.Phone.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private GameViewModel _game;

        private ButtonViewModel _button1;
        private ButtonViewModel _button2;

        private bool _buttonsVisible;

        private DateTime _lastClickTime;
        private CoreDispatcher _dispatcher;
        private string _displayMessage;
        private Timer _buttonPressTimer;

        private ObservableCollection<int> _currentButtonSequence { get; set; } 

        public GameViewModel Game { get { return _game; } set { _game = value; OnPropertyChanged("Game"); } }
        public ButtonViewModel Button1 { get { return _button1; } set { _button1 = value; OnPropertyChanged("Button1"); } }
        public ButtonViewModel Button2 { get { return _button2; } set { _button2 = value; OnPropertyChanged("Button2"); } }
        public ObservableCollection<int> CurrentButtonSequence { get { return _currentButtonSequence; } set { _currentButtonSequence = value; OnPropertyChanged("CurrentButtonSequence"); } }
        public bool ButtonsVisible { get { return _buttonsVisible; } set { _buttonsVisible = value; OnPropertyChanged("ButtonsVisible"); } }
        public string DisplayMessage { get { return _displayMessage; } set { _displayMessage = value; OnPropertyChanged("DisplayMessage"); } }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            this.Game = new GameViewModel();
            this.Button1 = new ButtonViewModel() { Id = 1, Color = Color.FromArgb(255, 0, 0, 0), Left = 100, Top = 100, Size = 250 };
            this.Button2 = new ButtonViewModel() { Id = 2, Color = Color.FromArgb(0, 0, 255, 0), Left = 450, Top = 250, Size = 180 };

            this.CurrentButtonSequence = new ObservableCollection<int>();
            this._dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
            this._buttonPressTimer = new Timer(OnButtonPressedTimerElapsed, null, 0, 500);
            this.ButtonsVisible = true;
        }

        public async void OnButtonPressedTimerElapsed(Object stateInfo)
        {
            if (this.CurrentButtonSequence.Count == 0)
                return;

            // TODO send button press sequence to server

            await _dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.CurrentButtonSequence.Clear();
                this._buttonPressTimer.Change(0, 500); // reset the timer
            });
        }


        #region Game Event handlers

        public void OnGameBeginning(BeginGameMessage message)
        {
            this.DisplayMessage = "Game begins!";
            this.ButtonsVisible = false;
        }

        public async void OnRoundBeginning(BeginRoundMessage message)
        {
            await _dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.DisplayMessage = "Round " + message.RoundNumber;
                this.Game.RoundNumber = message.RoundNumber;
                this.ButtonsVisible = false;
            });

        }

        public async void OnRoundEnding(EndRoundMessage message)
        {
            await _dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.ButtonsVisible = false;
            });
        }

        public async void OnPlayerScoresUpdating(UpdatePlayerScoresMessage message)
        {
            await _dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                foreach (var playerScore in message.PlayerScores)
                {
                    var score = this.Game.Players.Where(x => x.Name == playerScore.Key).FirstOrDefault();
                    if (score != null)
                        score.Score = playerScore.Value;
                    else
                        this.Game.Players.Add(new PlayerViewModel() { Name = playerScore.Key, Score = playerScore.Value });
                }
            });
        }

        public async void OnBeginPlaySequence(Message message)
        {

            await _dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Utils.Utils.GetRandomPositions(this, 600, 600);
                this.ButtonsVisible = true;
                this.CurrentButtonSequence.Clear();
            });

        }

        public async void OnEndPlaySequence(Message message)
        {
            await _dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.ButtonsVisible = false;
                this.CurrentButtonSequence.Clear();
            });
        }

        #endregion

        #region UI Event handlers

        public void HandleButtonClick(int buttonId)
        {
            CurrentButtonSequence.Add(buttonId);
            _lastClickTime = DateTime.Now;
            this._buttonPressTimer.Change(0, 500); // reset the timer
        }

        #endregion
    }
}
