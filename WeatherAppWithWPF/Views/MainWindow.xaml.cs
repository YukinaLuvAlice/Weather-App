using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;
using WeatherAppWithWPF.Commands;

namespace WeatherAppWithWPF.Views
{
    public partial class MainWindow : Window
    {
        private TaskbarIcon taskbarIcon;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTaskbarIcon();
        }

        private void InitializeTaskbarIcon()
        {
            var iconUri = new Uri("pack://application:,,,/Images/weather.ico");
            taskbarIcon = new TaskbarIcon
            {
                Icon = new System.Drawing.Icon(Application.GetResourceStream(iconUri).Stream),
                ToolTipText = "Weather App",
                Visibility = Visibility.Hidden
            };

            // Tạo context menu
            var contextMenu = new ContextMenu();

            var openItem = new MenuItem { Header = "Open" };
            openItem.Click += (s, e) => ShowMainWindow();

            var exitItem = new MenuItem { Header = "Exit" };
            exitItem.Click += (s, e) => ExitApplication();

            contextMenu.Items.Add(openItem);
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(exitItem);

            taskbarIcon.ContextMenu = contextMenu;

            // Sửa lại cách tạo command
            taskbarIcon.DoubleClickCommand = new RelayCommand(() => ShowMainWindow());
        }

        private void ShowMainWindow()
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Activate();
            taskbarIcon.Visibility = Visibility.Hidden;
        }

        private void ExitApplication()
        {
            taskbarIcon.Dispose();
            Application.Current.Shutdown();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                this.Hide();
                taskbarIcon.Visibility = Visibility.Visible;
            }
            base.OnStateChanged(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            taskbarIcon.Visibility = Visibility.Visible;
            base.OnClosing(e);
        }
    }
}