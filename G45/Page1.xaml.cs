using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices.Sensors;

namespace G45
{
    public partial class Page1 : PhoneApplicationPage
    {
        Accelerometer accelerometer = new Accelerometer();
        double iterator = 0;
        public Page1()
        {
            InitializeComponent();
            accelerometer.Start();
            accelerometer.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(myHandler);
        }
        void myHandler(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => updateMyScreen(e.SensorReading));
        }
        void updateMyScreen(AccelerometerReading e)
        {
            
            // updates the textblocks
            xreadout.Text = e.Acceleration.X.ToString("0.00");
            yreadout.Text = e.Acceleration.Y.ToString("0.00");
            zreadout.Text = e.Acceleration.Z.ToString("0.00");

            //draws on the canvas:

            double currentXOnGraph = Math.Abs((e.Acceleration.X * 200) - 200);
            double currentYOnGraph = Math.Abs((e.Acceleration.Y * 200) - 200);
            double currentZOnGraph = Math.Abs((e.Acceleration.Z * 200) - 200);

            Rectangle xPoint = new Rectangle();
            Rectangle yPoint = new Rectangle();
            Rectangle zPoint = new Rectangle();

            xPoint.Fill = new SolidColorBrush(Colors.Red);
            yPoint.Fill = new SolidColorBrush(Colors.Blue);
            zPoint.Fill = new SolidColorBrush(Colors.Green);

            // these pixels will be "pasted into" the canvas by setting their position according
            // to the currentX/Y/Z-on-graph values. To set their position relative to the canvas,
            // pass the canvas properties on to the pixels via the SetValue method.
            // use a generic iterator to determine the distance the pixel should be from the left
            // side of the canvas.

            xPoint.SetValue(Canvas.LeftProperty, iterator);
            xPoint.SetValue(Canvas.TopProperty, currentXOnGraph);

            yPoint.SetValue(Canvas.LeftProperty, iterator);
            yPoint.SetValue(Canvas.TopProperty, currentYOnGraph);

            zPoint.SetValue(Canvas.LeftProperty, iterator);
            zPoint.SetValue(Canvas.TopProperty, currentZOnGraph);

            // set the pixel size
            xPoint.Width = 1;
            xPoint.Height = 2;
            yPoint.Width = 1;
            yPoint.Height = 2;
            zPoint.Width = 1;
            zPoint.Height = 2;

            // finally, associate pixels with the canvas.

            Log.Children.Add(xPoint);
            Log.Children.Add(yPoint);
            Log.Children.Add(zPoint);

            if (iterator == 399)
            {
                Log.Children.Clear();
                iterator = 0;
            }
            else
            {
                iterator++;
            }
        }
        private void PlayOrPause_Click(object sender, RoutedEventArgs e)
        {
            if (PlayOrPause.Content.ToString() == "PAUSE")
            {
                PlayOrPause.Content = "PLAY";
                accelerometer.Stop();
            }
            else if (PlayOrPause.Content.ToString() == "PLAY")
            {
                accelerometer.Start();
                PlayOrPause.Content = "PAUSE";
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}