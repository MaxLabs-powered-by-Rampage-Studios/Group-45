﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace G45
{
    public partial class Page5 : PhoneApplicationPage
    {
        int show, beg, ges, det1, det2;
        Accelerometer acc;
        Vector3 initial,final;

        public Page5()
        {
            InitializeComponent();
            show = 0;
            beg = 0;
            ges = 0;
            det1 = 0;
            det2 = 0;
            acc = new Accelerometer();

        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            if (show == 0)
            {
                show = 1;
                //textBlock7.Text = "Start"+beg.ToString();
                button1.Content = "Stop";
                beg = 0;
                acc.Start();
                acc.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(detect);
            }
            else
            {
                show = 0;
                //textBlock7.Text = "Stop";
                button1.Content = "Start";
                textBlock4.Text = ":)";
                acc.Stop();
            }
        }

        private void detect(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            if (beg == 0)
            {
                initial = e.SensorReading.Acceleration;
                Deployment.Current.Dispatcher.BeginInvoke(() => updateMyScreen(e.SensorReading));

                if ((initial.X <= 0.1f && initial.X >= -0.1f) && (initial.Z <= -0.9f && initial.Z >= -1.1f) && (initial.Y <= 0.1f && initial.Y >= -0.1f))
                {
                    beg = 60000;
                    ges = 1;
                    det1 = 0;
                    det2 = 0;
                }
                else if ((initial.X <= 0.1f && initial.X >= -0.1f) && (initial.Y <= -0.9f && initial.Y >= -1.1f) && (initial.Z <= 0.1f && initial.Z >= -0.1f))
                {
                    beg = 60000;
                    ges = 2;
                    det1 = 0;
                    det2 = 0;
                }
            }
            else
            {
                final = e.SensorReading.Acceleration;
                switch (ges)
                {
                    case 1: // for circle and victory gesture
                        if (det1 == 3)
                        {
                            Deployment.Current.Dispatcher.BeginInvoke(() => updateScreen("Oh! "));
                            beg = 0;
                            ges = 0;
                            det1 = 0;
                            det2 = 0;
                        }
                        else if (det2 == 3)
                        {
                            Deployment.Current.Dispatcher.BeginInvoke(() => updateScreen("victory! "));
                            beg = 0;
                            ges = 0;
                            det1 = 0;
                            det2 = 0;
                        }
                        else if ((final.X <= 0.1f && final.X >= -0.1f) && (final.Y <= -0.9f && final.Y >= -1.1f) && (final.Z <= 0.1f && final.Z >= -0.1f))
                        {
                            beg = 0;
                            det1 = 0;
                            det2 = 0;
                        }
                        else if (final.X >= 0.1f && final.Y >= 0.1f && final.Z <= -1.1f && (det1 == 0 || det1 == 2))
                        {
                            det1++;
                        }
                        else if (final.X < 0.1f && final.X >= -0.1f && final.Y < 0 && final.Z >= -0.9f && det1 == 1)
                        {
                            det1++;
                        }
                        else if ((final.X < 0 && final.X >= -0.2f) && (final.Y < 0 && final.Y >= -0.2f) && final.Z > -0.9f && det2 == 0)
                        {
                            det2++;
                        }
                        else if ((final.X < 0 && final.X >= -0.2f) && (final.Y > 0 && final.Y <= 0.1f) && final.Z < -1.1f && det2 == 1)
                        {
                            det2++;
                        }
                        else if ((final.X > 0 && final.X <= 0.1f) && (final.Y < 0 && final.Y >= -0.2f) && final.Z > -0.9f && det2 == 2)
                        {
                            det2++;
                        }
                        break;
                    case 2:// for Hello and infinity
                        if (det1 == 4)
                        {
                            Deployment.Current.Dispatcher.BeginInvoke(() => updateScreen("Hello! "));
                            beg = 0;
                            ges = 0;
                            det1 = 0;
                            det2 = 0;
                        }
                        else if (det2 == 6)
                        {
                            Deployment.Current.Dispatcher.BeginInvoke(() => updateScreen("infinity! "));
                            beg = 0;
                            ges = 0;
                            det1 = 0;
                            det2 = 0;
                        }
                        else if ((final.X <= 0.1f && final.X >= -0.1f) && (final.Z <= -0.9f && final.Z >= -1.1f) && (final.Y <= 0.1f && final.Y >= -0.1f))
                        {
                            beg = 0;
                            det1 = 0;
                            det2 = 0;
                        }
                        else if (final.X < -0.1f && final.Y > -0.9f && (det1 == 0 || det1 == 2))
                        {
                            det1++;
                        }
                        else if (final.X > 0.1f && final.Y > -0.9f && (det1 == 1 || det1 == 3))
                        {
                            det1++;
                        }
                        else if (final.Y > -0.9f && (final.X < 0.1 && final.X > -0.1f) && (final.Z < 0.1 && final.Z > -0.1f) && (det2 == 0||det2 ==2 || det2 == 4))
                        {
                            det2++;
                        }
                        else if (final.Y < -1.1f && final.X > 0.1f && final.Z < -0.1f && det2 == 1)
                        {
                            det2++;
                        }
                        else if (final.Y < -1.1f && final.X < -0.1f && final.Z < -0.1f && det2 == 3)
                        {
                            det2++;
                        }
                        else if (final.Y < -1.1f && (final.X < 0.1f && final.X > -0.1f) && final.Z < -0.1f && det2 == 5)
                        {
                            det2++;                            
                        }
                        break;
                }
                if (beg != 0)
                {
                    beg--;
                }

                /*if (final.X >= 0.6f && final.Z >= 0.7f)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() => updateScreen("O"));
                    beg = 0;
                }
                else
                {
                    beg--;
                }*/
            }
        }

        void updateMyScreen(AccelerometerReading e)
        {
            textBlock3.Text = e.Acceleration.ToString();
        }

        void updateScreen(String str)
        {
            textBlock4.Text += str;
        }
    }
}