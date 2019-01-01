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
            RecodeButtonContent = "Start Recode";
            WebCam = new WebCamModel();
            WebCam.Init(0, 30);
        }
        ~MainViewModel()
        {
            WebCam.Destroy();
        }

        private void Recode()
        {
            if (RecodeButtonContent.Equals("Start Recode"))
            {
                WebCam.Start();
                RecodeButtonContent = "Stop Recode";
            }
            else
            {
                WebCam.Stop();
                RecodeButtonContent = "Start Recode";
            }
        }
    }
}
