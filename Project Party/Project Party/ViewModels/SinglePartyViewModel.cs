using Project_Party.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Party.ViewModels
{
    public class SinglePartyViewModel : BaseViewModel
    {
        Party DetailedParty;
        public String Name { get; set; }
        public SinglePartyViewModel(Party party) {
            DetailedParty = party;
            Name = party.Name;
        }
    }
}
