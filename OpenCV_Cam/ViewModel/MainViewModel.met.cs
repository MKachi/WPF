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
        public MainViewModel()
        {
            _webCam = new WebCamModel();
            _webCam.Init(0);
        }
        ~MainViewModel()
        {
            _webCam.Destroy();
        }
    }
}
