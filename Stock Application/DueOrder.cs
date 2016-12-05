using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Application
{
    /// <summary>
    /// DueOrder holds every information required to insert a bought share correctly to the customers depot
    /// </summary>
    public class DueOrder
    {
        /// <summary>
        /// Constructor with full information
        /// </summary>
        public DueOrder(Order tmpPlacedOrder, Customer tmpBuyingCustomer, string tmpHostURL, Share tmpBoughtShare, bool tmpBuyOrSell)
        {
            placedOrder = tmpPlacedOrder;
            buyingCustomer = tmpBuyingCustomer;
            hostURL = tmpHostURL;
            boughtShare = tmpBoughtShare;
            BuyOrSell = tmpBuyOrSell;
        }

        /// <summary>
        /// Indicates if the DueOrder was a Buy- or a Sell-Order
        /// false = Buy, true = sell
        /// </summary>
        public bool BuyOrSell = false;

        /// <summary>
        /// Placed order 
        /// </summary>
        public Order placedOrder = null;

        /// <summary>
        /// Customer which placed the order
        /// </summary>
        public Customer buyingCustomer = null;

        /// <summary>
        /// URL from the server the order was placed at
        /// </summary>
        public string hostURL = string.Empty;

        /// <summary>
        /// Share which should get bought
        /// </summary>
        public Share boughtShare = null;

    }
}
