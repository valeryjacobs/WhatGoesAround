using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatGoesAround.Phone.ViewModels
{
    public class ButtonViewModel : INotifyPropertyChanged
    {
        private int _color;
        private int _x;
        private int _y;
        private int _radius;

        public int Color { get { return _color; } set { _color = value; OnPropertyChanged("Color"); } }
        public int X { get { return _x; } set { _x = value; OnPropertyChanged("X"); } }
        public int Y { get { return _y; } set { _y = value; OnPropertyChanged("Y"); } }
        public int Radius { get { return _radius; } set { _radius = value; OnPropertyChanged("Radius"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
