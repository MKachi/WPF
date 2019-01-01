using MVVM;
using OpenCV_Cam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OpenCV_Cam.ViewModel
{
    public partial class MainViewModel : ObservedObject
    {
        private WebCamModel _webCam = null;
        public WebCamModel WebCam
        {
            get { return _webCam; }
            set { SetProperty(ref _webCam, value); }
        }

        private string _camContent;
        public string CamContent
        {
            get { return _camContent; }
            set { SetProperty(ref _camContent, value); }
        }

        private string _recodeContent;
        public string RecodeContent
        {
            get { return _recodeContent; }
            set { SetProperty(ref _recodeContent, value); }
        }
    }
}
