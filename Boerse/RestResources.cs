using Boerse;
using Grapevine.Interfaces.Server;
using Grapevine.Server.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDBConnection;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
[RestResource]
public class BoersenResource
{

	#region ### REST INTERFACES ###

	/// <summary>
	/// This interface should return an JSON array with all the stocks in our market.
	/// "aktienID": "GUID",
	/// "name":"String",
	/// "course": "double",
	/// "amount": "int"
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	[RestRoute(HttpMethod = Grapevine.Shared.HttpMethod.GET, PathInfo = "/boerse/listCourses")]
	public IHttpContext ListCourses(IHttpContext context)
	{
		string stockListAsJsonString = getStockListFromDb();
		context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.Ok, stockListAsJsonString);
		return context;
	}

	/// <summary>
	/// This interface should gets an JSON array with the order from a customer
	/// "orderID": "GUID",
	/// "aktienID": "GUID",
	/// "amount": int,
	/// “limit”: double // max Amount Customer wants to pay
	/// "timestamp": "int", // unix timestamp
	/// "hash": "String" // can be null
	///
	/// RESPONSE:
	/// 404: aktienID not found
	/// 200: OK, but check again with /check in x minutes
	/// 500: unknown error
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	[RestRoute(HttpMethod = Grapevine.Shared.HttpMethod.POST, PathInfo = "/boerse/buy")]
	public IHttpContext Buy(IHttpContext context)
	{
		var order = context.Request.QueryString["order"] ?? "what?";
		if(order == null)
		{
			context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.InternalServerError, "Oops, something went wrong!");
			return context;
		}
		Order orderObject = JsonConvert.DeserializeObject<Order>(order);
		bool isPresent = checkIfStockExists(orderObject);
		if(isPresent)
		{
			//Die Order ins Orderbuch eintragen.
			try
			{
				var dbOrders = dbConnectionOrders._db;
				MainOrder mainOrder = new MainOrder(orderObject, BUYORSELL.Buy);
				dbOrders.InsertOne(mainOrder.ToBsonDocument());		//Could be wrong, needs to be tested!
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
				context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.InternalServerError, "Oops, something went wrong!");
				return context;
			}

			context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.Ok, "Check again with /check");
			return context;
		}
		else
		{
			context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.NotFound, "aktienID not found");
			return context;
		}
	}

	/// <summary>
	/// This interface should gets an JSON array with the order from a customer
	/// "orderID": "GUID",
	/// "aktienID": "GUID",
	/// "amount": "int",
	/// “limit”: double		// min Amount Customer wants to have for the stock
	/// "timestamp": int,	// unix timestamp
	/// "hash": "String"	// can be null
	///
	/// RESPONSE:
	/// 200: OK, but check again with / check in x minutes
	/// 404: aktienID not found
	/// 500: unknown error
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	[RestRoute(HttpMethod = Grapevine.Shared.HttpMethod.POST, PathInfo = "/boerse/sell")]
	public IHttpContext Sell(IHttpContext context)
	{
		var order = context.Request.QueryString["order"] ?? "what?";
		if(order == null)
		{
			context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.InternalServerError, "Oops, something went wrong!");
			return context;
		}
		Order orderObject = JsonConvert.DeserializeObject<Order>(order);
		bool isPresent = checkIfStockExists(orderObject);
		if(isPresent)
		{
			//Die Order ins Orderbuch eintragen.
			try
			{
				var dbOrders = dbConnectionOrders._db;
				MainOrder mainOrder = new MainOrder(orderObject, BUYORSELL.Sell);
				dbOrders.InsertOne(mainOrder.ToBsonDocument());     //Could be wrong, needs to be tested!
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
				context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.InternalServerError, "Oops, something went wrong!");
				return context;
			}

			context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.Ok, "Check again with /check");
			return context;
		}
		else
		{
			context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.NotFound, "aktienID not found");
			return context;
		}
	}

	/// <summary>
	/// This interface should check the order from a customer
	/// "orderID": "GUID",
	///	"hash": "String" // can be null
	///
	/// RESPONSE:	The Response is a JSON array if OK
	/// 200 OK:
	///
	///	"orderID": "GUID",
	///	"price": double // price per good
	///	"amount":int
	///	"status":int // 0 if successfull, 1 in progress, 2 denied, 3 not enough goods, 4 wrong price
	///	"hash": "String" // can be null
	///
	///	404: orderID not found
	///	500: unknown internal error
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	[RestRoute(HttpMethod = Grapevine.Shared.HttpMethod.POST, PathInfo = "/boerse/check")]
	public IHttpContext Check(IHttpContext context)
	{
		var order = context.Request.QueryString["order"] ?? "what?";
		if(order == null)
		{
			context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.InternalServerError, "Oops, something went wrong!");
			return context;
		}
		CheckOrder orderObject = JsonConvert.DeserializeObject<CheckOrder>(order);
		try
		{
			var dbOrders = dbConnectionOrders._db;
			var entrys = dbOrders.Find(new BsonDocument()).ToList();
			foreach(var item in entrys)
			{
				MainOrder deserializedOrder = BsonSerializer.Deserialize<MainOrder>(item);
				if(orderObject.orderID.Equals(deserializedOrder.receivedOrder.orderID))
				{
					//Find the current price of the stock
					var dbStocks = dbConnectionStocks._db;
					double course = double.Parse(item.GetElement("course").Value.ToString(), System.Globalization.NumberStyles.Any);

					//Create a Response Order
					ResponseOrder responseOrder = new ResponseOrder();
					responseOrder.orderID = deserializedOrder.receivedOrder.orderID;
					responseOrder.statusOfOrder = deserializedOrder.statusOfOrder;
					responseOrder.amount = deserializedOrder.receivedOrder.amount;
					responseOrder.price = course;
					string response = JsonConvert.SerializeObject(responseOrder);
					context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.Ok, response);
					break;
				}
				else
					context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.NotFound, "OrderID not found!");
			}
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex);
		}
		return context;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	[RestRoute]
	public IHttpContext Info(IHttpContext context)
	{
		context.Response.SendResponse("This is an info! :)");
		return context;
	}
	#endregion

	#region ### HELPER FUNCTIONS ###

	/// <summary>
	/// This functions checks if the given aktienID is present in our market.
	/// </summary>
	/// <param name="order"></param>
	/// <returns></returns>
	private bool checkIfStockExists(Order order)
	{
		try
		{
			var dbStocks = dbConnectionStocks._db;
			var entrys = dbStocks.Find(new BsonDocument()).ToList();
			foreach(var item in entrys)
			{
				Guid aktienID = new Guid(item.GetElement("aktienID").Value.ToString());
				if(order.aktienID.Equals(aktienID))
					return true;
			}
			return false;
		}
		catch(Exception ex)
		{
			Debug.Write(ex);
			return false;
		}
	}

	/// <summary>
	/// This function is used to get a list from all current stocks from the market.
	/// </summary>
	/// <returns></returns>
	private string getStockListFromDb()
	{
		string result = string.Empty;
		Stock stock = new Stock();
		var dbStocks = dbConnectionStocks._db;
		try
		{
			var entrys = dbStocks.Find(new BsonDocument()).ToList();
			foreach(var item in entrys)
			{
				stock.aktienID = new Guid(item.GetElement("aktienID").Value.ToString());
				stock.name = item.GetElement("name").Value.ToString();
				stock.course = double.Parse(item.GetElement("course").Value.ToString(), System.Globalization.NumberStyles.Any);
				stock.amount = Int32.Parse(item.GetElement("amount").Value.ToString(), System.Globalization.NumberStyles.Any);
				result += JsonConvert.SerializeObject(stock);
			}
		}
		catch(Exception ex)
		{
			Debug.WriteLine(ex);
		}
		return result;
	}
	#endregion
}
