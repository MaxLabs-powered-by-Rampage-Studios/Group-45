using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace G45
{
    public partial class Page4 : PhoneApplicationPage
    {
        private ShakeDetector _shakeDecetor;
        int i, d, k;
        public Page4()
        {
            InitializeComponent();
            i = 0;
            d = 0;
            k = 0;
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
                    textBlock7.Text = (i - k).ToString();
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
                k = i;
                button1.Content = "SHOW";
            }
            else if (d == 1)
            {
                d = 0;
                button1.Content = "START";
                textBlock4.FontSize = 72;
                textBlock4.Text = textBlock4.Text + i.ToString() + '.';
                textBlock5.Text = textBlock5.Text + ((char)('A' + ((i % 26) - 1))) + '.';
                textBlock6.Text = textBlock6.Text + ((char)('A' + (((i - k) % 26) - 1)));
            }
        }
    }
}