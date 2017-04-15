using EZCompressor.Core.Compression;
using EZCompressor.Core.DataModels;
using EZCompressor.UI.Commands;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace EZCompressor.UI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly Page _page;
        private CompressionMode _compressionMode;
        private readonly Compressor<JPEGCompressor> jpegCompressor = new Compressor<JPEGCompressor>();
        private readonly Compressor<PNGCompressor> pngCompressor   = new Compressor<PNGCompressor>();

        public MainPageViewModel(Page page)
        {
            _page = page;
            
            var mainContent = (Border) _page.FindName("MainContent");

            mainContent.Drop += MainContent_Drop;
        }

        public string OutputLocation { get; set; }
        public ObservableCollection<ImageItem> Images { get; set; } = new ObservableCollection<ImageItem>();
        public CompressionMode CompressionMode
        {
            get => _compressionMode;
            set
            {
                _compressionMode = value;
                OnPropertyChanged();
            }
        }
        public ICommand CompressCommand => new ActionCommand(p => Task.Factory.StartNew(() => Compress()));
        public ICommand ClearCommand => new ActionCommand(p => ClearList());
        public ICommand PickFolderCommand => new ActionCommand(p => PickFolder());
        
        private async void Compress()
        {
            if (Images.Count < 1 || string.IsNullOrEmpty(OutputLocation))
                return;

            pngCompressor.CompressionMode = _compressionMode;
            pngCompressor.OutputDirectory = OutputLocation;

            jpegCompressor.CompressionMode = _compressionMode;
            jpegCompressor.Quality = 80;
            jpegCompressor.OutputDirectory = OutputLocation;

            foreach (var image in Images)
            {
                string ext = Path.GetExtension(image.Path);
                CompressionResult result;

                switch (ext)
                {
                    case ".jpg":
                    case ".jpeg":
                        image.CompressionStatus = CompressionStatus.Running;
                        result = await jpegCompressor.CompressAsync(image);
                        image.CompressionStatus = result.Status;
                        break;

                    case ".png":
                        image.CompressionStatus = CompressionStatus.Running;
                        result = await pngCompressor.CompressAsync(image);
                        image.CompressionStatus = result.Status;
                        break;

                    default:
                        image.CompressionStatus = CompressionStatus.Unfinished;
                        //throw new NotSupportedException($"{ext} is not supported.");
                        break;
                }
            }
        }

        private void PickFolder()
        {
            using(var dialog = new CommonOpenFileDialog())
            {
                dialog.Title = "Pick a folder";
                dialog.IsFolderPicker = true;
                dialog.AddToMostRecentlyUsedList = false;
                dialog.AllowNonFileSystemItems = false;
                dialog.EnsureFileExists = true;
                dialog.EnsurePathExists = true;
                dialog.EnsureReadOnly = false;
                dialog.EnsureValidNames = true;
                dialog.Multiselect = false;
                dialog.ShowPlacesList = true;

                if(dialog.ShowDialog() == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    var button = (Hyperlink) _page.FindName("FolderHyperlink");
                    button.Inlines.Clear();
                    button.Inlines.Add(dialog.FileName);

                    OutputLocation = dialog.FileName;
                }
            }
        }

        private void ClearList()
        {
            Images.Clear();
        }

        private void MainContent_Drop(object sender, System.Windows.DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;

            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop, false);

            foreach (var path in files)
            {
                ImageItem image = new ImageItem()
                {
                    Path = path,
                    CompressionStatus = CompressionStatus.None
                };

                Images.Add(image);
            }

        }
    }
}
