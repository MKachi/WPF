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

namespace OpenCV_Cam.Model
{
    public class WebCamModel : ObservedObject
    {
        private CvCapture       _capture;
        private DispatcherTimer _updater;
        private WriteableBitmap _writeBitmap;
        private IplImage        _source;

        public void Init(int camIndex)
        {
            try
            {
                _capture = CvCapture.FromCamera(CaptureDevice.Any, camIndex);
                _writeBitmap = new WriteableBitmap(
                    _capture.FrameWidth, _capture.FrameHeight, 
                    96, 96, 
                    PixelFormats.Bgr24, null);
                _updater = new DispatcherTimer();
                _updater.Interval = new TimeSpan(0, 0, 0, 0, 33);
                _updater.Tick += new EventHandler(Update);
            }
            catch (Exception except)
            {
                Destroy();
                MessageBox.Show(except.Message);
            }
        }
        public void Destroy()
        {
            if (!ReferenceEquals(_capture, null))
            {
                _capture.Dispose();
                _capture = null;
            }

            if (!ReferenceEquals(_updater, null))
            {
                _updater.Stop();
            }
        }

        public void Start()
        {
            if (!ReferenceEquals(_updater, null) && !_updater.IsEnabled)
            {
                _updater.IsEnabled = true;
                _updater.Start();
            }
        }
        public void Stop()
        {
            if (!ReferenceEquals(_updater, null))
            {
                _updater.Stop();
                _updater.IsEnabled = false;
            }
        }

        private void Update(object sender, EventArgs e)
        {
            using (_source = _capture.QueryFrame())
            {
                WriteableBitmapConverter.ToWriteableBitmap(_source, _writeBitmap);
            }
        }
    }
}
