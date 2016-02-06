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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WhatGoesAround.RPi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string signalRHub = "http://whatgoesaroundweb.azurewebsites.net/";
        private const string signalRHubProxy = "WGAHub";
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

        public MainPage()
        {
            InitializeComponent();
            InitGPIO();
            InitTimer();
            var hubConnection = new HubConnection(signalRHub);
            var chat = hubConnection.CreateHubProxy(signalRHubProxy);
            chat.On<string, string>("Send", (name, message) =>
            {
                InputSignal.Text = string.Format("{0},{1}",name,message);
                

            });

            hubConnection.Start().Wait();
        }

        private void InitTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
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
