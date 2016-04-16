using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNSave.Data
{
    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public object email { get; set; }
        public string website { get; set; }
        public string description { get; set; }
        public int visitCount { get; set; }
        public double distance { get; set; }

    }
}
