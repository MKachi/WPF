using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DICOM.ViewModel
{
    public partial class MainViewModel : ObservedObject
    {
        private ICommand _loadJpgCommand;
        public ICommand LoadJpgCommand
        {
            get { return _loadJpgCommand ?? (new RelayCommand(LoadJpg)); }
        }

        private ICommand _loadDCMCommand;
        public ICommand LoadDCMCommand
        {
            get { return _loadDCMCommand ?? (new RelayCommand(LoadDCM)); }
        }

        private ICommand _saveJpgCommand;
        public ICommand SaveJpgCommand
        {
            get { return _saveJpgCommand ?? (new RelayCommand(SaveJpg)); }
        }

        private ICommand _saveDCMCommand;
        public ICommand SaveDCMCommand
        {
            get { return _saveDCMCommand ?? (new RelayCommand(SaveDCM)); }
        }
    }
}
