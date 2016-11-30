using System;

namespace Stock_Application
{
    /// <summary>
    /// Represents one customer which can buy and sell stocks at the market
    /// Instances of customers are stored in a table on the server´s DB
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Required for unique identification (critical)
        /// </summary>
        private string prpGUID = string.Empty;

        /// <summary>
        /// Required for databinding
        /// </summary>
        public string GUID
        {
            get { return prpGUID; }
            set { prpGUID = value; }
        }

        /// <summary>
        /// Some general data about customer (not critical)
        /// </summary>
        private string prpFirstname = String.Empty;

        /// <summary>
        /// Required for databinding
        /// </summary>
        public string Firstname
        {
            get { return prpFirstname; }
            set { prpFirstname = value; }
        }

        /// <summary>
        /// Some general data about customer (not critical)
        /// </summary>
        private string prpLastname = String.Empty;

        /// <summary>
        /// Required for databinding
        /// </summary>
        public string Lastname
        {
            get { return prpLastname; }
            set { prpLastname = value; }
        }

        /// <summary>
        /// Capital with which the customer can buy stocks at the market
        /// </summary>
        private double prpEquity = double.MinValue;

        /// <summary>
        /// Required for databinding
        /// </summary>
        public string Equity
        {
            get { return prpEquity.ToString(); }
            set { prpEquity = Double.Parse(value, System.Globalization.NumberStyles.Any); }
        }

        /// <summary>
        /// Contains all shares which belong to a customer
        /// </summary>
        public Depot Depot = null;

        /// <summary>
        /// Not nice - only for display purposes
        /// </summary>
        public string DepotGuid
        {
            get { return Depot?.DepotGuid; }
        }

        /// <summary>
        /// Constructor for setting the required information initially
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="equity"></param>
        /// <param name="guid">if string.empty, a new guid will be generated</param>
        /// <param name="depotGuid"> if string.empty, a new depot guid will be generated</param>
        public Customer(string firstname, string lastname, double equity, string guid, string depotGuid)
        {
            this.prpFirstname = firstname;
            this.prpLastname = lastname;
            this.prpEquity = equity;

            if (depotGuid != string.Empty)
            {
                this.Depot = new Depot(depotGuid);

                //TODO: load data for depot according from server
            }
            else
            {
                this.Depot = new Depot(string.Empty);
            }
            
            //storing GUID as string for simplicity
            if (guid != string.Empty)
            {
                prpGUID = guid;
            }
            else
            {
                prpGUID = Guid.NewGuid().ToString();
            }
        }




    }
}
