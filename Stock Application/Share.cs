namespace Stock_Application
{
    /// <summary>
    /// Represents a share of the market which can be bought and sold by a trade on the stock market
    /// Shares are stored in the trader´s depot
    /// </summary>
    public class Share
    {
        /// <summary>
        /// Guid of a share used for identification
        /// </summary>
        private string prpGuid = string.Empty;

        /// <summary>
        /// Guid as string for binding to datagridview
        /// </summary>
        public string GUID
        {
            get { return prpGuid; }
            set { prpGuid = value; }
        }

        /// <summary>
        /// Name of the company to which the shares belong
        /// </summary>
        private string prpName = string.Empty;

        /// <summary>
        /// Name as string for databinding to datagridview
        /// </summary>
        public string Name
        {
            get { return prpName; }
            set { prpName = value; }
        }

        /// <summary>
        /// Current price of a share
        /// </summary>
        public float prpPrice = float.MinValue;

        /// <summary>
        /// Current price of a share as string for databinding to the datagridview
        /// </summary>
        public string Price
        {
            get { return prpPrice.ToString(); }
            set { prpPrice = float.Parse(value, System.Globalization.NumberStyles.Any); }
        }

        /// <summary>
        /// Current count of shares
        /// </summary>
        public int prpAmount = int.MinValue;

        /// <summary>
        /// Current count of shares as string used for databinding to the datagridview
        /// </summary>
        public string Amount
        {
            get { return prpAmount.ToString(); }
            set { prpAmount = int.Parse(value, System.Globalization.NumberStyles.Any); }
        }

        /// <summary>
        /// Depot which the shares belong to 
        /// </summary>
        public string prpDeputGuid = string.Empty;

        /// <summary>
        /// Current depot as string used for databinding to the datagridview
        /// </summary>
        public string DepotGUID
        {
            get { return prpDeputGuid; }
            set { prpDeputGuid = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tmpGuid"></param>
        /// <param name="tmpName"></param>
        /// <param name="tmpPrice"></param>
        /// <param name="tmpAmount"></param>
        public Share(string tmpGuid, string tmpName, string tmpPrice, string tmpAmount, string tmpDepotGuid)
        {
            this.GUID = tmpGuid;
            this.Name = tmpName;
            this.Price = tmpPrice;
            this.Amount = tmpAmount;
            this.DepotGUID = tmpDepotGuid;
        }





    }
}
