using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Application
{
    /// <summary>
    /// Objects of this class hold the information about the stocks of a certain customer
    /// </summary>
    public class Depot
    {
        /// <summary>
        /// Contains the shares which belong to a depot
        /// </summary>
        public BindingList<Share> lstShares = new BindingList<Share>();

        /// <summary>
        /// ID of a depot
        /// </summary>
        public string prpDepotGuid = string.Empty;

        /// <summary>
        /// Depot ID as string for databinding to gridview
        /// </summary>
        public string DepotGuid
        {
            get { return prpDepotGuid; }
            set { prpDepotGuid = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tmpGuid">if string empty new guid should be generated</param>
        public Depot(string tmpGuid)
        {
            if (tmpGuid != string.Empty)
            {
                DepotGuid = tmpGuid;
            }
            else
            {
                DepotGuid = Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// Adds a share to a depot (local list and DB storage)
        /// </summary>
        /// <param name="tmpShare"></param>
        public void AddShareToDepot(Share tmpShare)
        {
            //add share to depot´s local list of shares
            lstShares.Add(tmpShare);

            //add share to servers table for shares
            addShareTuple(tmpShare);
        }

        /// <summary>
        /// Adds a given share to the server´s DB for shares
        /// </summary>
        /// <param name="tmpShare"></param>
        private async void addShareTuple(Share tmpShare)
        {
            try
            {
                BsonDocument newEntry = new BsonDocument
            {
                {"GUID", tmpShare.GUID},
                {"Name", tmpShare.Name},
                {"Price", tmpShare.Price },
                {"Amount", tmpShare.Amount },
                {"DepotGUID", tmpShare.DepotGUID }
            };
                await db_connection.shareTable.InsertOneAsync(newEntry);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                throw new InvalidOperationException();
            }
        }





    }
}
