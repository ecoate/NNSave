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

namespace NNSave
{
    [Activity(Label = "LocationDetailActivity")]
    public class LocationDetailActivity : Activity
    {
        private TextView locName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LocationDetailView);
            Location detail = new Location();
            detail.name = Intent.GetStringExtra("LocationName");
            detail.address = Intent.GetStringExtra("LocationAddress");
            detail.phone = Intent.GetStringExtra("LocationPhone");
            detail.email = Intent.GetStringExtra("LocationEmail");
            detail.description = Intent.GetStringExtra("LocationDescription");
            detail.latitude = Intent.GetDoubleExtra("LocationLat", 0);
            detail.longitude = Intent.GetDoubleExtra("LocationLon", 0);

            var locationDetailLayout = LayoutInflater.Inflate(Resource.Layout.LocationDetailView, null);

            // Create your application here
        }
    }
}