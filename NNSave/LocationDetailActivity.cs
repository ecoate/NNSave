using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NNSave.Data;
using NNSave.Extensions;

using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace NNSave
{
    [Activity(Label = "LocationDetailActivity")]
    public class LocationDetailActivity : Activity, IOnMapReadyCallback
    {
        private List<Location> locationList = new List<Location>();
        private ListView locationDetailListView;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LocationDetailView);

            locationDetailListView = FindViewById<ListView>(Resource.Id.locationsDetailListView);

            Location detail = new Location();
            detail.name = Intent.GetStringExtra("LocationName");
            detail.address = Intent.GetStringExtra("LocationAddress");
            detail.phone = Intent.GetStringExtra("LocationPhone");
            detail.email = Intent.GetStringExtra("LocationEmail");
            detail.description = Intent.GetStringExtra("LocationDescription");
            detail.visitCount= Intent.GetIntExtra("LocationVisitCount", 0);        
            detail.latitude = Intent.GetDoubleExtra("LocationLat", 0);
            detail.longitude = Intent.GetDoubleExtra("LocationLon", 0);

            var locationDetailLayout = LayoutInflater.Inflate(Resource.Layout.LocationDetailView, null);

            locationList.Add(detail);
            locationDetailListView.Adapter = new LocationDetailAdapter(this, locationList);

            var mapview = FindViewById<MapView>(Resource.Id.map);

            // create the map view with the context
            mapview.OnCreate(savedInstanceState);

            // get the map, which calls the OnMapReady() method below (by virtue of the IOnMapReadyCallback interface that this class implements)
            mapview.GetMapAsync(this);

        }

        public void OnMapReady(GoogleMap googleMap)
        {
            throw new NotImplementedException();
        }
    }
}