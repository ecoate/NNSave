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

namespace NNSave.Extensions
{
    public static class ViewExtensions
    {
        public static TextView InflateAndBindTextView(this View parentView, int textViewResourceId, string text)
        {
            TextView textView = null;

            if (parentView != null)
            {
                textView = parentView.FindViewById<TextView>(textViewResourceId);

                if (textView != null)
                {
                    textView.Text = text;
                }
            }

            return textView;
        }
    }
}