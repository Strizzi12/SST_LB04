namespace Stock_Application
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            this.office2013DarkTheme1 = new Telerik.WinControls.Themes.Office2013DarkTheme();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grdGridCustomers = new Telerik.WinControls.UI.RadGridView();
            this.btnCreateCustomer = new Telerik.WinControls.UI.RadButton();
            this.txtLastName = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtAmount = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtFirstName = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.lblEquity = new System.Windows.Forms.Label();
            this.lblLastname = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblGuid = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grdCustomerDepot = new Telerik.WinControls.UI.RadGridView();
            this.btnSellShares = new Telerik.WinControls.UI.RadButton();
            this.btnBuyShares = new Telerik.WinControls.UI.RadButton();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGridCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGridCustomers.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCreateCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerDepot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerDepot.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSellShares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBuyShares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(13, 13);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1213, 939);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grdGridCustomers);
            this.tabPage1.Controls.Add(this.btnCreateCustomer);
            this.tabPage1.Controls.Add(this.txtLastName);
            this.tabPage1.Controls.Add(this.txtAmount);
            this.tabPage1.Controls.Add(this.txtFirstName);
            this.tabPage1.Controls.Add(this.radLabel3);
            this.tabPage1.Controls.Add(this.radLabel2);
            this.tabPage1.Controls.Add(this.radLabel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1205, 902);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Create Customer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grdGridCustomers
            // 
            this.grdGridCustomers.AutoScroll = true;
            this.grdGridCustomers.AutoSizeRows = true;
            this.grdGridCustomers.Location = new System.Drawing.Point(8, 146);
            this.grdGridCustomers.Margin = new System.Windows.Forms.Padding(5);
            // 
            // 
            // 
            this.grdGridCustomers.MasterTemplate.AllowAddNewRow = false;
            this.grdGridCustomers.MasterTemplate.AllowCellContextMenu = false;
            this.grdGridCustomers.MasterTemplate.AllowColumnChooser = false;
            this.grdGridCustomers.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.grdGridCustomers.MasterTemplate.AllowColumnReorder = false;
            this.grdGridCustomers.MasterTemplate.AllowDragToGroup = false;
            this.grdGridCustomers.MasterTemplate.AllowEditRow = false;
            this.grdGridCustomers.MasterTemplate.AllowRowHeaderContextMenu = false;
            this.grdGridCustomers.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.grdGridCustomers.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdGridCustomers.Name = "grdGridCustomers";
            this.grdGridCustomers.Size = new System.Drawing.Size(1189, 748);
            this.grdGridCustomers.TabIndex = 13;
            this.grdGridCustomers.Text = "radGridView1";
            this.grdGridCustomers.ThemeName = "Office2013Dark";
            this.grdGridCustomers.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.grdGridCustomers_RowsChanging);
            this.grdGridCustomers.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdGridCustomers_CellDoubleClick);
            // 
            // btnCreateCustomer
            // 
            this.btnCreateCustomer.Location = new System.Drawing.Point(899, 43);
            this.btnCreateCustomer.Margin = new System.Windows.Forms.Padding(5);
            this.btnCreateCustomer.Name = "btnCreateCustomer";
            this.btnCreateCustomer.Size = new System.Drawing.Size(247, 52);
            this.btnCreateCustomer.TabIndex = 12;
            this.btnCreateCustomer.Text = "Create new customer";
            this.btnCreateCustomer.ThemeName = "Office2013Dark";
            this.btnCreateCustomer.Click += new System.EventHandler(this.btnCreateCustomer_Click);
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(190, 53);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(5);
            this.txtLastName.Mask = "(^$)|(^([^\\-!#\\$%&\\(\\)\\*,\\./:;\\?@\\[\\\\\\]_\\{\\|\\}¨ˇ“”€\\+<=>§°\\d\\s¤®™©]| )+$)";
            this.txtLastName.MaskType = Telerik.WinControls.UI.MaskType.Regex;
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(617, 37);
            this.txtLastName.TabIndex = 9;
            this.txtLastName.TabStop = false;
            this.txtLastName.ThemeName = "Office2013Dark";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(190, 100);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(5);
            this.txtAmount.Mask = "C";
            this.txtAmount.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(617, 37);
            this.txtAmount.TabIndex = 11;
            this.txtAmount.TabStop = false;
            this.txtAmount.Text = "0,00 €";
            this.txtAmount.ThemeName = "Office2013Dark";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(190, 11);
            this.txtFirstName.Margin = new System.Windows.Forms.Padding(5);
            this.txtFirstName.Mask = "(^$)|(^([^\\-!#\\$%&\\(\\)\\*,\\./:;\\?@\\[\\\\\\]_\\{\\|\\}¨ˇ“”€\\+<=>§°\\d\\s¤®™©]| )+$)";
            this.txtFirstName.MaskType = Telerik.WinControls.UI.MaskType.Regex;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(617, 37);
            this.txtFirstName.TabIndex = 7;
            this.txtFirstName.TabStop = false;
            this.txtFirstName.ThemeName = "Office2013Dark";
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(8, 100);
            this.radLabel3.Margin = new System.Windows.Forms.Padding(5);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(81, 36);
            this.radLabel3.TabIndex = 10;
            this.radLabel3.Text = "Equity:";
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(8, 54);
            this.radLabel2.Margin = new System.Windows.Forms.Padding(5);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(116, 36);
            this.radLabel2.TabIndex = 8;
            this.radLabel2.Text = "Lastname:";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(8, 8);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(5);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(119, 36);
            this.radLabel1.TabIndex = 6;
            this.radLabel1.Text = "Firstname:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnBuyShares);
            this.tabPage2.Controls.Add(this.btnSellShares);
            this.tabPage2.Controls.Add(this.grdCustomerDepot);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.lblEquity);
            this.tabPage2.Controls.Add(this.lblLastname);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.lblGuid);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1205, 902);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Customer´s Depot";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.label5.Location = new System.Drawing.Point(1010, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 32);
            this.label5.TabIndex = 7;
            // 
            // lblEquity
            // 
            this.lblEquity.AutoSize = true;
            this.lblEquity.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblEquity.Location = new System.Drawing.Point(1009, 77);
            this.lblEquity.Name = "lblEquity";
            this.lblEquity.Size = new System.Drawing.Size(0, 32);
            this.lblEquity.TabIndex = 7;
            // 
            // lblLastname
            // 
            this.lblLastname.AutoSize = true;
            this.lblLastname.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblLastname.Location = new System.Drawing.Point(697, 77);
            this.lblLastname.Name = "lblLastname";
            this.lblLastname.Size = new System.Drawing.Size(0, 32);
            this.lblLastname.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1009, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "Equity:";
            // 
            // lblGuid
            // 
            this.lblGuid.AutoSize = true;
            this.lblGuid.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblGuid.Location = new System.Drawing.Point(30, 77);
            this.lblGuid.Name = "lblGuid";
            this.lblGuid.Size = new System.Drawing.Size(0, 32);
            this.lblGuid.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1008, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 32);
            this.label4.TabIndex = 3;
            this.label4.Text = "Equity:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(697, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lastname:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "GUID:";
            // 
            // grdCustomerDepot
            // 
            this.grdCustomerDepot.AutoScroll = true;
            this.grdCustomerDepot.AutoSizeRows = true;
            this.grdCustomerDepot.Location = new System.Drawing.Point(8, 134);
            this.grdCustomerDepot.Margin = new System.Windows.Forms.Padding(5);
            // 
            // 
            // 
            this.grdCustomerDepot.MasterTemplate.AllowAddNewRow = false;
            this.grdCustomerDepot.MasterTemplate.AllowCellContextMenu = false;
            this.grdCustomerDepot.MasterTemplate.AllowColumnChooser = false;
            this.grdCustomerDepot.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.grdCustomerDepot.MasterTemplate.AllowColumnReorder = false;
            this.grdCustomerDepot.MasterTemplate.AllowDragToGroup = false;
            this.grdCustomerDepot.MasterTemplate.AllowEditRow = false;
            this.grdCustomerDepot.MasterTemplate.AllowRowHeaderContextMenu = false;
            this.grdCustomerDepot.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.grdCustomerDepot.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.grdCustomerDepot.Name = "grdCustomerDepot";
            this.grdCustomerDepot.Size = new System.Drawing.Size(1189, 694);
            this.grdCustomerDepot.TabIndex = 14;
            this.grdCustomerDepot.Text = "radGridView1";
            this.grdCustomerDepot.ThemeName = "Office2013Dark";
            // 
            // btnSellShares
            // 
            this.btnSellShares.Location = new System.Drawing.Point(693, 838);
            this.btnSellShares.Margin = new System.Windows.Forms.Padding(5);
            this.btnSellShares.Name = "btnSellShares";
            this.btnSellShares.Size = new System.Drawing.Size(247, 52);
            this.btnSellShares.TabIndex = 15;
            this.btnSellShares.Text = "Sell selected shares";
            this.btnSellShares.ThemeName = "Office2013Dark";
            // 
            // btnBuyShares
            // 
            this.btnBuyShares.Location = new System.Drawing.Point(950, 838);
            this.btnBuyShares.Margin = new System.Windows.Forms.Padding(5);
            this.btnBuyShares.Name = "btnBuyShares";
            this.btnBuyShares.Size = new System.Drawing.Size(247, 52);
            this.btnBuyShares.TabIndex = 13;
            this.btnBuyShares.Text = "Buy shares";
            this.btnBuyShares.ThemeName = "Office2013Dark";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 950);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainView";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Stock Application";
            this.ThemeName = "Office2013Dark";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGridCustomers.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGridCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCreateCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerDepot.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerDepot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSellShares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBuyShares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Office2013DarkTheme office2013DarkTheme1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private Telerik.WinControls.UI.RadGridView grdGridCustomers;
        private Telerik.WinControls.UI.RadButton btnCreateCustomer;
        private Telerik.WinControls.UI.RadMaskedEditBox txtLastName;
        private Telerik.WinControls.UI.RadMaskedEditBox txtAmount;
        private Telerik.WinControls.UI.RadMaskedEditBox txtFirstName;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblGuid;
        private System.Windows.Forms.Label lblEquity;
        private System.Windows.Forms.Label lblLastname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadGridView grdCustomerDepot;
        private Telerik.WinControls.UI.RadButton btnBuyShares;
        private Telerik.WinControls.UI.RadButton btnSellShares;
    }
}