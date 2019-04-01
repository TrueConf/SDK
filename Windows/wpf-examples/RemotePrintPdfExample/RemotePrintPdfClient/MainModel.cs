using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePrintPdfClient
{
    class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> _abook;
        public List<string> Abook
        {
            get
            {
                return _abook;
            }
            set
            {
                _abook = value;
                RaisePropertyChanged("Abook");
            }
        }
        
        private string _selectedID = "";
        public string SelectedID
        {
            get
            {
                return _selectedID;
            }
            set
            {
                _selectedID = value;
                RaisePropertyChanged("SelectedID");
            }
        }

        private string _callTime;
        public string CallTime
        {
            get
            {
                return _callTime;
            }
            set
            {
                _callTime = value;
                RaisePropertyChanged("CallTime");
            }
        }

        private string _peerID;
        public string PeerID
        {
            get
            {
                return _peerID;
            }
            set
            {
                _peerID = value;
                RaisePropertyChanged("PeerID");
            }
        }

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                RaisePropertyChanged("Status");
            }
        }

        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                RaisePropertyChanged("FileName");
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
