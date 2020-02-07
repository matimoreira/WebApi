using System;
using System.Collections.Generic;

namespace WebApi
{
    public class VehicleBrand
    {
        public VehicleBrand(int p_id, String p_name)
        {
            this.id = p_id;
            this.name = p_name;
        }
        public int id { get; set; }
        public String name { get; set; }

    }
}
