using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System.Threading;
using Windows.UI.Core;
using WhatGoesAround.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WhatGoesAround.RPi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
         private const string signalRHub = "http://whatgoesaroundcomesaround.azurewebsites.net/";
        //http://localhost:11615/
        //private const string signalRHub = "http://localhost:11615/";
        private const string signalRHubProxy = "WGAHub";
        private const string _piID = "D";

        private const int LED_PIN = 5;
        private const int BLUE_PIN = 6;
        private const int INTERAL_PIN = 13;
        private GpioPin redPin;
        private GpioPin bluePin;
        private GpioPinValue redPinValue = GpioPinValue.High;
        private GpioPinValue bluePinValue = GpioPinValue.Low;
        private SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush grayBrush = new SolidColorBrush(Windows.UI.Colors.LightGray);
        private DispatcherTimer dispatcherTimer;
        private int timerCount = 0;
        private string _connectionid = "";

        public MainPage()
        {
            InitializeComponent();
            InitGPIO();
            var hubConnection = new HubConnection(signalRHub);
            var chat = hubConnection.CreateHubProxy(signalRHubProxy);
            chat.On <Common.Action>("SelectPlayer", (message) =>
            {              
                if(message.DeviceId == _piID)
                    SetLeds(message);
            });

            hubConnection.Start().Wait();
            _connectionid = hubConnection.ConnectionId;
            chat.Invoke<string>("Register", _piID);
        }

        private void SetLeds(Common.Action message)
        {
            SetLedPin(redPin, message.Red ? GpioPinValue.Low : GpioPinValue.High);
            SetLedPin(bluePin, message.Blue ? GpioPinValue.Low : GpioPinValue.High);
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
                {
                    redCircle.Visibility = message.Red ? Visibility.Visible : Visibility.Collapsed;
                    blueCircle.Visibility = message.Blue ? Visibility.Visible : Visibility.Collapsed;
                }
            );

            //Wait and turn off the leds
            System.Threading.Tasks.Task.Delay(2000).Wait();
            SetLedPin(redPin, GpioPinValue.High);
            SetLedPin(bluePin, GpioPinValue.High);
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
                {
                    redCircle.Visibility = Visibility.Collapsed;
                    blueCircle.Visibility =Visibility.Collapsed;
                }
            );
        }


        private void SetLedPin(GpioPin pin, GpioPinValue pinValue)
        {
            pin.Write(pinValue);
        }

        private void DiscoLeds()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0,200);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            redPinValue = redPin.Read();
            if (redPinValue == GpioPinValue.Low)
            {
                redPinValue = GpioPinValue.High;
                bluePinValue = GpioPinValue.Low;
            }
            else
            {
                redPinValue = GpioPinValue.Low;
                bluePinValue = GpioPinValue.High;
            }
            redPin.Write(redPinValue);
            bluePin.Write(bluePinValue);

            timerCount += 200;
            if (timerCount > 5000)
                dispatcherTimer.Stop();            
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            redPin = gpio.OpenPin(LED_PIN);
            bluePin = gpio.OpenPin(BLUE_PIN);


            // Initialize LED to the OFF state by first writing a HIGH value
            // We write HIGH because the LED is wired in a active LOW configuration
            redPin.Write(GpioPinValue.High);
            redPin.SetDriveMode(GpioPinDriveMode.Output);
            bluePin.Write(GpioPinValue.Low);
            bluePin.SetDriveMode(GpioPinDriveMode.Output);

            GpioStatus.Text = "GPIO pins initialized correctly.";
        }
    }
}
