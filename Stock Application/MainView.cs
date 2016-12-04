using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MongoDB.Driver;
using System.ComponentModel;
using Telerik.WinControls.UI;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net;

namespace Stock_Application
{
    public partial class MainView : Telerik.WinControls.UI.RadForm
    {
        /// <summary>
        /// Client for all the http calls
        /// </summary>
        static HttpClient client = new HttpClient();

        //generates list of available URLs
        static List<string> lstURLs = StockURLS.getStockURLS();

        /// <summary>
        /// Temporary list for inserting new due orders when possible
        /// </summary>
        private List<DueOrder> insertDueList = new List<DueOrder>();

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
        /// </summary>
        public List<DueOrder> LstDueOrders = new List<DueOrder>();

        /// <summary>
        /// Represents the selected customer from Tab1
        /// </summary>
        public Customer SelectedCustomer = null;

        /// <summary>
        /// Worker which polls responses for the placed orders
        /// </summary>
        private BackgroundWorker responseWorker = new BackgroundWorker();

        /// <summary>
        /// Additional inits have to be done here
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            initializeRuntimeData();
            initializeTabControl();
            initializeThreadForResponses();
        }

        /// <summary>
        /// Starts worker for polling responses
        /// </summary>
        private void initializeThreadForResponses()
        {
            responseWorker.DoWork += pollResponses;
            responseWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Polls for responses
        /// </summary>
        private async void pollResponses(object sender, DoWorkEventArgs e)
        {
            //List to remove done answered orders
            List<DueOrder> listToRemove = new List<DueOrder>();
            while (true)
            {
                syncDueOrderList(listToRemove);

                foreach (var dueOrderItem in LstDueOrders)
                {
                    //reset client object if there is a problem with server and request can not be handled
                    client.CancelPendingRequests();
                    client.Dispose();
                    client = null;
                    client = new HttpClient();

                    client.BaseAddress = new Uri(dueOrderItem.hostURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //generate order object to Json serialize it afterwards
                    Check tmpOrder = new Check(dueOrderItem.placedOrder.orderID);

                    //generate JSON payload for POST
                    string tmpPayload = Newtonsoft.Json.JsonConvert.SerializeObject(tmpOrder);
                    StringContent httpContent = new StringContent(tmpPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    //execute check call to server
                    try
                    {
                        response = await client.PostAsync("boerse/check", httpContent);
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }

                    //extract status from json object
                    int status = int.MinValue;
                    status = await evaluateCheckResponse(response);

                    switch (status)
                    {
                        //successful
                        case 0:
                            addShareToCustomerDepot(response, dueOrderItem);

                            //if order was processed by stock delete it from dueOrderList
                            listToRemove.Add(dueOrderItem);
                            break;
                        //in progress
                        case 1:
                            break;
                        //denied
                        case 2:
                            Debug.Print("Order was denied!");
                            listToRemove.Add(dueOrderItem);
                            break;
                        //not enough goods
                        case 3:
                            Debug.Print("Not enough goods for order!");
                            listToRemove.Add(dueOrderItem);
                            break;
                        //wrong price
                        case 4:
                            Debug.Print("Wrong price for order!");
                            listToRemove.Add(dueOrderItem);
                            break;
                        default:
                            Debug.Print("Unknown error occured. Status not within the expected range!");
                            break;
                    }
                }
                Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// Adds a given share to the customers depot
        /// </summary>
        private async void addShareToCustomerDepot(HttpResponseMessage response, DueOrder dueOrderItem)
        {
            try
            {
                string orderGuid = await getPropertyFromResponse(response, "orderID");

                double price = double.Parse(await getPropertyFromResponse(response, "price"), System.Globalization.NumberStyles.Any);

                int amount = int.Parse(await getPropertyFromResponse(response, "amount"), System.Globalization.NumberStyles.Any);

                //Guid from the bought share
                string shareGuid = dueOrderItem.boughtShare.GUID;

                //host url required for getting full information about bought share
                string hostURL = dueOrderItem.hostURL;




            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        /// <summary>
        /// Gets the given property from a given http response msg
        /// </summary>
        /// <param name="response"></param>
        /// <param name="propName">Json property name which should get returned</param>
        /// <returns></returns>
        private async Task<string> getPropertyFromResponse(HttpResponseMessage response, string propName)
        {
            try
            {
                //reads http content -> string
                string jsonResponseString = await response.Content.ReadAsStringAsync();

                //parses string to json object
                JObject mystockpricelist = JObject.Parse(jsonResponseString);

                //returns searched value
                return mystockpricelist[propName].ToString();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets the json response from server and extracts the status of the order on server
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<int> evaluateCheckResponse(HttpResponseMessage response)
        {
            int status = int.MinValue;

            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    //getting json strin from response
                    string jsonResponseString = await response.Content.ReadAsStringAsync();

                    JObject mystockpricelist = null;

                    try
                    {
                        mystockpricelist = JObject.Parse(jsonResponseString);
                        //get the status for order status evaluation
                        if (mystockpricelist["state"] != null)
                        {
                            status = int.Parse(mystockpricelist["state"].ToString(), System.Globalization.NumberStyles.Any);
                        }
                        else if (mystockpricelist["status"] != null)
                        {
                            status = int.Parse(mystockpricelist["status"]?.ToString(), System.Globalization.NumberStyles.Any);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }
                }
            }
            return status;
        }

        /// <summary>
        /// Syncs the list of due orders with closed and new orders
        /// </summary>
        /// <param name="listToRemove"></param>
        private void syncDueOrderList(List<DueOrder> listToRemove)
        {
            foreach (var deleteItem in listToRemove)
            {
                removeDueOrder(deleteItem);
            }
            listToRemove.Clear();

            foreach (var item in insertDueList)
            {
                LstDueOrders.Add(item);
            }
            insertDueList.Clear();
        }

        /// <summary>
        /// Removes a due order from local list and DB
        /// </summary>
        /// <param name="itemToRemove"></param>
        private void removeDueOrder(DueOrder itemToRemove)
        {
            LstDueOrders.Remove(itemToRemove);

            var filter = Builders<BsonDocument>.Filter.Eq("OrderGuid", itemToRemove.placedOrder.orderID);
            db_connection.dueOrderTable.DeleteOne(filter);
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
        private List<DueOrder> loadDueOrdersFromDB()
        {
            List<DueOrder> tmpDueOrderList = new List<DueOrder>();

            try
            {
                var dueOrderDB = db_connection.dueOrderTable.Find(new BsonDocument()).ToList();

                //getting values from DB to internal format
                foreach (var dueOrderItem in dueOrderDB)
                {
                    tmpDueOrderList.Add(generateDueOrderObject(dueOrderItem));
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
        /// Gets values from mongo DB response and creates due order item from it 
        /// </summary>
        /// <param name="dueOrderItem"></param>
        /// <returns></returns>
        private DueOrder generateDueOrderObject(BsonDocument dueOrderItem)
        {
            try
            {
                Order tmpOrder = new Order(dueOrderItem.GetElement("OrderGuid").Value.ToString(), dueOrderItem.GetElement("AktienGuid").Value.ToString(), dueOrderItem.GetElement("BoughtAmount").Value.ToString(), dueOrderItem.GetElement("PriceLimit").Value.ToString(), dueOrderItem.GetElement("OrderHash").Value.ToString());

                Customer tmpCustomer = new Customer(dueOrderItem.GetElement("CustomerFirstname").Value.ToString(), dueOrderItem.GetElement("CustomerLastname").Value.ToString(), double.Parse(dueOrderItem.GetElement("CustomerEquity").Value.ToString(), System.Globalization.NumberStyles.Any), dueOrderItem.GetElement("CustomerGuid").Value.ToString(), "no data");

                string tmpHostURL = dueOrderItem.GetElement("HostURL").Value.ToString();

                Share tmpShare = new Share(dueOrderItem.GetElement("ShareGuid").Value.ToString(), dueOrderItem.GetElement("ShareName").Value.ToString(), dueOrderItem.GetElement("SharePrice").Value.ToString(), dueOrderItem.GetElement("ShareAmount").Value.ToString(), "no data");

                DueOrder tmpDueOrder = new DueOrder(tmpOrder, tmpCustomer, tmpHostURL, tmpShare);

                return tmpDueOrder;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return null;
            }
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
                //gets the available shares from the selected server
                getShares(lstURLs.ElementAt(radDropDownList1.SelectedIndex));

            }
            catch (Exception)
            {
                MessageBox.Show("Error receiving data from selected stock market. Reasons might be that the server is using https.", "Rest API error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gets available shares from server
        /// </summary>
        /// <param name="hostUri"></param>
        private async void getShares(string hostUri)
        {
            try
            {
                //reset client object if there is a problem with server and request can not be handled
                client.CancelPendingRequests();
                client.Dispose();
                client = null;
                client = new HttpClient();

                client.BaseAddress = new Uri(hostUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("boerse/listCourses");

                if (response.IsSuccessStatusCode)
                {
                    parseShareData(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Server is currently unreachable. Please retry later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //reset client object if there is a problem with server and request can not be handled
                client.CancelPendingRequests();
                client.Dispose();
                client = null;
                client = new HttpClient();
                return;
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

                //generate bindinglist for databinding on grd
                LstAvailableSharesOfMarket = new BindingList<Share>(stockList);

                //add new datasource to grd
                grdAvailableShares.DataSource = null;
                grdAvailableShares.DataSource = LstAvailableSharesOfMarket;
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

            string hostURL = string.Empty;
            //the indizes match so this should work
            try
            {
                hostURL = lstURLs.ElementAt(radDropDownList1.SelectedIndex);

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
                checkValidBuyOrderData(tmpCustomer, hostURL, tmpAmount, tmpShareToBuy, tmpMaxBuyValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending buy order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.Print(ex.Message);
                return;
            }

            //if all checks are done with success the order can be placed
            placeBuyOrder(tmpCustomer, hostURL, tmpAmount, tmpShareToBuy, tmpMaxBuyValue);
        }

        /// <summary>
        /// Generates the order and send it to the according server
        /// </summary>
        /// <param name="tmpCustomer"></param>
        /// <param name="tmpHost"></param>
        /// <param name="tmpPort"></param>
        /// <param name="tmpAmount"></param>
        /// <param name="tmpShareToBuy"></param>
        private async void placeBuyOrder(Customer tmpCustomer, string tmpHostURL, int tmpAmount, GridViewRowInfo tmpShareToBuy, double tmpMaxBuyValue)
        {
            //reset client object if there is a problem with server and request can not be handled
            client.CancelPendingRequests();
            client.Dispose();
            client = null;
            client = new HttpClient();

            client.BaseAddress = new Uri(tmpHostURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //generate order object to Json serialize it afterwards
            Order tmpOrder = new Order(Guid.NewGuid().ToString(), tmpShareToBuy.Cells[0].Value.ToString(), tmpAmount.ToString(), tmpMaxBuyValue.ToString(), string.Empty);

            //generate JSON payload for POST
            string tmpPayload = Newtonsoft.Json.JsonConvert.SerializeObject(tmpOrder);

            StringContent httpContent = new StringContent(tmpPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                //execute post to server
                response = await client.PostAsync("boerse/buy", httpContent);
            }
            catch (Exception)
            {
                MessageBox.Show("Server is not reachable. Please retry alter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (response?.StatusCode == HttpStatusCode.InternalServerError || response == null)
            {
                MessageBox.Show("Internal server error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Order was succusfully placed at server´s stock.", "Order placed.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //generate share object from datagrid entry
                Share tmpShare = new Share(tmpShareToBuy.Cells[0].Value.ToString(), tmpShareToBuy.Cells[1].Value.ToString(), tmpShareToBuy.Cells[2].Value.ToString(), tmpShareToBuy.Cells[3].Value.ToString(), tmpShareToBuy.Cells[4].Value.ToString());

                DueOrder tmpDueOrder = new DueOrder(tmpOrder, tmpCustomer, tmpHostURL, tmpShare);

                //if placing the order was successful the order has to be stored for the client application
                addDueOrderItem(tmpDueOrder);

                //negative because the customer now has less money available
                setCustomersEquityByGuid(tmpCustomer.GUID, (-1 * tmpAmount * tmpMaxBuyValue));
            }
            resetTab3Controls();
        }

        /// <summary>
        /// Sets the customer´s equity depending on the change value
        /// No check for valid values; has to be done before
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <param name="changeEquity">+ if customers gained money, - if he lost money</param>
        private void setCustomersEquityByGuid(string customerGuid, double changeEquity)
        {
            var tmpCustomer = from item in LstCustomers
                              where item.GUID.Equals(customerGuid)
                              select item;
            if (tmpCustomer.Count() > 0)
            {
                //get current equity of customer
                double tmpEquity = double.Parse((tmpCustomer.First() as Customer).Equity, System.Globalization.NumberStyles.Any);
                tmpEquity += changeEquity;

                (tmpCustomer.First() as Customer).Equity = tmpEquity.ToString();

                updateCustomerInDB(tmpCustomer.First() as Customer);

                //so the changed data will be visible on gui as well
                setLabelTexts(tmpCustomer.First() as Customer);
            }
        }

        /// <summary>
        /// Updates a customer´s equity by its guid
        /// </summary>
        private async void updateCustomerInDB(Customer tmpCustomer)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("GUID", tmpCustomer.GUID);
            var update = Builders<BsonDocument>.Update
                .Set("Equity", tmpCustomer.Equity);
            var result = await db_connection.customerTable.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// Adds a new due order to local list and DB
        /// </summary>
        private async void addDueOrderItem(DueOrder tmpTuple)
        {
            //add it to local list
            insertDueList.Add(tmpTuple);

            try
            {
                BsonDocument newEntry = new BsonDocument
            {
                //Order object
                {"OrderGuid", tmpTuple.placedOrder.orderID },
                {"AktienGuid", tmpTuple.placedOrder.aktienID },
                {"BoughtAmount", tmpTuple.placedOrder.amount },
                {"PriceLimit", tmpTuple.placedOrder.limit },
                {"OrderTimestamp", tmpTuple.placedOrder.timestamp },
                {"OrderHash", tmpTuple.placedOrder.hash },

                //Customer object
                {"CustomerGuid", tmpTuple.buyingCustomer.GUID},
                {"CustomerFirstname", tmpTuple.buyingCustomer.Firstname },
                {"CustomerLastname", tmpTuple.buyingCustomer.Lastname },
                {"CustomerEquity", tmpTuple.buyingCustomer.Equity },
                {"CustomerDepotGuid", tmpTuple.buyingCustomer.DepotGuid },

                //Host urls
                {"HostURL", tmpTuple.hostURL },

                //bought share
                {"ShareGuid", tmpTuple.boughtShare.GUID },
                {"ShareName", tmpTuple.boughtShare.Name },
                {"SharePrice", tmpTuple.boughtShare.Price },
                {"ShareAmount", tmpTuple.boughtShare.Amount }
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
        private void checkValidBuyOrderData(Customer tmpCustomer, string tmpHostURL, int tmpAmount, GridViewRowInfo tmpShareToBuy, double tmpMaxBuyValue)
        {
            if (tmpCustomer == null)
            {
                MessageBox.Show("No valid selected Customer found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new NullReferenceException();
            }

            if (tmpHostURL == string.Empty)
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
