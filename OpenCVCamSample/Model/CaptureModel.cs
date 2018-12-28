using MVVM;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace OpenCVCamSample.Model
{
    public class CaptureModel : ObservedObject
    {
        private VideoCapture    _device;

        private WriteableBitmap _screenData;
        public WriteableBitmap ScreenData
        {
            get { return _screenData; }
            set { SetProperty(ref _screenData, value); }
        }

        private bool _captured = false;
        public bool IsCapture
        {
            get { return _captured; }
            set { SetProperty(ref _captured, value); }
        }

        private int _frameWidth;
        public int FrameWidth
        {
            get { return _frameWidth; }
            set { SetProperty(ref _frameWidth, value); }
        }

        private int _frameHeight;
        public int FrameHeight
        {
            get { return _frameHeight; }
            set { SetProperty(ref _frameHeight, value); }
        }

        public bool Init(int cameraIndex)
        {
            try
            {
                _device = VideoCapture.FromCamera(CaptureDevice.Any, cameraIndex);
                _device.FrameWidth = FrameWidth;
                _device.FrameHeight = FrameHeight;
                _device.Open(cameraIndex);

                _screenData = new WriteableBitmap(
                    _device.FrameWidth, _device.FrameHeight, 
                    96, 96, PixelFormats.Bgr24, null);
                return true;
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
                return false;
            }
        }
        public void CaptureStart()
        {
            Mat frame = new Mat();
            Cv2.NamedWindow("1", WindowMode.AutoSize);
            while (true)
            {
                if (_device.Read(frame))
                {
                    Cv2.ImShow("1", frame);
                    WriteableBitmapConverter.ToWriteableBitmap(frame, ScreenData);
                }
                int c = Cv2.WaitKey(10);
                if (c != -1)
                {
                    break;
                }
            }
        }
    }
}
