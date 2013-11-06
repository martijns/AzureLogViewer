namespace AzureLogViewerGui.Scrapers
{
    partial class StorageAccountScraperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StorageAccountScraperForm));
            this.lbValidated = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectDir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbFailed = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCurrentPath = new System.Windows.Forms.Label();
            this.lblValidatingAccount = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbValidated
            // 
            this.lbValidated.FormattingEnabled = true;
            this.lbValidated.Location = new System.Drawing.Point(12, 169);
            this.lbValidated.Name = "lbValidated";
            this.lbValidated.Size = new System.Drawing.Size(197, 238);
            this.lbValidated.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(676, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // btnSelectDir
            // 
            this.btnSelectDir.Location = new System.Drawing.Point(16, 54);
            this.btnSelectDir.Name = "btnSelectDir";
            this.btnSelectDir.Size = new System.Drawing.Size(312, 23);
            this.btnSelectDir.TabIndex = 2;
            this.btnSelectDir.Text = "Select directory and start searching...";
            this.btnSelectDir.UseVisualStyleBackColor = true;
            this.btnSelectDir.Click += new System.EventHandler(this.HandleSelectDirClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Current path:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Validating account:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Validated and added";
            // 
            // lbFailed
            // 
            this.lbFailed.FormattingEnabled = true;
            this.lbFailed.Location = new System.Drawing.Point(215, 169);
            this.lbFailed.Name = "lbFailed";
            this.lbFailed.Size = new System.Drawing.Size(490, 238);
            this.lbFailed.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(405, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Failed with reason";
            // 
            // lblCurrentPath
            // 
            this.lblCurrentPath.AutoSize = true;
            this.lblCurrentPath.Location = new System.Drawing.Point(215, 93);
            this.lblCurrentPath.Name = "lblCurrentPath";
            this.lblCurrentPath.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentPath.TabIndex = 8;
            this.lblCurrentPath.Text = "label6";
            // 
            // lblValidatingAccount
            // 
            this.lblValidatingAccount.AutoSize = true;
            this.lblValidatingAccount.Location = new System.Drawing.Point(215, 120);
            this.lblValidatingAccount.Name = "lblValidatingAccount";
            this.lblValidatingAccount.Size = new System.Drawing.Size(35, 13);
            this.lblValidatingAccount.TabIndex = 9;
            this.lblValidatingAccount.Text = "label7";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(408, 54);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel...";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.HandleCancelClicked);
            // 
            // StorageAccountScraperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 419);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblValidatingAccount);
            this.Controls.Add(this.lblCurrentPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbFailed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbValidated);
            this.Name = "StorageAccountScraperForm";
            this.Text = "Storage account scraper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbValidated;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbFailed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCurrentPath;
        private System.Windows.Forms.Label lblValidatingAccount;
        private System.Windows.Forms.Button btnCancel;
    }
}