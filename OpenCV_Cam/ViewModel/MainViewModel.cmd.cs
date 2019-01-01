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
        private ICommand _camCommand;
        public ICommand CamCommand
        {
            get { return _camCommand ?? (_camCommand = new RelayCommand(CamAction)); }
        }

        private ICommand _snapshotCommand;
        public ICommand SnapshotCommand
        {
            get { return _snapshotCommand ?? (_snapshotCommand = new RelayCommand(SnapshotAction)); }
        }

        private ICommand _recodeCommand;
        public ICommand RecodeCommand
        {
            get { return _recodeCommand ?? (_recodeCommand = new RelayCommand(RecodeAction)); }
        }
    }
}
