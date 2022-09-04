using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace Project_Party.Models
{
    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureSource { get; set; }
        public string FlyerSource { get; set; }
        public DateTime Time { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string LocationName { get; set; }
        public Position PartyPositon { get; set; }

        public string Musikart { get; set; }

        public string Cost { get; set; }

        public int MinAge { get; set; }
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
