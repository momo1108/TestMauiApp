﻿using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestMauiApp.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        Microsoft.UI.Xaml.Window nativeWindow;
        int screenWidth, screenHeight;
        const int desiredWidth = 1000;
        const int desiredHeight = 1720;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
                IWindow mauiWindow = handler.VirtualView;
                nativeWindow = handler.PlatformView;
                nativeWindow.Activated += OnWindowActivated;
                nativeWindow.Activate();

                // allow Windows to draw a native titlebar which respects IsMaximizable/IsMinimizable
                nativeWindow.ExtendsContentIntoTitleBar = false;

                IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
                AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

                // set a specific window size
                appWindow.MoveAndResize(new RectInt32((screenWidth - desiredWidth) / 2, (screenHeight - desiredHeight) / 2, desiredWidth, desiredHeight));

                if (appWindow.Presenter is OverlappedPresenter p)
                {
                    p.IsResizable = false;
                    // these only have effect if XAML isn't responsible for drawing the titlebar.
                    p.IsMaximizable = false;
                    p.IsMinimizable = false;
                }
            });
        }


        private void OnWindowActivated(object sender, Microsoft.UI.Xaml.WindowActivatedEventArgs args)
        {
            // Retrieve the screen resolution
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
            screenWidth = (int)displayInfo.Width;
            screenHeight = (int)displayInfo.Height;
            // Remove this event handler since it is not needed anymore
            nativeWindow.Activated -= OnWindowActivated;
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }

}
