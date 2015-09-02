using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Manager.Utilities
{
    public class AppStatus : INotifyPropertyChanged
    {
        String appStatusMessage = "Hello World";
        public String AppStatusMessage
        {
            get { return appStatusMessage; }
            private set
            {
                appStatusMessage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AppStatusMessage"));
            }
        }

        SolidColorBrush statusBarColor = new SolidColorBrush(Color.FromRgb(21, 130, 161));
        public SolidColorBrush StatusBarColor
        {
            get { return statusBarColor; }
            private set
            {
                statusBarColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("StatusBarColor"));
            }
        }

        Stack<AppStatus> statusStack = new Stack<AppStatus>();

        public async void StatusMessage(String message, GlobalAppStatus.MessageType messageType)
        {
            AppStatusMessage = message;
            switch(messageType)
            {
                case GlobalAppStatus.MessageType.ERROR:
                    StatusBarColor = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    break;
                case GlobalAppStatus.MessageType.WARNING:
                    StatusBarColor = new SolidColorBrush(Color.FromRgb(209, 196, 17));
                    break;
                case GlobalAppStatus.MessageType.SUCCESS:
                    StatusBarColor = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    break;
                case GlobalAppStatus.MessageType.INFO:
                    StatusBarColor = new SolidColorBrush(Color.FromRgb(21, 130, 161));
                    break;
            }
            await Task.Delay(3000);
            AppStatusMessage = "";
            StatusBarColor = new SolidColorBrush(Color.FromRgb(21, 130, 161));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public static class GlobalAppStatus
    {
        public enum MessageType { INFO, SUCCESS, WARNING, ERROR };
        static AppStatus appStatus = new AppStatus();
        public static AppStatus AppStatus
        { 
            get { return appStatus; }
        }
    }
}
