﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Models
{
    public class FoundPhoto : INotifyPropertyChanged
    {
        private bool _isSelected;
        public string FilePath { get; set; }
        public double Yaw { get; set; }
        public double Pitch { get; set; }
        public double HFOV { get; set; }
        public double VFOV { get; set; }
        public double DistanceFromCenter { get; set; }
        public double DistanceFromCamera { get; set; }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
