using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatGoesAround.Phone.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private GameViewModel _game;

        private ObservableCollection<ButtonViewModel> _buttons;

        private bool _buttonsVisible;

        public GameViewModel Game { get { return _game; } set { _game = value; OnPropertyChanged("Game"); } }
        public ObservableCollection<ButtonViewModel> Buttons { get { return _buttons; } set { _buttons = value; OnPropertyChanged("Buttons"); } }
        public bool ButtonsVisible { get { return _buttonsVisible; } set { _buttonsVisible = value; OnPropertyChanged("ButtonsVisible"); } }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
