using Project_Party.Models;
using System;
using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;
using MySqlConnector;
using GoogleApi.Entities.Search.Video.Common;
using System.Threading;

namespace Project_Party.Services
{
    public class MockDataStore : IDataStore<Party>
    {
        readonly List<Party> partys;
        //private MySqlConnection connection;
        public static bool Connected = false;
        string connStr = "Server=s223.goserver.host;Port=3306;User ID=web320;Password=patryk;Database=web320_db1;";
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


        public async Task<bool> Connect()
        {
            bool result = false;
            try
            {
                using (MySqlConnection sqlcon = new MySqlConnection(connStr))
                {
                    sqlcon.Open();
                }

                Console.WriteLine("Connected to Database!");
                result = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return result;


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
            string commandString = "select * from partys";
            using (MySqlConnection sqlcon = new MySqlConnection(connStr))
            {
                sqlcon.Open();
                using (MySqlCommand command = new MySqlCommand(commandString, sqlcon))
                {
                    using(MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {

                            partys.Add(GetPartyFromReader(reader));
                            Console.WriteLine(partys.Count);
                        }
                    }
                }
            }


            return await Task.FromResult(partys);
        }

        private Party GetPartyFromReader(MySqlDataReader reader)
        {
            return new Party { Id = reader.GetInt32("party_id"), Name = reader.GetString("Name"), Description = reader.GetString("Description"), PictureSource = reader.GetString("PictureSource"),
                FlyerSource = reader.GetString("FlyerSource"), Time = reader.GetDateTime("Time"), City = reader.GetString("City"), Adress = reader.GetString("Adress"),
                LocationName = reader.GetString("LocationName"), PartyPositon = new Position(reader.GetDouble("PartyPositionX"), reader.GetDouble("PartyPositionY")),
                Musikart= reader.GetString("Musikart"), Cost= reader.GetString("Cost"), MinAge = reader.GetInt32("MinAge") };
        }
        //To limit the search
        public async Task<IEnumerable<Party>> GetItemsAsync(int count)
        {

            string commandString = "select * from partys LIMIT " + count;
            using (MySqlConnection sqlcon = new MySqlConnection(connStr))
            {
                sqlcon.Open();
                using (MySqlCommand command = new MySqlCommand(commandString, sqlcon))
                {
                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        partys.Clear();
                        while (await reader.ReadAsync())
                        {
                            partys.Add(GetPartyFromReader(reader));
                            
                        }
                    }
                    Console.WriteLine("Count of Partys" + partys.Count);
                }
            }
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
            string commandString = "select * from partys where ID=" + id;
            using (MySqlConnection sqlcon = new MySqlConnection(connStr))
            {
                sqlcon.Open();
                using (MySqlCommand command = new MySqlCommand(commandString, sqlcon))
                {
                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetUInt32("ID"));
                        }
                    }
                }
            }

            return await Task.FromResult(partys.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Party>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(partys);
        }

        //Get all events in the city
        //in time and nearby
        public async Task<IEnumerable<Party>> GetItemsAsync(string city)
        {
            /*
            try
            {
                
                using (MySqlConnection sqlcon = new MySqlConnection(connStr))
                {
                    sqlcon.Open();
                    using (MySqlCommand command = new MySqlCommand(commandString, sqlcon))
                    {
                        partys.Clear();
                        var reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            partys.Add(GetPartyFromReader(reader));
                        }
                        Console.WriteLine(partys.Count);

                        reader.Close();
                    }

                    return await Task.FromResult(partys);
                }

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            */
            return partys;
        }

        public async Task<IEnumerable<Party>> GetItemsAsync(DateTime dateTime)
        {
            try
            {
                String sqlDateTime = dateTime.ToString("yyyy-MM-dd");
                Console.WriteLine(sqlDateTime);
                string commandString = "Select * from partys where Date(Time) = '" + sqlDateTime + "'";
                using (MySqlConnection sqlcon = new MySqlConnection(connStr))
                {
                    sqlcon.Open();
                    using (MySqlCommand command = new MySqlCommand(commandString, sqlcon))
                    {
                        partys.Clear();
                        var reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            partys.Add(GetPartyFromReader(reader));
                        }
                        Console.WriteLine(partys.Count);

                        reader.Close();
                    }

                    return await Task.FromResult(partys);
                }

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return partys;
        }

        //Get all events in the MapSpan for clean MapView
        public async Task<IEnumerable<Party>> GetItemsAsync(MapSpan mapSpan)
        {
            Position center = mapSpan.Center;
            try
            {
                string commandString = "SELECT *  FROM partys where (111.111 * DEGREES(ACOS(LEAST(1.0, COS(RADIANS(`PartyPositionX`)) * COS(RADIANS(" + center.Latitude + ")) * COS(RADIANS(`PartyPositionY` - " + center.Longitude + ")) + SIN(RADIANS(`PartyPositionX`)) * SIN(RADIANS(" + center.Latitude + ")))))) < " + mapSpan.Radius.Kilometers + "LIMIT 100";
                using(MySqlConnection sqlcon = new MySqlConnection(connStr))
                {
                    sqlcon.Open();
                    using (MySqlCommand command = new MySqlCommand(commandString, sqlcon))
                    {
                        partys.Clear();
                        var reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            partys.Add(GetPartyFromReader(reader));
                        }
                        Console.WriteLine(partys.Count);

                        reader.Close();
                    }

                    return await Task.FromResult(partys);
                }
                
            }catch(MySqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return partys;
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