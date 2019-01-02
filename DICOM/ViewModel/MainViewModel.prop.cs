using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DICOM.ViewModel
{
    public partial class MainViewModel : ObservedObject
    {
        private BitmapImage _displayImage;
        public BitmapImage DisplayImage
        {
            get { return _displayImage; }
            set { SetProperty(ref _displayImage, value); }
        }
    }
}
