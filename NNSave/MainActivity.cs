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
using NNSave.Services;

namespace NNSave
{
    [Activity(Label = "NN$ave", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private List<Location> locationList = new List<Location>();
        private ListView locationListView;
        private Button mapButton;
        private RetrieveData dataGetter = new RetrieveData();
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var retClass = new RetrieveData();

            List<Location> locs = await retClass.GetAllLocations();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            locationListView = FindViewById<ListView>(Resource.Id.locationsListView);
            mapButton = FindViewById<Button>(Resource.Id.goToMapButton);

            locationListView.ItemClick += Item_Clicked;            
            mapButton.Click += Map_Button_Clicked;
            //real data
            locationList = await dataGetter.GetAllLocations();            
            locationListView.Adapter = new LocationListAdapter(this, locationList);

            //ServiceApp
            ServiceApp.Current.LocationServiceConnected += (object sender, ServiceConnectedEventArgs e) => {
                ServiceApp.Current.LocationService.LocationChanged += HandleLocationChanged;
            };
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

        protected void Map_Button_Clicked(object sender, EventArgs ea)
        {
            var intent = new Intent(this, typeof(LocationMainMapActivity));
            StartActivity(intent);
        }


        ///<summary>
        /// Updates UI with location data
        /// </summary>
        public void HandleLocationChanged(object sender, Android.Locations.LocationChangedEventArgs e)
        {
            Android.Locations.Location location = e.Location;
            //Log.Debug(logTag, "Foreground updating");
            LocationDetection.LocationChanged(e.Location, this);

            // these events are on a background thread, need to update on the UI thread
            //RunOnUiThread(() => {
            //    latText.Text = String.Format("Latitude: {0}", location.Latitude);
            //    longText.Text = String.Format("Longitude: {0}", location.Longitude);
            //    altText.Text = String.Format("Altitude: {0}", location.Altitude);
            //    speedText.Text = String.Format("Speed: {0}", location.Speed);
            //    accText.Text = String.Format("Accuracy: {0}", location.Accuracy);
            //    bearText.Text = String.Format("Bearing: {0}", location.Bearing);
            //});

        }
    }
}

