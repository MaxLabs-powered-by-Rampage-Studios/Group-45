using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices.Sensors;

namespace G45
{
    public partial class Page3 : PhoneApplicationPage
    {
        private ShakeDetector _shakeDecetor;
        int i, d;
        public Page3()
        {
            InitializeComponent();
            i = 0;
            d = 0;
            textBlock1.Text = i.ToString();
            _shakeDecetor = new ShakeDetector();
            _shakeDecetor.ShakeEvent += new EventHandler<EventArgs>(_shakeDecetor_ShakeEvent);
            _shakeDecetor.Start();
        }

        void _shakeDecetor_ShakeEvent(object sender, EventArgs e)
        {
            if (d == 1)
            {
                this.Dispatcher.BeginInvoke(() =>
                {
                    i++;
                    Storyboard shakeAnimation = Resources["ShakeAnimation"] as Storyboard;
                    textBlock1.Text = i.ToString();
                    textBlock2.Text = ShakeDetector.D.ToString();
                    textBlock3.Text = ShakeDetector.deg.ToString();
                    shakeAnimation.Begin();
                });
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (d == 0)
            {
                d = 1;
                button1.Content = "SHOW";
            }
            else if (d == 1)
            {
                d = 0;
                button1.Content = "START";
                textBlock4.Text = i.ToString();
            }
        }
    }
}