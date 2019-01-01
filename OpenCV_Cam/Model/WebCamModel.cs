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
        private CvCapture       _capture = null;
        private DispatcherTimer _updater = null;

        private bool            _recoding = false;
        private CvVideoWriter   _recoder = null;

        private WriteableBitmap _writeBitmap = null;
        public WriteableBitmap  CamScreen
        {
            get { return _writeBitmap; }
            set { SetProperty(ref _writeBitmap, value); }
        }
        public IplImage         Source
        {
            get { return _capture.QueryFrame(); }
        }

        public void Init(int camIndex, int updateInterval)
        {
            try
            {
                _capture = CvCapture.FromCamera(CaptureDevice.Any, camIndex);
                CamScreen = new WriteableBitmap(
                    _capture.FrameWidth, _capture.FrameHeight, 
                    96, 96, 
                    PixelFormats.Bgr24, null);
                _updater = new DispatcherTimer();
                _updater.Interval = new TimeSpan(0, 0, 0, 0, updateInterval);
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

        public void Snapshot(string filename)
        {
            Cv.SaveImage(filename, Source);
        }

        public void StartRecode(string filename, double fps)
        {
            _recoder = new CvVideoWriter(filename, FourCC.XVID, fps,
                    new CvSize(CamScreen.PixelWidth, CamScreen.PixelHeight), true);
            _recoding = true;
        }
        public void StopRecode()
        {
            if (!ReferenceEquals(_recoder, null))
            {
                _recoder.Dispose();
                _recoder = null;
                _recoding = false;
            }
        }

        private void Update(object sender, EventArgs e)
        {
            using (IplImage image = _capture.QueryFrame())
            {
                WriteableBitmapConverter.ToWriteableBitmap(image, _writeBitmap);
            }

            if (_recoding)
            {
                using (IplImage image = _capture.QueryFrame())
                {
                    _recoder.WriteFrame(image);
                }
            }
        }
    }
}
