using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using NNSave.Data;
using System.Collections.Generic;

using System.Collections.Generic;

namespace NNSave
{
    [Activity(Label = "NNSave", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private List<Location> locationList = new List<Location>();
        private ListView locationListView;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var retClass = new RetrieveData();

            List<Location> locs = await retClass.GetAllLocations();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            locationListView = FindViewById<ListView>(Resource.Id.locationsListView);
            Location test = new Location();
            test.address = "123 testing address";
            test.name = "Plaza Azteca";
            test.visitCount = 165;
            locationList.Add(test);
            locationList.Add(test);
            locationList.Add(test);

            locationListView.Adapter = new LocationListAdapter(this, locationList);
        }
    }
}

