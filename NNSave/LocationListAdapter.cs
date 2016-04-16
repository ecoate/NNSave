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
    public class LocationListAdapter : BaseAdapter<Location>
    {
        List<Location> items;
        Activity context;

        public LocationListAdapter(Activity context, List<Location> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Location this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];           

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.LocationListRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.locationNameTextView).Text = item.name;
            convertView.FindViewById<TextView>(Resource.Id.locationAddressTextView).Text = item.address;
            convertView.FindViewById<TextView>(Resource.Id.locationVistCountTextView).Text = item.visitCount.ToString();

            return convertView;
        }
    }
}