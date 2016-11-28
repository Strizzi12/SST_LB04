using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDBConnection;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.IO;

namespace Boerse
{
	class BoersenApplication
	{
		static void Main(string[] args)
		{
			Debug.WriteLine("Started!");
			ServerSettings settings = new ServerSettings();
			//settings.Host = "ec2-35-164-218-97.us-west-2.compute.amazonaws.com";    //93.82.35.63 
			settings.Host = "localhost";    //93.82.35.63 
			settings.Port = "1234";
			insertInitialData();
			try
			{
				using(var server = new RestServer(settings))
				{
					server.LogToConsole().Start();
					//New Thread needs to be started, which calculates the market prizes and handles the orders.
					Console.ReadLine();
					server.Stop();
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine("Error: " + ex);
				Console.ReadLine();
			}
		}

		/// <summary>
		/// This function should insert data into the database
		/// </summary>
		private static void insertInitialData()
		{
			string result = string.Empty;
			Stock stock = new Stock();
			var dbStocks = dbConnectionStocks._db;
			var entrys = dbStocks.Find(new BsonDocument()).ToList();
			foreach(var item in entrys)
			{
				stock.aktienID = new Guid(item.GetElement("aktienID").Value.ToString());
				stock.name = item.GetElement("name").Value.ToString();
				stock.course = double.Parse(item.GetElement("course").Value.ToString(), System.Globalization.NumberStyles.Any);
				stock.amount = Int32.Parse(item.GetElement("amount").Value.ToString(), System.Globalization.NumberStyles.Any);

				result += JsonConvert.SerializeObject(stock);
			}
			if(!result.Equals(string.Empty))
				return;

			var entry = new BsonDocument
			{
				{"aktienID", Guid.NewGuid().ToString() },
				{"name", "Super Firma von Andi"},
				{"course", 100},
				{"amount", 1000}
			};
			dbConnectionStocks._db.InsertOne(entry);

			var entry2 = new BsonDocument
			{
				{"aktienID", Guid.NewGuid().ToString() },
				{"name", "Super Firma von Mike"},
				{"course", 100},
				{"amount", 1000}
			};
			dbConnectionStocks._db.InsertOne(entry2);

			var entry3 = new BsonDocument
			{
				{"aktienID", Guid.NewGuid().ToString() },
				{"name", "Super Firma von Bugsdehude"},
				{"course", 50},
				{"amount", 1000}
			};
			dbConnectionStocks._db.InsertOne(entry3);

			var entry4 = new BsonDocument
			{
				{"aktienID", Guid.NewGuid().ToString() },
				{"name", "Super Firma von Blablub"},
				{"course", 70},
				{"amount", 1000}
			};
			dbConnectionStocks._db.InsertOne(entry4);

			var entry5 = new BsonDocument
			{
				{"aktienID", Guid.NewGuid().ToString() },
				{"name", "Super Firma von Cyka"},
				{"course", 10},
				{"amount", 100}
			};
			dbConnectionStocks._db.InsertOne(entry5);

			var entry6 = new BsonDocument
			{
				{"aktienID", Guid.NewGuid().ToString() },
				{"name", "Super Firma von Blyat"},
				{"course", 15},
				{"amount", 150}
			};
			dbConnectionStocks._db.InsertOne(entry6);

			var entry7 = new BsonDocument
			{
				{"aktienID", Guid.NewGuid().ToString() },
				{"name", "Super Firma von RUSH B"},
				{"course", 100},
				{"amount", 1000}
			};
			dbConnectionStocks._db.InsertOne(entry7);
		}
	}
}
