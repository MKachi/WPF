using MVVM;
using OpenCV_Cam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private string _recodeButtonContent;
        public string RecodeButtonContent
        {
            get { return _recodeButtonContent; }
            set { SetProperty(ref _recodeButtonContent, value); }
        }
    }
}
