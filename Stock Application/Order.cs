using System;

namespace Stock_Application
{
    /// <summary>
    /// Represents a buy order for shares from a stock
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Guid of the order
        /// </summary>
        public Guid orderID;

        /// <summary>
        /// Guid of the share which user wants to buy
        /// </summary>
        public Guid aktienID;

        /// <summary>
        /// Cnt how much shares should be bought
        /// </summary>
        public int amount = int.MinValue;

        /// <summary>
        /// Max price per share which the user wants to pay
        /// </summary>
        public double limit = double.MinValue;

        /// <summary>
        /// unix timestamp of the share
        /// </summary>
        public int timestamp = int.MinValue;

        /// <summary>
        /// hash for additional excercise
        /// </summary>
        public string hash = string.Empty;

        /// <summary>
        /// Overloaded constructor incl. conversions for the types and generating unix timestamp
        /// </summary>
        /// <param name="tmpOrderID"></param>
        /// <param name="tmpAktienID"></param>
        /// <param name="tmpAmount"></param>
        /// <param name="tmpLimit"></param>
        /// <param name="tmpTimestamp"></param>
        /// <param name="tmpHash"></param>
        public Order(string tmpOrderID, string tmpAktienID, string tmpAmount, string tmpLimit, string tmpHash)
        {
            orderID = Guid.Parse(tmpOrderID);
            aktienID = Guid.Parse(tmpAktienID);
            amount = int.Parse(tmpAmount, System.Globalization.NumberStyles.Any);
            limit = double.Parse(tmpLimit, System.Globalization.NumberStyles.Any);
            //http://stackoverflow.com/questions/17632584/how-to-get-the-unix-timestamp-in-c-sharp
            timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            hash = tmpHash;
        }
    }
}
