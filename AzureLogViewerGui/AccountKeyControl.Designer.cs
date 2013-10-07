namespace AzureLogViewerGui
{
    partial class AccountKeyControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.accountNameText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.accountKeyText = new System.Windows.Forms.TextBox();
            this.validateButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.accountNameText);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.accountKeyText);
            this.flowLayoutPanel1.Controls.Add(this.validateButton);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(641, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Accountname:";
            // 
            // accountNameText
            // 
            this.accountNameText.Location = new System.Drawing.Point(111, 3);
            this.accountNameText.Name = "accountNameText";
            this.accountNameText.Size = new System.Drawing.Size(141, 22);
            this.accountNameText.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Key:";
            // 
            // accountKeyText
            // 
            this.accountKeyText.Location = new System.Drawing.Point(304, 3);
            this.accountKeyText.Name = "accountKeyText";
            this.accountKeyText.Size = new System.Drawing.Size(172, 22);
            this.accountKeyText.TabIndex = 3;
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(482, 3);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(75, 23);
            this.validateButton.TabIndex = 4;
            this.validateButton.Text = "Validate";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.HandleValidateClicked);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(600, 3);
            this.button2.Margin = new System.Windows.Forms.Padding(40, 3, 3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.HandlePlusClicked);
            // 
            // AccountKeyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "AccountKeyControl";
            this.Size = new System.Drawing.Size(641, 30);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox accountNameText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox accountKeyText;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.Button button2;
    }
}
