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
        private RetrieveData dataGetter = new RetrieveData();
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var retClass = new RetrieveData();

            List<Location> locs = await retClass.GetAllLocations();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            locationListView = FindViewById<ListView>(Resource.Id.locationsListView);
            locationListView.ItemClick += Item_Clicked;

            //test data
            //Location test = new Location();
            //test.address = "123 testing address";
            //test.name = "Plaza Azteca";
            //test.visitCount = 165;
            //locationList.Add(test);
            //locationList.Add(test);
            //locationList.Add(test);

            //real data
            locationList = await dataGetter.GetAllLocations();            
            locationListView.Adapter = new LocationListAdapter(this, locationList);
        }

        protected void Item_Clicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(LocationDetailActivity));
            Location clickedLoc = new Location();
            clickedLoc = locationList[e.Position];
            intent.PutExtra("LocationName", clickedLoc.name);
            intent.PutExtra("LocationAddress", clickedLoc.address);
            intent.PutExtra("LocationPhone", clickedLoc.phone);
            intent.PutExtra("LocationEmail", clickedLoc.email);
            intent.PutExtra("LocationDescription", clickedLoc.description);
            intent.PutExtra("LocationVisitCount", clickedLoc.visitCount);
            intent.PutExtra("LocationLat", clickedLoc.latitude);
            intent.PutExtra("LocationLon", clickedLoc.longitude);
            StartActivity(intent);
        }
    }
}

