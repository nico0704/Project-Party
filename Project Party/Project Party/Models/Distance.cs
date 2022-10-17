using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Party.Models
{
    public class Distance
    {
        public String Name { get; set; }
        public int Meters { get; set; }
        public Distance(String name, int meters)
        {
            this.Name = name;
            this.Meters = meters; 
        }
    }
}
