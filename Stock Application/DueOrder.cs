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
        public DueOrder(Order tmpPlacedOrder, Customer tmpBuyingCustomer, string tmpHostURL, Share tmpBoughtShare)
        {
            placedOrder = tmpPlacedOrder;
            buyingCustomer = tmpBuyingCustomer;
            hostURL = tmpHostURL;
            boughtShare = tmpBoughtShare;
        }

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
