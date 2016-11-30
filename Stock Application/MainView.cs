using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MongoDB.Driver;
using System.ComponentModel;
using Telerik.WinControls.UI;
using System.Linq;
using Grapevine.Client;
using Newtonsoft.Json.Linq;
using Grapevine.Shared;

namespace Stock_Application
{
    public partial class MainView : Telerik.WinControls.UI.RadForm
    {

        /// <summary>
        /// List of customer objects which are available during runtime
        /// This list is loaded and saved at server´s DB for persistency
        /// </summary>
        public BindingList<Customer> LstCustomers = new BindingList<Customer>();

        /// <summary>
        /// Holds the available shares of a certain stock
        /// Depot guid is not assigned because shares are also not assigned to a depot until they get bought
        /// </summary>
        public BindingList<Share> LstAvailableSharesOfMarket = new BindingList<Share>();

        /// <summary>
        /// Holds the outstanding orders
        /// Item1 is the orderID, Item2 is the CustomerID which placed the order
        /// </summary>
        public List<Tuple<string, string>> LstDueOrders = new List<Tuple<string, string>>();

        /// <summary>
        /// Represents the selected customer from Tab1
        /// </summary>
        public Customer SelectedCustomer = null;

        /// <summary>
        /// Additional inits have to be done here
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            initializeRuntimeData();
            initializeTabControl();
        }

        /// <summary>
        /// Winfroms TabControl has a strange behaviour for disabling Tabpages so it is programatically
        /// </summary>
        private void initializeTabControl()
        {
            for (int i = 1; i < tabControl.TabPages.Count; i++)
            {
                (tabControl.TabPages[i] as Control).Enabled = false;
            }
        }

        /// <summary>
        /// Sets up the runtime data and configuration
        /// </summary>
        private void initializeRuntimeData()
        {
            LstCustomers = loadCustomersFromDB();
            LstDueOrders = loadDueOrdersFromDB();
            setDataBindings();
        }

