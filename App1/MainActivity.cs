using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace App1
{
   [Activity(Label = "App1", MainLauncher = true)]
   public class MainActivity : Activity
   {
      private Button _errorButton;
      private Button _trackEventsButton;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         SetContentView(Resource.Layout.Main);

         AppCenter.Start("2b9c19cb-c7b2-4cae-921f-e3e4474f9aad", typeof(Analytics), typeof(Crashes));

         _errorButton = FindViewById<Button>(Resource.Id.button2);
         _errorButton.Click += _errorButton_Click;

         _trackEventsButton = FindViewById<Button>(Resource.Id.button1);
         _trackEventsButton.Click += _trackEventsButton_Click;
         
         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.Main);
      }

      private void _trackEventsButton_Click(object sender, EventArgs e)
      {
         Analytics.TrackEvent(nameof(_trackEventsButton) + " " + DateTime.Now.ToShortDateString());
      }

      private void _errorButton_Click(object sender, System.EventArgs e)
      {
         try
         {
            throw new Exception();
         }
         catch (Exception exception)
         {
            var props = new Dictionary<string, string>
            {
               { exception.Message, DateTime.Now.ToShortDateString() },
               { "Exception thrown", "_errorButton_Click" }
            };

            Crashes.TrackError(exception);
         }
      }
   }
}

