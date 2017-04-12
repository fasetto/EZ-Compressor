using EZCompressor.Core.DataModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace EZCompressor.UI.ViewModels
{
    public class MainPageViewModel
    {
        private Page _page;

        public MainPageViewModel(Page page)
        {
            _page = page;

            var mainContent = (Border) _page.FindName("MainContent");

            for(int i = 0; i < 100; i++)
                Images.Add(new ImageItem() { Path = "E:\\#dev\\Ez-Compressor\\iamge-item" + i + ".jpg", IsCompressed = i %3 ==0 ? true : false});



            mainContent.DragOver += MainContent_DragOver;
            mainContent.Drop += MainContent_Drop;
        }

        public List<ImageItem> Images { get; set; } = new List<ImageItem>();

        private void MainContent_Drop(object sender, System.Windows.DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;

            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop, false);

        }

        private void MainContent_DragOver(object sender, System.Windows.DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }
    }
}
