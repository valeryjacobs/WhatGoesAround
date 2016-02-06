using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatGoesAround.Phone.ViewModels
{
    public class AppSettingsViewModel : INotifyPropertyChanged
    {

        private string _currentPlayerId;
        private string _currentPlayerName;

        public string CurrentPlayerId { get { return _currentPlayerId; } set { _currentPlayerId = value; OnChanged("CurrentPlayerId"); } }
        public string CurrentPlayerName { get { return _currentPlayerName; } set { _currentPlayerName = value; OnChanged("CurrentPlayerName"); } }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void LoadFromSettings(IDictionary<string, object> settings)
        {
            this.CurrentPlayerId = (settings["CurrentPlayerId"] == null ? null : settings["CurrentPlayerId"].ToString());
            this.CurrentPlayerName = (settings["CurrentPlayerName"] == null ? null : settings["CurrentPlayerName"].ToString());

        }

        public void SaveToSettings(IDictionary<string, object> settings)
        {
            settings["CurrentPlayerId"] = this.CurrentPlayerId;
            settings["CurrentPlayerName"] = this.CurrentPlayerName;
        }
    }
}
