namespace DatabaseConnectionAdmin
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.UsersGrid = new System.Windows.Forms.DataGridView();
            this.usernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moneyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lastTaskDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lastTaskDataSet = new DatabaseConnectionAdmin.LastTaskDataSet();
            this.usersTableAdapter = new DatabaseConnectionAdmin.LastTaskDataSetTableAdapters.UsersTableAdapter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ShowAsSelected = new System.Windows.Forms.Button();
            this.RemoveUser = new System.Windows.Forms.Button();
            this.AddUserButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.AddTransaction = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.debitUserDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creditUserDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transactionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.transactionsTableAdapter = new DatabaseConnectionAdmin.LastTaskDataSetTableAdapters.TransactionsTableAdapter();
            this.RefreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lastTaskDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lastTaskDataSet)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // UsersGrid
            // 
            this.UsersGrid.AllowUserToAddRows = false;
            this.UsersGrid.AllowUserToDeleteRows = false;
            this.UsersGrid.AutoGenerateColumns = false;
            this.UsersGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.UsersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.usernameDataGridViewTextBoxColumn,
            this.passwordDataGridViewTextBoxColumn,
            this.moneyDataGridViewTextBoxColumn});
            this.UsersGrid.DataSource = this.usersBindingSource;
            this.UsersGrid.Location = new System.Drawing.Point(6, 6);
            this.UsersGrid.MultiSelect = false;
            this.UsersGrid.Name = "UsersGrid";
            this.UsersGrid.ReadOnly = true;
            this.UsersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.UsersGrid.Size = new System.Drawing.Size(449, 498);
            this.UsersGrid.TabIndex = 0;
            // 
            // usernameDataGridViewTextBoxColumn
            // 
            this.usernameDataGridViewTextBoxColumn.DataPropertyName = "username";
            this.usernameDataGridViewTextBoxColumn.HeaderText = "username";
            this.usernameDataGridViewTextBoxColumn.Name = "usernameDataGridViewTextBoxColumn";
            this.usernameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // passwordDataGridViewTextBoxColumn
            // 
            this.passwordDataGridViewTextBoxColumn.DataPropertyName = "password";
            this.passwordDataGridViewTextBoxColumn.HeaderText = "password";
            this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
            this.passwordDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // moneyDataGridViewTextBoxColumn
            // 
            this.moneyDataGridViewTextBoxColumn.DataPropertyName = "#money";
            this.moneyDataGridViewTextBoxColumn.HeaderText = "money";
            this.moneyDataGridViewTextBoxColumn.Name = "moneyDataGridViewTextBoxColumn";
            this.moneyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usersBindingSource
            // 
            this.usersBindingSource.DataMember = "Users";
            this.usersBindingSource.DataSource = this.lastTaskDataSetBindingSource;
            // 
            // lastTaskDataSetBindingSource
            // 
            this.lastTaskDataSetBindingSource.DataSource = this.lastTaskDataSet;
            this.lastTaskDataSetBindingSource.Position = 0;
            // 
            // lastTaskDataSet
            // 
            this.lastTaskDataSet.DataSetName = "LastTaskDataSet";
            this.lastTaskDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // usersTableAdapter
            // 
            this.usersTableAdapter.ClearBeforeFill = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(751, 536);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.ShowAsSelected);
            this.tabPage1.Controls.Add(this.RemoveUser);
            this.tabPage1.Controls.Add(this.AddUserButton);
            this.tabPage1.Controls.Add(this.UsersGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(743, 510);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Users";
            // 
            // ShowAsSelected
            // 
            this.ShowAsSelected.Location = new System.Drawing.Point(571, 36);
            this.ShowAsSelected.Name = "ShowAsSelected";
            this.ShowAsSelected.Size = new System.Drawing.Size(144, 23);
            this.ShowAsSelected.TabIndex = 1;
            this.ShowAsSelected.Text = "Show as selected user";
            this.ShowAsSelected.UseVisualStyleBackColor = true;
            this.ShowAsSelected.Click += new System.EventHandler(this.ShowAsSelected_Click);
            // 
            // RemoveUser
            // 
            this.RemoveUser.Location = new System.Drawing.Point(571, 7);
            this.RemoveUser.Name = "RemoveUser";
            this.RemoveUser.Size = new System.Drawing.Size(144, 23);
            this.RemoveUser.TabIndex = 1;
            this.RemoveUser.Text = "Remove selected user";
            this.RemoveUser.UseVisualStyleBackColor = true;
            this.RemoveUser.Click += new System.EventHandler(this.RemoveUser_Click);
            // 
            // AddUserButton
            // 
            this.AddUserButton.Location = new System.Drawing.Point(462, 7);
            this.AddUserButton.Name = "AddUserButton";
            this.AddUserButton.Size = new System.Drawing.Size(103, 23);
            this.AddUserButton.TabIndex = 1;
            this.AddUserButton.Text = "Add user";
            this.AddUserButton.UseVisualStyleBackColor = true;
            this.AddUserButton.Click += new System.EventHandler(this.AddUserButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.AddTransaction);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(743, 510);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Transactions";
            // 
            // AddTransaction
            // 
            this.AddTransaction.Location = new System.Drawing.Point(540, 7);
            this.AddTransaction.Name = "AddTransaction";
            this.AddTransaction.Size = new System.Drawing.Size(111, 23);
            this.AddTransaction.TabIndex = 1;
            this.AddTransaction.Text = "Add transaction";
            this.AddTransaction.UseVisualStyleBackColor = true;
            this.AddTransaction.Click += new System.EventHandler(this.AddTransaction_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.debitUserDataGridViewTextBoxColumn,
            this.creditUserDataGridViewTextBoxColumn,
            this.sumDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.transactionsBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(7, 7);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(526, 500);
            this.dataGridView2.TabIndex = 0;
            // 
            // debitUserDataGridViewTextBoxColumn
            // 
            this.debitUserDataGridViewTextBoxColumn.DataPropertyName = "debitUser";
            this.debitUserDataGridViewTextBoxColumn.HeaderText = "debitUser";
            this.debitUserDataGridViewTextBoxColumn.Name = "debitUserDataGridViewTextBoxColumn";
            this.debitUserDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // creditUserDataGridViewTextBoxColumn
            // 
            this.creditUserDataGridViewTextBoxColumn.DataPropertyName = "creditUser";
            this.creditUserDataGridViewTextBoxColumn.HeaderText = "creditUser";
            this.creditUserDataGridViewTextBoxColumn.Name = "creditUserDataGridViewTextBoxColumn";
            this.creditUserDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sumDataGridViewTextBoxColumn
            // 
            this.sumDataGridViewTextBoxColumn.DataPropertyName = "#sum";
            this.sumDataGridViewTextBoxColumn.HeaderText = "sum";
            this.sumDataGridViewTextBoxColumn.Name = "sumDataGridViewTextBoxColumn";
            this.sumDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // transactionsBindingSource
            // 
            this.transactionsBindingSource.DataMember = "Transactions";
            this.transactionsBindingSource.DataSource = this.lastTaskDataSetBindingSource;
            // 
            // transactionsTableAdapter
            // 
            this.transactionsTableAdapter.ClearBeforeFill = true;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(13, 552);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 2;
            this.RefreshButton.Text = "Refersh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 580);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Admin form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UsersGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lastTaskDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lastTaskDataSet)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView UsersGrid;
        private System.Windows.Forms.BindingSource lastTaskDataSetBindingSource;
        private LastTaskDataSet lastTaskDataSet;
        private System.Windows.Forms.BindingSource usersBindingSource;
        private LastTaskDataSetTableAdapters.UsersTableAdapter usersTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn moneyDataGridViewTextBoxColumn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingSource transactionsBindingSource;
        private LastTaskDataSetTableAdapters.TransactionsTableAdapter transactionsTableAdapter;
        private System.Windows.Forms.Button RemoveUser;
        private System.Windows.Forms.Button AddUserButton;
        private System.Windows.Forms.Button AddTransaction;
        private System.Windows.Forms.DataGridViewTextBoxColumn debitUserDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creditUserDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sumDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button ShowAsSelected;
        private System.Windows.Forms.Button RefreshButton;
    }
}

