using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace WhatGoesAround.Phone.ViewModels
{
    public class ButtonViewModel : INotifyPropertyChanged
    {
        private int _id;
        private Color _color;
        private int _size;
        private int _left;
        private int _top;

        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        public Color Color { get { return _color; } set { _color = value; OnPropertyChanged("Color"); } }
        public int Size { get { return _size; } set { _size = value; OnPropertyChanged("Size"); } }
        public int Left { get { return _left; } set { _left = value; OnPropertyChanged("Left"); } }
        public int Top { get { return _top; } set { _top = value; OnPropertyChanged("Top"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
