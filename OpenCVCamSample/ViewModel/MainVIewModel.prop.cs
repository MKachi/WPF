using MVVM;
using OpenCVCamSample.Model;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace OpenCVCamSample.ViewModel
{
    public partial class MainViewModel : ObservedObject
    {
        private CaptureModel _captureModel;
        public CaptureModel CaptureModel
        {
            get { return _captureModel; }
            set { SetProperty(ref _captureModel, value); }
        }
    }
}
