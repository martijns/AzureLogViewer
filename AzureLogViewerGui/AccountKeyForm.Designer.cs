namespace AzureLogViewerGui
{
    partial class AccountKeyForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.accountName = new System.Windows.Forms.TextBox();
            this.accountKey = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.validateButton = new System.Windows.Forms.Button();
            this.validationStatus = new System.Windows.Forms.Label();
            this.devStorageCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Account key:";
            // 
            // accountName
            // 
            this.accountName.Location = new System.Drawing.Point(92, 11);
            this.accountName.Margin = new System.Windows.Forms.Padding(2);
            this.accountName.Name = "accountName";
            this.accountName.Size = new System.Drawing.Size(128, 20);
            this.accountName.TabIndex = 2;
            // 
            // accountKey
            // 
            this.accountKey.Location = new System.Drawing.Point(92, 42);
            this.accountKey.Margin = new System.Windows.Forms.Padding(2);
            this.accountKey.Name = "accountKey";
            this.accountKey.Size = new System.Drawing.Size(409, 20);
            this.accountKey.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(92, 112);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(128, 19);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.HandleSaveClicked);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(295, 112);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(128, 19);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.HandleCancelClicked);
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(92, 78);
            this.validateButton.Margin = new System.Windows.Forms.Padding(2);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(128, 19);
            this.validateButton.TabIndex = 6;
            this.validateButton.Text = "Validate connection";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.HandleValidateClicked);
            // 
            // validationStatus
            // 
            this.validationStatus.AutoSize = true;
            this.validationStatus.Location = new System.Drawing.Point(239, 80);
            this.validationStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.validationStatus.Name = "validationStatus";
            this.validationStatus.Size = new System.Drawing.Size(0, 13);
            this.validationStatus.TabIndex = 7;
            // 
            // devStorageCheckbox
            // 
            this.devStorageCheckbox.AutoSize = true;
            this.devStorageCheckbox.Location = new System.Drawing.Point(258, 13);
            this.devStorageCheckbox.Name = "devStorageCheckbox";
            this.devStorageCheckbox.Size = new System.Drawing.Size(113, 17);
            this.devStorageCheckbox.TabIndex = 8;
            this.devStorageCheckbox.Text = "Developer storage";
            this.devStorageCheckbox.UseVisualStyleBackColor = true;
            this.devStorageCheckbox.CheckedChanged += new System.EventHandler(this.HandleDeveloperStorageCheckedChanged);
            // 
            // AccountKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 141);
            this.Controls.Add(this.devStorageCheckbox);
            this.Controls.Add(this.validationStatus);
            this.Controls.Add(this.validateButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.accountKey);
            this.Controls.Add(this.accountName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AccountKeyForm";
            this.Text = "Account key";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox accountName;
        private System.Windows.Forms.TextBox accountKey;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.Label validationStatus;
        private System.Windows.Forms.CheckBox devStorageCheckbox;
    }
}