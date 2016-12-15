using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LightSensors
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var lightSensor = LightSensor.GetDefault();
            if (lightSensor == null)
            {
                MainText.Text = "There is no light sensor in this device";
                return;
            }
            lightSensor.ReportInterval = 15;
            lightSensor.ReadingChanged += async (s, e) =>
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    if (e.Reading.IlluminanceInLux < 30)
                    {

                        RequestedTheme = ElementTheme.Dark;
                        MainText.Text = "Night";
                    }
                    else
                    {
                        RequestedTheme = ElementTheme.Light;
                        MainText.Text = "Day";
                    }
                });
            };
        }
    }
}
