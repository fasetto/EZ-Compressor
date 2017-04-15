using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EZCompressor.Core.DataModels
{
    public class ImageItem : INotifyPropertyChanged
    {
        private CompressionStatus _compressionStatus;

        public ImageItem()
        {

        }

        public string Path { get; set; }

        public CompressionStatus CompressionStatus
        {
            get => _compressionStatus;
            set
            {
                _compressionStatus = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

        #endregion
    }
}
