using MVVM;
using OpenCV_Cam.Model;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OpenCV_Cam.ViewModel
{
    public partial class MainViewModel : ObservedObject
    {
        public MainViewModel()
        {
            CamContent = "Cam On";
            RecodeContent = "Start Recode";

            WebCam = new WebCamModel();
            WebCam.Init(0, 30);
        }
        ~MainViewModel()
        {
            WebCam.Destroy();
        }

        private bool CheckDirectory()
        {
            DirectoryInfo info = new DirectoryInfo("./Data");
            bool result = info.Exists;
            if (!result)
            {
                info.Create();
            }
            return result;
        }

        private void CamAction()
        {
            if (CamContent.Equals("Cam On"))
            {
                WebCam.Start();
                CamContent = "Cam Off";
            }
            else
            {
                WebCam.Stop();
                CamContent = "Cam On";
            }
        }
        private void SnapshotAction()
        {
            if (CheckDirectory())
            {
                string filename = string.Format("./Data/{0}.jpg", DateTime.Now.ToString("yyyy년 MM월 dd일 - hh시 mm분 ss초"));
                WebCam.Snapshot(filename);
            }
        }
        private void RecodeAction()
        {
            if (RecodeContent.Equals("Start Recode"))
            {
                if (CheckDirectory())
                {
                    string filename = string.Format("./Data/{0}.avi", DateTime.Now.ToString("yyyy년 MM월 dd일 - hh시 mm분 ss초"));
                    WebCam.StartRecode(filename, 15.0);
                    RecodeContent = "Stop Recode";
                }
            }
            else
            {
                RecodeContent = "Start Recode";
                WebCam.StopRecode();
            }
        }
    }
}
