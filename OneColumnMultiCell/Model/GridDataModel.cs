using MVVM;
using System;

namespace OneColumnMultiCell.Model
{
    public class GridDataModel : ObservedObject
    {
        private bool _changeValue;
        public bool ChangeValue
        {
            get { return _changeValue; }
            set { SetProperty(ref _changeValue, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private float _value;
        public float Value
        {
            get { return _value; }
            set
            {
                SetProperty(ref _value, value);
                CorrectValue = (float)Math.Truncate(_value * 10) / 10;
            }
        }

        private float _correctValue;
        public float CorrectValue
        {
            get { return _correctValue; }
            set { SetProperty(ref _correctValue, value); }
        }
    }
}