        /// <summary>
        /// Initial load of due orders from DB
        /// </summary>
        private List<Tuple<string, string>> loadDueOrdersFromDB()
        {
            List<Tuple<string, string>> tmpDueOrderList = new List<Tuple<string, string>>();

            try
            {
                var dueOrderDB = db_connection.dueOrderTable.Find(new BsonDocument()).ToList();

                //getting values from DB to internal format
                foreach (var dueOrderItem in dueOrderDB)
                {
                    tmpDueOrderList.Add(new Tuple<string, string>(dueOrderItem.GetElement("OrderGuid").Value.ToString(), dueOrderItem.GetElement("CustomerGuid").Value.ToString()));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load due orders from server´s DB.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return tmpDueOrderList;
        }

        /// <summary>
        /// Sets inital databindings for telerik controls
        /// </summary>
        private void setDataBindings()
        {
            grdGridCustomers.DataSource = LstCustomers;
        }

        /// <summary>
        /// Loads customer information from server´s DB and stores it in runtime List<Customer>
        /// </summary>
        /// <returns></returns>
        private BindingList<Customer> loadCustomersFromDB()
        {
            BindingList<Customer> tmpCusList = new BindingList<Customer>();

            try
            {
                var customerDB = db_connection.customerTable.Find(new BsonDocument()).ToList();

                //getting values from DB to internal format
                foreach (var customerItem in customerDB)
                {
                    //create customer object with loaded data
                    Customer tmpCus = new Customer(customerItem.GetElement("Firstname").Value.ToString(), customerItem.GetElement("Lastname").Value.ToString(), Double.Parse(customerItem.GetElement("Equity").Value.ToString(), System.Globalization.NumberStyles.Any), customerItem.GetElement("GUID").Value.ToString(), customerItem.GetElement("DepotGuid").Value.ToString());

                    tmpCus = loadSharesForCustomer(tmpCus);

                    tmpCusList.Add(tmpCus);
                }
                return tmpCusList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer data from database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.Print(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Loads the shares for a given customer from the DB
        /// </summary>
        private Customer loadSharesForCustomer(Customer tmpCus)
        {
            //find shares which are associated with the customers depot id
            var filter = Builders<BsonDocument>.Filter.Eq("DepotGUID", tmpCus.DepotGuid);
            var shareDB = db_connection.shareTable.Find(filter).ToList();

            foreach (var shareItem in shareDB)
            {
                Share tmpShare = new Share(shareItem.GetElement("GUID").Value.ToString(), shareItem.GetElement("Name").Value.ToString(), shareItem.GetElement("Price").Value.ToString(), shareItem.GetElement("Amount").Value.ToString(), shareItem.GetElement("DepotGUID").Value.ToString());

                tmpCus.Depot.lstShares.Add(tmpShare);
            }
            return tmpCus;
        }

        /// <summary>
        /// Creates a customer object with the given user input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                //simple check implementation -> should be checked for telerik controls error provider active but no idea how to do this in winforms (Telerik wpf controls better for this)
                //manual check with regex could be implemented here for validating the input in greater detail but no need for this lab excercise
                if (Double.Parse(txtAmount.Value.ToString(), System.Globalization.NumberStyles.Any) /*removes space and currency sign*/ >= 0 && txtFirstName.Value.ToString() != string.Empty && txtLastName.Value.ToString() != string.Empty)
                {
                    //creating customer object with user input
                    Customer tmpCus = new Customer(txtFirstName.Value.ToString(), txtLastName.Value.ToString(), Double.Parse(txtAmount.Value.ToString(), System.Globalization.NumberStyles.Any), string.Empty, string.Empty);

                    //adding customer object to DB
                    try
                    {
                        addCustomerTuple(tmpCus);
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show("Unable to insert tuple into DB.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    LstCustomers.Add(tmpCus);

                    //Reset the text of inputfields
                    txtAmount.ResetText();
                    txtFirstName.ResetText();
                    txtLastName.ResetText();
                }
                else
                {
                    MessageBox.Show("Input for user creation does not fit the requirements.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        /// <summary>
        /// Adds a given customer object as tuple to DB
        /// </summary>
        private async void addCustomerTuple(Customer tmpCusObject)
        {
            try
            {
                BsonDocument newEntry = new BsonDocument
            {
                {"GUID", tmpCusObject.GUID},
                {"Firstname", tmpCusObject.Firstname},
                {"Lastname", tmpCusObject.Lastname },
                {"Equity", tmpCusObject.Equity },
                {"DepotGuid", tmpCusObject.Depot.DepotGuid }
            };
                await db_connection.customerTable.InsertOneAsync(newEntry);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Deletes a customer from the database when the entry of datagrid is delted with DELETE key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdGridCustomers_RowsChanging(object sender, Telerik.WinControls.UI.GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                DialogResult dialogResult = MessageBox.Show("Do you really want to delete this entry?", "Customer deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        //get ID for tupel to delete from database as well
                        string tmpGuid = grdGridCustomers.CurrentRow.Cells[0].Value.ToString();

                        //delete tupel from database
                        var filter = Builders<BsonDocument>.Filter.Eq("GUID", tmpGuid);
                        db_connection.customerTable.DeleteOne(filter);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to delete entry from database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.Print(ex.Message);
                    }
                }
                else
                {
                    //Cancel current event and follow ups to prevent deletion
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Retrieves clicked rows information and passes it to next tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdGridCustomers_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                //rowindex -1 is the header of control 
                if (e.RowIndex != -1 && grdGridCustomers.CurrentCell != null && grdGridCustomers.CurrentRow != null)
                {
                    GridViewRowInfo clickedRow = grdGridCustomers.SelectedRows[0];
                    string tmpGuid = clickedRow.Cells[0].Value.ToString();

                    Customer tmpCustomer = getCustomerByGUID(tmpGuid);
                    if (tmpCustomer != null)
                    {
                        initializeSecondTab(tmpCustomer);

                        setSelectedCustomer(tmpCustomer);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        /// <summary>
        /// Sets the selected Customer and enables tabs
        /// </summary>
        /// <param name="tmpCustomer"></param>
        private void setSelectedCustomer(Customer tmpCustomer)
        {
            SelectedCustomer = tmpCustomer;

            //enable tabs after a customer was selected (before that it would make no sense to go to one of the other Tabs)
            enableTabs();
        }

        /// <summary>
        /// Enables other Tabs of Tabcontrol
        /// </summary>
        private void enableTabs()
        {
            foreach (var item in tabControl.TabPages)
            {
                (item as Control).Enabled = true;
            }
        }

        /// <summary>
        /// Gets Customer object from list by its guid
        /// </summary>
        /// <param name="tmpGuid"></param>
        private Customer getCustomerByGUID(string tmpGuid)
        {
            //linq query for the customer with given Guid
            var tmpCustomer = from item in LstCustomers
                              where item.GUID.Equals(tmpGuid)
                              select item;

            if (tmpCustomer.Count() > 0)
            {
                return tmpCustomer.First();
            }
            return null;
        }

        /// <summary>
        /// Makes initialization steps for the second tab and displays it to the user
        /// </summary>
        private void initializeSecondTab(Customer tmpCustomer)
        {
            //clear 2nd tab´s datagrid
            grdCustomerDepot.DataSource = null;
            grdCustomerDepot.MasterTemplate.Refresh();

            //display correct data on tab 2
            setLabelTexts(tmpCustomer);
            setDepotGridDataSource(tmpCustomer);

            //displaying tab 2
            tabControl.SelectedTab = tabPage2;
        }

        /// <summary>
        /// Sets the lbls texts according to the double clicked customer from first tab
        /// </summary>
        /// <param name="tmpCustomer"></param>
        private void setLabelTexts(Customer tmpCustomer)
        {
            lblGuid.Text = tmpCustomer.GUID;
            lblLastname.Text = tmpCustomer.Lastname;
            lblEquity.Text = tmpCustomer.Equity;
        }

        /// <summary>
        /// Sets the datasource of the Depot datagrid
        /// </summary>
        /// <param name="tmpCustomer"></param>
        private void setDepotGridDataSource(Customer tmpCustomer)
        {
            //manual add of a share for test purposes
            //tmpCustomer.Depot.AddShareToDepot(new Share("1","mytestshare","1000", "1", tmpCustomer.DepotGuid));

            grdCustomerDepot.DataSource = tmpCustomer.Depot.lstShares;
        }

        /// <summary>
        /// Changes user´s view to Tab3 for buying shares of a selected market
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuyShares_Click(object sender, EventArgs e)
        {
            //displaying tab 3
            tabControl.SelectedTab = tabPage3;
        }

        /// <summary>
        /// Gets data from stocks API (available stocks to buy)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                RestClient client = new RestClient();
                //gets list of urls and ports of the stocks
                StockURLS STK = new StockURLS();

                //the indizes match so this should work
                //via host url and port its not possible to get this call to work (http://ec2-35-156-47-142.eu-central-1.compute.amazonaws.com:8080/awsServer) cannot get the /awsServer into the call
                client.Host = STK.URLPORTS[radDropDownList1.SelectedIndex].Item1;
                client.Port = STK.URLPORTS[radDropDownList1.SelectedIndex].Item2;

                //request for shares which can be bought currently at a certain stock
                RestRequest request = new RestRequest("/boerse/listCourses");
                //https is not supported by this method -> gives error on "Niemansland" stock because this is using https only 
                request.HttpMethod = Grapevine.Shared.HttpMethod.GET;

                //get the Json from server and get the payload from it
                IRestResponse response = client.Execute(request);
                string restContent = response.GetContent();

                parseShareData(restContent);

                //reset and assign new datasource of grid to display new data
                grdAvailableShares.DataSource = null;
                grdAvailableShares.DataSource = LstAvailableSharesOfMarket;
            }
            catch (Exception)
            {
                MessageBox.Show("Error receiving data from selected stock market. Reasons might be that the server is using https.", "Rest API error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Parses the json data form server and generates a local list with this data
        /// Shares will be generated with no depot id as string: String.Empty to avoid declaring another class this shares
        /// </summary>
        /// <param name="restContent"></param>
        private void parseShareData(string restContent)
        {
            //reset list of shares
            LstAvailableSharesOfMarket = new BindingList<Share>();

            try
            {
                //this algorithmn has to be used bc not every group managed to use the correct order of the properties in the json string
                JArray myStockPriceList = JArray.Parse(restContent);
                var stockList = new List<Share>();
                stockList = myStockPriceList.Select(x => new Share((string)x["aktienID"], (string)x["name"], (string)x["course"], (string)x["amount"], "no depot assigned")).ToList();

                LstAvailableSharesOfMarket = new BindingList<Share>(stockList);

                //algorithm relies on correct order of properties and cant be used
                /*JToken token = JToken.Parse(restContent);
                foreach (var item in token)
                {
                    Share tmpShare = new Share(item.ElementAt(0).Values().First().ToString(), item.ElementAt(1).Values().First().ToString(), item.ElementAt(2).Values().First().ToString(), item.ElementAt(3).Values().First().ToString(), );
                    LstAvailableSharesOfMarket.Add(tmpShare);
                }*/
            }
            catch (Exception)
            {
                MessageBox.Show("Json format of stock has invalid format. Application is unable to parse Json.", "Parser Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Collects required information of selected user, selected stock and the amount of shares and sends a buy request to stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendBuyOrder_Click(object sender, EventArgs e)
        {
            //selected user
            Customer tmpCustomer = SelectedCustomer;

            //selected stock
            StockURLS STK = new StockURLS();


            string tmpHost = string.Empty;
            int tmpPort = int.MinValue;
            //the indizes match so this should work
            try
            {
                tmpHost = STK.URLPORTS[radDropDownList1.SelectedIndex].Item1;
                tmpPort = STK.URLPORTS[radDropDownList1.SelectedIndex].Item2;
            }
            catch (Exception)
            {
                MessageBox.Show("You have to select a stock before you can playce an order.", "Invalid operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //amount of shares to buy
            int tmpAmount = int.Parse(txtShareAmount.Value.ToString(), System.Globalization.NumberStyles.Any);

            //selected share to buy (first should be fine cause of multiselect = false)
            var tmpShareToBuy = grdAvailableShares.SelectedRows?.First();

            //max buy value per share
            double tmpMaxBuyValue = double.Parse(txtMaxBuyValue.Value.ToString(), System.Globalization.NumberStyles.Any);

            try
            {
                checkValidBuyOrderData(tmpCustomer, tmpHost, tmpPort, tmpAmount, tmpShareToBuy, tmpMaxBuyValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending buy order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.Print(ex.Message);
                return;
            }

            //if all checks are done with success the order can be placed
            placeBuyOrder(tmpCustomer, tmpHost, tmpPort, tmpAmount, tmpShareToBuy, tmpMaxBuyValue);
        }

        /// <summary>
        /// Generates the order and send it to the according server
        /// </summary>
        /// <param name="tmpCustomer"></param>
        /// <param name="tmpHost"></param>
        /// <param name="tmpPort"></param>
        /// <param name="tmpAmount"></param>
        /// <param name="tmpShareToBuy"></param>
        private void placeBuyOrder(Customer tmpCustomer, string tmpHost, int tmpPort, int tmpAmount, GridViewRowInfo tmpShareToBuy, double tmpMaxBuyValue)
        {
            RestClient client = new RestClient();

            client.Host = tmpHost;
            client.Port = tmpPort;

            //request for placing an order
            RestRequest request = new RestRequest("/boerse/buy");
            //https is not supported by this method -> gives error on "Niemansland" stock because this is using https only 
            request.HttpMethod = Grapevine.Shared.HttpMethod.POST;
            //set payloads format to json
            request.ContentType = ContentType.JSON;

            //generate order object to Json serialize it afterwards
            Order tmpOrder = new Order(Guid.NewGuid().ToString(), tmpShareToBuy.Cells[0].Value.ToString(), tmpAmount.ToString(), tmpMaxBuyValue.ToString(), string.Empty);

            //generate JSON payload for POST
            string tmpPayload = Newtonsoft.Json.JsonConvert.SerializeObject(tmpOrder);
            request.Payload = tmpPayload;

            //send the post request to server
            var respond = client.Execute(request);

            if (respond.StatusCode == HttpStatusCode.InternalServerError)
            {
                MessageBox.Show("Internal server error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Order was succusfully placed at server´s stock.", "Order placed.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //if placing the order was successful the order has to be stored for the client application
                addDueOrderItem(new Tuple<string, string>(tmpOrder.orderID.ToString(), tmpCustomer.GUID));
            }
            resetTab3Controls();
        }

        /// <summary>
        /// Adds a new due order to local list and DB
        /// </summary>
        private async void addDueOrderItem(Tuple<string, string> tmpTuple)
        {
            //add it to local list
            LstDueOrders.Add(tmpTuple);

            try
            {
                BsonDocument newEntry = new BsonDocument
            {
                {"OrderGuid", tmpTuple.Item1 },
                {"CustomerGuid", tmpTuple.Item2 }
            };
                await db_connection.dueOrderTable.InsertOneAsync(newEntry);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Resets the controls of Tab3 when the user tried to place an order
        /// </summary>
        private void resetTab3Controls()
        {
            txtMaxBuyValue.Value = 0;
            txtShareAmount.Value = 0;
        }

        /// <summary>
        /// Checks for valid data of the buy order
        /// </summary>
        /// <param name="tmpCustomer"></param>
        /// <param name="tmpHost"></param>
        /// <param name="tmpPort"></param>
        /// <param name="tmpAmount"></param>
        /// <returns></returns>
        private void checkValidBuyOrderData(Customer tmpCustomer, string tmpHost, int tmpPort, int tmpAmount, GridViewRowInfo tmpShareToBuy, double tmpMaxBuyValue)
        {
            if (tmpCustomer == null)
            {
                MessageBox.Show("No valid selected Customer found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new NullReferenceException();
            }

            if (tmpHost == string.Empty || tmpPort < 0)
            {
                throw new NullReferenceException();
            }

            if (tmpAmount <= 0)
            {
                MessageBox.Show("Invalid amount of shares for order!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new InvalidOperationException();
            }

            int cntAvailableShare = int.MaxValue;
            try
            {
                cntAvailableShare = int.Parse(tmpShareToBuy.Cells[3].Value.ToString(), System.Globalization.NumberStyles.Any);
            }
            catch (Exception)
            {
                throw;
            }

            if (tmpMaxBuyValue <= 0)
            {
                MessageBox.Show("Invalid max. buy value per share!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }

            //see if customer has enough money to order the shares
            if (double.Parse(tmpCustomer.Equity, System.Globalization.NumberStyles.Any) < (tmpMaxBuyValue * tmpAmount))
            {
                MessageBox.Show("Customer has not enough money to place this order!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }

            if (cntAvailableShare <= 0)
            {
                MessageBox.Show("No shares available at the moment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }
        }




    }
}
