using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatGoesAround.Common;
using Windows.UI;

namespace WhatGoesAround.Phone.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private GameViewModel _game;

        //private ObservableCollection<ButtonViewModel> _buttons;
        private ButtonViewModel _button1;
        private ButtonViewModel _button2;

        private bool _buttonsVisible;

        public GameViewModel Game { get { return _game; } set { _game = value; OnPropertyChanged("Game"); } }
        public ButtonViewModel Button1 { get { return _button1; } set { _button1 = value; OnPropertyChanged("Button1"); } }
        public ButtonViewModel Button2 { get { return _button2; } set { _button2 = value; OnPropertyChanged("Button2"); } }

        //public ObservableCollection<ButtonViewModel> Buttons { get { return _buttons; } set { _buttons = value; OnPropertyChanged("Buttons"); } }
        public bool ButtonsVisible { get { return _buttonsVisible; } set { _buttonsVisible = value; OnPropertyChanged("ButtonsVisible"); } }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            this.Game = new GameViewModel();
            //this.Buttons = new ObservableCollection<ButtonViewModel>()
            //{
            //    new ButtonViewModel() {Id =1, Color = Color.FromArgb(255, 0, 0, 0), Radius=50, X = 100, Y =20  },
            //    new ButtonViewModel() {Id =2, Color = Color.FromArgb(0, 0, 255, 0), Radius=30 , X = 250, Y = 60 }
            //};
            this.Button1 = new ButtonViewModel() { Id = 1, Color = Color.FromArgb(255, 0, 0, 0), Left = 100, Top = 100, Size = 60 };
            this.Button2 = new ButtonViewModel() { Id = 2, Color = Color.FromArgb(0, 0, 255, 0), Left = 350, Top = 250, Size = 100 };
        }

        #region Game Event handlers

        public void OnGameBeginning(BeginGameMessage message)
        {

        }

        public void OnRoundBeginning(BeginRoundMessage message)
        {

        }

        public void OnRoundEnding(EndRoundMessage message)
        {

        }

        public void OnPlayerScoresUpdating(UpdatePlayerScoresMessage message)
        {

        }

        #endregion
    }
}
