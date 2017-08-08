namespace DatabaseConnectionAdmin
{
    partial class NewTransaction
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
            this.Sum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.To = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.From = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Apply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Sum
            // 
            this.Sum.Location = new System.Drawing.Point(48, 58);
            this.Sum.Name = "Sum";
            this.Sum.Size = new System.Drawing.Size(296, 20);
            this.Sum.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sum";
            // 
            // To
            // 
            this.To.Location = new System.Drawing.Point(48, 32);
            this.To.Name = "To";
            this.To.Size = new System.Drawing.Size(296, 20);
            this.To.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "To";
            // 
            // From
            // 
            this.From.Location = new System.Drawing.Point(48, 6);
            this.From.Name = "From";
            this.From.Size = new System.Drawing.Size(296, 20);
            this.From.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "From";
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(268, 85);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(75, 23);
            this.Apply.TabIndex = 9;
            this.Apply.Text = "OK";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // NewTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 116);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.Sum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.To);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.From);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewTransaction";
            this.Text = "NewTransaction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Sum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox To;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox From;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Apply;
    }
}