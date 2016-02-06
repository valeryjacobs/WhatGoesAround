using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatGoesAround.Phone.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PlayerViewModel> _players;
        private int _currentPlayerId;
        private int _roundNumber;

        public ObservableCollection<PlayerViewModel> Players { get { return _players; } set { _players = value;  OnPropertyChanged("Players"); } }
        public int CurrentPlayerId { get { return _currentPlayerId; } set { _currentPlayerId = value; OnPropertyChanged("CurrentPlayerId"); } }
        public int RoundNumber { get { return _roundNumber; } set { _roundNumber = value; OnPropertyChanged("RoundNumber"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
