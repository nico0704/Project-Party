using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Party.Models
{
    public class Party
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureName { get; set; }
        public DateTime Time { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string LocationName { get; set; }

       /* public Party(int partyId,string name, string pictureName, string description, DateTime time, string locationName, string city, string adress)
        {
            Id = partyId;
            Name = name;
            Description = description;
            PictureName = pictureName;
            Time = time;
            LocationName = locationName;
            City = city;
            Adress = adress;
        }
       */
    }
}
