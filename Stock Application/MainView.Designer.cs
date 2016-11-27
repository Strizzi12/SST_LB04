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
            this.office2013DarkTheme1 = new Telerik.WinControls.Themes.Office2013DarkTheme();
            this.radCollapsPanel = new Telerik.WinControls.UI.RadCollapsiblePanel();
            this.grdGridCustomers = new Telerik.WinControls.UI.RadGridView();
            this.btnCreateCustomer = new Telerik.WinControls.UI.RadButton();
            this.txtLastName = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtAmount = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtFirstName = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radCollapsPanel)).BeginInit();
            this.radCollapsPanel.PanelContainer.SuspendLayout();
            this.radCollapsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGridCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGridCustomers.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCreateCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radCollapsPanel
            // 
            this.radCollapsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radCollapsPanel.AnimationType = Telerik.WinControls.UI.CollapsiblePanelAnimationType.Slide;
            this.radCollapsPanel.ExpandDirection = Telerik.WinControls.UI.RadDirection.Right;
            this.radCollapsPanel.HeaderText = "Create Customer";
            this.radCollapsPanel.IsExpanded = false;
            this.radCollapsPanel.Location = new System.Drawing.Point(13, 13);
            this.radCollapsPanel.Name = "radCollapsPanel";
            this.radCollapsPanel.OwnerBoundsCache = new System.Drawing.Rectangle(13, 13, 430, 981);
            // 
            // radCollapsPanel.PanelContainer
            // 
            this.radCollapsPanel.PanelContainer.Controls.Add(this.grdGridCustomers);
            this.radCollapsPanel.PanelContainer.Controls.Add(this.btnCreateCustomer);
            this.radCollapsPanel.PanelContainer.Controls.Add(this.txtLastName);
            this.radCollapsPanel.PanelContainer.Controls.Add(this.txtAmount);
            this.radCollapsPanel.PanelContainer.Controls.Add(this.txtFirstName);
            this.radCollapsPanel.PanelContainer.Controls.Add(this.radLabel3);
            this.radCollapsPanel.PanelContainer.Controls.Add(this.radLabel2);
            this.radCollapsPanel.PanelContainer.Controls.Add(this.radLabel1);
            this.radCollapsPanel.PanelContainer.Size = new System.Drawing.Size(0, 0);
            this.radCollapsPanel.Size = new System.Drawing.Size(42, 981);
            this.radCollapsPanel.TabIndex = 0;
            this.radCollapsPanel.Text = "radCollapsPanel";
            this.radCollapsPanel.ThemeName = "Office2013Dark";
            ((Telerik.WinControls.UI.RadCollapsiblePanelElement)(this.radCollapsPanel.GetChildAt(0))).ExpandDirection = Telerik.WinControls.UI.RadDirection.Right;
            ((Telerik.WinControls.UI.RadCollapsiblePanelElement)(this.radCollapsPanel.GetChildAt(0))).IsExpanded = false;
            ((Telerik.WinControls.UI.RadCollapsiblePanelElement)(this.radCollapsPanel.GetChildAt(0))).AnimationType = Telerik.WinControls.UI.CollapsiblePanelAnimationType.Slide;
            // 
            // grdGridCustomers
            // 
            this.grdGridCustomers.AutoScroll = true;
            this.grdGridCustomers.AutoSizeRows = true;
            this.grdGridCustomers.Location = new System.Drawing.Point(18, 185);
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
            this.grdGridCustomers.Size = new System.Drawing.Size(552, 772);
            this.grdGridCustomers.TabIndex = 5;
            this.grdGridCustomers.Text = "radGridView1";
            this.grdGridCustomers.ThemeName = "Office2013Dark";
            this.grdGridCustomers.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.grdGridCustomers_RowsChanging);
            // 
            // btnCreateCustomer
            // 
            this.btnCreateCustomer.Location = new System.Drawing.Point(405, 135);
            this.btnCreateCustomer.Name = "btnCreateCustomer";
            this.btnCreateCustomer.Size = new System.Drawing.Size(165, 36);
            this.btnCreateCustomer.TabIndex = 4;
            this.btnCreateCustomer.Text = "Create";
            this.btnCreateCustomer.ThemeName = "Office2013Dark";
            this.btnCreateCustomer.Click += new System.EventHandler(this.btnCreateCustomer_Click);
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(157, 59);
            this.txtLastName.Mask = "(^$)|(^([^\\-!#\\$%&\\(\\)\\*,\\./:;\\?@\\[\\\\\\]_\\{\\|\\}¨ˇ“”€\\+<=>§°\\d\\s¤®™©]| )+$)";
            this.txtLastName.MaskType = Telerik.WinControls.UI.MaskType.Regex;
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(413, 32);
            this.txtLastName.TabIndex = 2;
            this.txtLastName.TabStop = false;
            this.txtLastName.ThemeName = "Office2013Dark";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(157, 97);
            this.txtAmount.Mask = "C";
            this.txtAmount.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(413, 32);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.TabStop = false;
            this.txtAmount.Text = "0,00 €";
            this.txtAmount.ThemeName = "Office2013Dark";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(157, 20);
            this.txtFirstName.Mask = "(^$)|(^([^\\-!#\\$%&\\(\\)\\*,\\./:;\\?@\\[\\\\\\]_\\{\\|\\}¨ˇ“”€\\+<=>§°\\d\\s¤®™©]| )+$)";
            this.txtFirstName.MaskType = Telerik.WinControls.UI.MaskType.Regex;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(413, 32);
            this.txtFirstName.TabIndex = 1;
            this.txtFirstName.TabStop = false;
            this.txtFirstName.ThemeName = "Office2013Dark";
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(18, 97);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(70, 31);
            this.radLabel3.TabIndex = 2;
            this.radLabel3.Text = "Equity:";
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(18, 60);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(100, 31);
            this.radLabel2.TabIndex = 1;
            this.radLabel2.Text = "Lastname:";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(18, 24);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(102, 31);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Firstname:";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1592, 977);
            this.Controls.Add(this.radCollapsPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainView";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Stock Application";
            this.ThemeName = "Office2013Dark";
            this.radCollapsPanel.PanelContainer.ResumeLayout(false);
            this.radCollapsPanel.PanelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radCollapsPanel)).EndInit();
            this.radCollapsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdGridCustomers.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGridCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCreateCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Office2013DarkTheme office2013DarkTheme1;
        private Telerik.WinControls.UI.RadCollapsiblePanel radCollapsPanel;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadMaskedEditBox txtLastName;
        private Telerik.WinControls.UI.RadMaskedEditBox txtAmount;
        private Telerik.WinControls.UI.RadMaskedEditBox txtFirstName;
        private Telerik.WinControls.UI.RadButton btnCreateCustomer;
        private Telerik.WinControls.UI.RadGridView grdGridCustomers;
    }
}