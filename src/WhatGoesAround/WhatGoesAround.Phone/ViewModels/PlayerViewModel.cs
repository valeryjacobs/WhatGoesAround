using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatGoesAround.Phone.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged, IComparable
    {
        private string _name;
        private int _score;

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public int Score { get { return _score; } set { _score = value; OnPropertyChanged("Score"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public int CompareTo(object obj)
        {
            return this.Score - ((PlayerViewModel)obj).Score;
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
