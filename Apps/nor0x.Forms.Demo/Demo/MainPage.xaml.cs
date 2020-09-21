using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace Demo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void SendEvent_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(EventNameEntry.Text))
                {
                    EventNameEntry.Text = "testEvent";
                }
                if (!EventParamsEntry.Text.Contains(':'))
                {
                    EventParamsEntry.Text = "testKey:testValue";
                }

                var parameters = EventParamsEntry.Text.Split(',');

                var eventParams = new Dictionary<string, string>();
                foreach (var eventPair in parameters)
                {
                    var split = eventPair.Split(':');
                    if (split.Count() == 2)
                    {
                        eventParams.Add(split[0], split[1]);
                    }
                    else
                    {
                        eventParams.Add("testKey", "testValue");
                    }
                }
                Analytics.TrackEvent(EventNameEntry.Text, eventParams);
            }
            catch (Exception ex)
            {
                //i.e. something is wrong with parsing the entries
                Crashes.TrackError(ex);
            }
        }
        void SendCrash_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (!CrashParamsEntry.Text.Contains(':'))
                {
                    CrashParamsEntry.Text = "testKey:testValue";
                }

                var parameters = CrashParamsEntry.Text.Split(',');

                var crashParams = new Dictionary<string, string>();
                foreach (var eventPair in parameters)
                {
                    var split = eventPair.Split(':');
                    if (split.Count() == 2)
                    {
                        crashParams.Add(split[0], split[1]);
                    }
                    else
                    {
                        crashParams.Add("testKey", "testValue");
                    }
                }
                Crashes.TrackError(new Exception(), crashParams);
            }
            catch (Exception ex)
            {
                //i.e. something is wrong with parsing the entries
                Crashes.TrackError(ex);
            }
        }

        void SendTestCrash_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                Crashes.GenerateTestCrash();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        void CrashApp_Clicked(System.Object sender, System.EventArgs e)
        {
            Crashes.GenerateTestCrash();
        }
    }
}
