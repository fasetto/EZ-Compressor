using EZCompressor.Core.Helpers;
using EZCompressor.UI.Commands;
using EZCompressor.UI.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EZCompressor.UI.ViewModels
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class WindowViewModel : ViewModelBase
    {
        #region Private Members

        /// <summary>
        /// The window this view-model controls
        /// </summary>
        private Window _window;
        
        /// <summary>
        /// The window resizer helper that keeps the window size correct in various states
        /// </summary>
        private WindowResizer _windowResizer;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int _outerMarginSize = 10;

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        private int _windowRadius = 5;

        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition _dockPosition = WindowDockPosition.Undocked;

        #endregion

        #region Public Properties

        public Page CurrentPage { get; set; } = new MainPage();

        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 800;

        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 500;

        /// <summary>
        /// True if the window should be borderless because it is docked or maximized
        /// </summary>
        public bool Borderless { get { return (_window.WindowState == WindowState.Maximized || _dockPosition != WindowDockPosition.Undocked); } }

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get { return Borderless ? 0 : 6; } }

        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                // If it is maximized or docked, no border
                return Borderless ? 0 : _outerMarginSize;
            }
            set { _outerMarginSize = value; }
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                // If it is maximized or docked, no border
                return Borderless ? 0 : _windowRadius;
            }
            set { _windowRadius = value; }
        }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 42;
        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }


        #endregion


        public ICommand MinimizeCommand => new ActionCommand(p => _window.WindowState = WindowState.Minimized);
        public ICommand MaximizeCommand => new ActionCommand(p => _window.WindowState ^= WindowState.Maximized);
        public ICommand CloseCommand => new ActionCommand(p => _window.Close());
        public ICommand MenuCommand => new ActionCommand(p => SystemCommands.ShowSystemMenu(_window, GetMousePosition()));


        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            _window = window;

            _window.StateChanged += (sender, e) => WindowResized();

            // Fix window resize issue
            _windowResizer = new WindowResizer(_window);

            _windowResizer.WindowDockChanged += (dock) =>
            {
                _dockPosition = dock;

                WindowResized();
            };
        }


        #region Private Helpers

        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(_window);

            // Add the window position so its a "ToScreen"
            if (_window.WindowState == WindowState.Maximized)
                return new Point(position.X + _windowResizer.CurrentMonitorSize.Left, position.Y + _windowResizer.CurrentMonitorSize.Top);
            else
                return new Point(position.X + _window.Left, position.Y + _window.Top);
        }

        /// <summary>
        /// If the window resizes to a special position (docked or maximized)
        /// this will update all required property change events to set the borders and radius values
        /// </summary>
        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }


        #endregion
    }
}
