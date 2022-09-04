using Project_Party.Models;
using System;
using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;
using MySqlConnector;


namespace Project_Party.Services
{
    public class MockDataStore : IDataStore<Party>
    {
        readonly List<Party> partys;
        private MySqlConnection connection;
        public static bool Connected = false;
        private MockDataStore()
        {
            if (!Connected)
            {
                Connect();
                Connected = true;
            }

            partys = new List<Party>();
            /*
            partys = new List<Party>()
            {
                
                new Party { Id = Guid.NewGuid().ToString(), Name = "First item", Description="This is an item description.", PictureName="TestFlyer.jpg", Time=DateTime.Now, City="Fulda", Adress="LeipzigerStraße 21", LocationName="Sclub", PartyPositon = new Position(50.57535144548077, 9.674515176483713) },
                new Party { Id = Guid.NewGuid().ToString(), Name = "Second item", Description="This is an item description.", PictureName="TestFlyer.jpg", Time=DateTime.Now, City="Fulda", Adress="LeipzigerStraße 21", LocationName="Sclub", PartyPositon =  new Position(50.56549200066893, 9.686799350829851)},
                new Party { Id = Guid.NewGuid().ToString(), Name = "Third item", Description="This is an item description.", PictureName="TestFlyer.jpg", Time=DateTime.Now, City="Fulda", Adress="LeipzigerStraße 21", LocationName="Sclub", PartyPositon =  new Position(50.54492846437669, 9.681417933944115)  }
            };
            */

        }

        private static MockDataStore _instance = null;
        public static MockDataStore Instance()
        {
            if (_instance == null)
            {

                _instance = new MockDataStore();
            }


            return _instance;

        }


        public Task<bool> Connect()
        {
            bool result = false;
            try
            {
                string connStr = "Server=s223.goserver.host;Port=3306;User ID=web320;Password=patryk;Database=web320_db1;";
                connection = new MySqlConnection(connStr);
                connection.Open();
                Console.WriteLine("Connected to Server!");
                result = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return Task.FromResult(result);


        }
        public async Task<bool> AddItemAsync(Party item)
        {
            //Noch zu machen
            partys.Add(item);

            return await Task.FromResult(true);
        }

        //is going pobably out
        public async Task<bool> UpdateItemAsync(Party item)
        {
            /*
            var oldItem = partys.Where((Party arg) => arg.Id == item.Id).FirstOrDefault();
            partys.Remove(oldItem);
            partys.Add(item);
            */
            return await Task.FromResult(true);
        }


        public async Task<IEnumerable<Party>> GetItemsAsync()
        {
            partys.Clear();
            string commandString = "select * from Party";
            MySqlCommand command = new MySqlCommand(commandString, connection);


            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //Creating only a Test Party
                //Need to Setup real Database

                partys.Add(GetPartyFromReader(reader));
                Console.WriteLine(partys.Count);
            }
            reader.Close();


            return await Task.FromResult(partys);
        }

        private Party GetPartyFromReader(MySqlDataReader reader)
        {
            return new Party { Id = reader.GetInt32("Id"), Name = reader.GetString("Name"), Description = reader.GetString("Description"), PictureSource = reader.GetString("PictureSource"),
                FlyerSource = reader.GetString("FlyerSource"), Time = reader.GetDateTime("Time"), City = reader.GetString("City"), Adress = reader.GetString("Adress"),
                LocationName = reader.GetString("LocationName"), PartyPositon = new Position(reader.GetDouble("PartyPositionX"), reader.GetDouble("PartyPositionY")),
                Musikart= reader.GetString("Musikart"), Cost= reader.GetString("Cost"), MinAge = reader.GetInt32("MinAge") };
        }
        //To limit the search
        public async Task<IEnumerable<Party>> GetItemsAsync(int count)
        {

            string commandString = "select * from Party LIMIT " + count;
            MySqlCommand command = new MySqlCommand(commandString, connection);


            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                reader.GetUInt32("ID");
                partys.Add(GetPartyFromReader(reader));
                Console.WriteLine(partys.Count);
            }

            reader.Close();

            return await Task.FromResult(partys);
        }


        //Noch zu machen
        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = partys.Where((Party arg) => arg.Id == id).FirstOrDefault();
            partys.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Party> GetItemAsync(int id)
        {
            string commandString = "select * from Party where ID=" + id;
            MySqlCommand command = new MySqlCommand(commandString, connection);


            MySqlDataReader reader = command.ExecuteReader();

            //command.Parameters.AddWithValue("@zip","india");
            while (reader.Read())
            {
                Console.WriteLine(reader.GetUInt32("ID"));
            }

            reader.Close();
            return await Task.FromResult(partys.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Party>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(partys);
        }

        //Get all events in the city
        //in time and nearby
        public Task<IEnumerable<Party>> GetItemsAsync(string city)
        {
            throw new NotImplementedException();
        }

        //Get all events in the MapSpan for clean MapView
        public Task<IEnumerable<Party>> GetItemsAsync(MapSpan mapSpan)
        {
            throw new NotImplementedException();
        }

        //Get all events actualPosition and by Distance
        public Task<IEnumerable<Party>> GetItemsAsync(Position actualPosition, double distance)
        {
            throw new NotImplementedException();
        }
        public Task<Party> GetItem(int id)
        {
            return Task.FromResult(partys.FirstOrDefault(s => s.Id == id));
        }
    }
}