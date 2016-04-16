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
using System.Threading.Tasks;

namespace NNSave
{
    [Activity(Label = "LocationMainMapActivity")]
    public class LocationMainMapActivity : Activity, IOnMapReadyCallback
    {
        ImageView _GetDirectionsActionImageView;
        LatLng _GeocodedLocation;
        GoogleMap _GoogleMap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LocationMapView);
            // Create your application here

            var mapview = FindViewById<MapView>(Resource.Id.bigMap);

            // create the map view with the context
            mapview.OnCreate(savedInstanceState);

            // get the map, which calls the OnMapReady() method below (by virtue of the IOnMapReadyCallback interface that this class implements)
            mapview.GetMapAsync(this);
        }

        public async void OnMapReady(GoogleMap googleMap)
        {
            _GoogleMap = googleMap;
            _GoogleMap.UiSettings.MapToolbarEnabled = true;
            _GeocodedLocation = await GetPositionAsync();

            if (_GeocodedLocation != null)
            {
                // because we now have coordinates, show the get directions action image view, and wire up its click handler
                // _GetDirectionsActionImageView.Visibility = ViewStates.Visible;

                // initialze the map
                MapsInitializer.Initialize(this);

                // display the map region that contains the point. (the zoom level has been defined on the map layout in AcquaintanceDetail.axml)
                _GoogleMap.MoveCamera(CameraUpdateFactory.NewLatLng(_GeocodedLocation));

                // create a new pin
                var marker = new MarkerOptions();

                // set the pin's position
                marker.SetPosition(new LatLng(_GeocodedLocation.Latitude, _GeocodedLocation.Longitude));

                // add the pin to the map
                _GoogleMap.AddMarker(marker);
            }

        }

        async Task<LatLng> GetPositionAsync()
        {
            return new LatLng(37.063892, -76.493166);
        }
    }
}