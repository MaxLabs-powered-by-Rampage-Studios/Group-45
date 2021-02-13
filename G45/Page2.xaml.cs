using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices.Sensors;

namespace G45
{
    public partial class Page2 : PhoneApplicationPage
    {
        private const double _gravity = 32.2; // Acceleration of gravity (ft/sec2)
        private const double _damping = 0.5;  // Damping factor for boundary collisions
        private const double _mul = 128.0;    // Motion multiplier (sensitivity)
        private Accelerometer _accel = new Accelerometer();
        private double _sx = 0.0;
        private double _sy = 0.0;
        private double _vx = 0.0;
        private double _vy = 0.0;
        private double _ax = 0.0;
        private double _ay = 0.0;
        private double _time = DateTime.Now.Ticks;

        private double _width, _height;

        public Page2()
        {
            InitializeComponent();

            // Initialize values used for boundary checking
            _width = (Application.Current.Host.Content.ActualWidth - Ball.Width) / 2.0;
            _height = (Application.Current.Host.Content.ActualHeight - Ball.Height) / 2.0;

            // Start the accelerometer
            _accel.CurrentValueChanged +=
                new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(OnReadingChanged);
            _accel.Start();

        }

        private void OnReadingChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {

            // Compute number of seconds elapsed since last interval

            double time = (e.SensorReading.Timestamp.Ticks - _time) / 10000000.0;



            // Compute average accelerations during this time interval

            double ax = ((e.SensorReading.Acceleration.X * _gravity) + _ax) / 2.0;

            double ay = ((e.SensorReading.Acceleration.Y * _gravity) + _ay) / 2.0;



            // Compute velocities at end of this time interval

            double vx = _vx + (ax * time);

            double vy = _vy + (ay * time);



            // Compute new ball position

            double sx = _sx + ((((_vx + vx) / 2.0) * time) * _mul);

            double sy = _sy - ((((_vy + vy) / 2.0) * time) * _mul);



            // Check for boundary collisions and "bounce" if necessary

            if (sx < -_width)
            {

                sx = -_width;

                vx = -vx * _damping;

            }

            else if (sx > _width)
            {

                sx = _width;

                vx = -vx * _damping;

            }



            if (sy < -_height)
            {

                sy = -_height;

                vy = -vy * _damping;

            }

            else if (sy > _height)
            {

                sy = _height;

                vy = -vy * _damping;

            }



            // Save the latest motion parameters

            _time = e.SensorReading.Timestamp.Ticks;

            _ax = ax;

            _ay = ay;

            _vx = vx;

            _vy = vy;

            _sx = sx;

            _sy = sy;

            // Call back to the UI thread to update the ball position
            Dispatcher.BeginInvoke(() => UpdateDisplay(sx, sy));
        }
        private void UpdateDisplay(double x, double y)
        {
            // Update the ball position
            BallTransform.X = x;
            BallTransform.Y = y - 40;
        }
    }
}