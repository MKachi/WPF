using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenCV_Cam.ViewModel
{
    public partial class MainViewModel : ObservedObject
    {
        private ICommand _recodeCommand;
        public ICommand RecodeCommand
        {
            get { return _recodeCommand ?? (_recodeCommand = new RelayCommand(Recode)); }
        }
    }
}
