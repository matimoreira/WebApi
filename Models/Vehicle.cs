using System;
using System.Collections.Generic;

namespace WebApi
{
    public class Vehicle
    {
        public int id { get; set; }
        public String name { get; set; }
        public String licenseplate { get; set; }
        public String seriesnumber { get; set; }
        public String motornumber { get; set; }
        public String gps { get; set; }
        public int enterpriseid { get; set; }
        public int vehiclemodelid { get; set; }
        public int vehiclegroupid { get; set; }
        public int vehicletypeid { get; set; }
        public Driver Driver{ get; set; }
        public String isinuse { get; set; }
        public IList<Position> Position { get; set; }
        public String active { get; set; }



    }
}
