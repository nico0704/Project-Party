using Org.BouncyCastle.Asn1.Mozilla;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project_Party.Services
{
    public static class SQLFilter
    {
        public static DateTime Time { get; set; }

        public static List<String> Music = new List<String>();

        public static int? Age { get; set; }

        public static String Location { get; set; }
    }
}
