using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenCVCamSample.ViewModel
{
    public partial class MainViewModel : ObservedObject
    {
        private ICommand _captureCommand;
        public ICommand CaptureCommand
        {
            get { return _captureCommand ?? (_captureCommand = new RelayCommand(Capture)); }
        }

        private ICommand _recodeCommand;
        public ICommand RecodeCommand
        {
            get { return _recodeCommand ?? (_recodeCommand = new RelayCommand(Recode)); }
        }

        private ICommand _snapshotCommand;
        public ICommand SnapshotCommand
        {
            get { return _snapshotCommand ?? (_snapshotCommand = new RelayCommand(Snapshot)); }
        }
    }
}
