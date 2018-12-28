using MVVM;
using OpenCVCamSample.Model;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace OpenCVCamSample.ViewModel
{
    public partial class MainViewModel : ObservedObject
    {
        public MainViewModel()
        {
            CaptureModel = new CaptureModel();
            CaptureModel.FrameWidth = 640;
            CaptureModel.FrameHeight = 360;
        }

        private void Capture()
        {
            CaptureModel.Init(0);
            CaptureModel.CaptureStart();
        }

        private void Recode()
        {

        }

        private void Snapshot()
        {

        }
    }
}
