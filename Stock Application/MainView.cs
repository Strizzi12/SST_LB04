﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MongoDB.Driver;
using System.Threading;
using System.ComponentModel;
using Telerik.WinControls.UI;
using System.Linq;

namespace Stock_Application
{
    public partial class MainView : Telerik.WinControls.UI.RadForm
    {

        /// <summary>
        /// List of customer objects which are available during runtime
        /// This list is loaded and saved at server´s DB for persistency
        /// </summary>
        public BindingList<Customer> LstCustomers = new BindingList<Customer>();

        public MainView()
        {
            InitializeComponent();
            initializeRuntimeData();
        }

        /// <summary>
        /// Sets up the runtime data and configuration
        /// </summary>
        private void initializeRuntimeData()
        {
            LstCustomers = loadCustomersFromDB();
            setDataBindings();
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
                var db = db_connection.customerTable.Find(new BsonDocument()).ToList();

                //getting values from DB to internal format
                foreach (var item in db)
                {
                    Customer tmpCus = new Customer(item.GetElement("Firstname").Value.ToString(), item.GetElement("Lastname").Value.ToString(), Double.Parse(item.GetElement("Equity").Value.ToString(), System.Globalization.NumberStyles.Any), item.GetElement("GUID").Value.ToString());
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
                    Customer tmpCus = new Customer(txtFirstName.Value.ToString(), txtLastName.Value.ToString(), Double.Parse(txtAmount.Value.ToString(), System.Globalization.NumberStyles.Any), string.Empty);

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
                {"Equity", tmpCusObject.Equity }
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
        /// Gets Customer object from list by its guid
        /// </summary>
        /// <param name="tmpGuid"></param>
        private Customer getCustomerByGUID(string tmpGuid)
        {
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
            setLabelTexts(tmpCustomer);

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





    }
}
