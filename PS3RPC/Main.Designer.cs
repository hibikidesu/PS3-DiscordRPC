namespace PS3RPC
{
    partial class Main
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
            this.ccapiButton = new System.Windows.Forms.RadioButton();
            this.tmapiButton = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.connectionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ccapiButton
            // 
            this.ccapiButton.AutoSize = true;
            this.ccapiButton.Location = new System.Drawing.Point(12, 12);
            this.ccapiButton.Name = "ccapiButton";
            this.ccapiButton.Size = new System.Drawing.Size(56, 17);
            this.ccapiButton.TabIndex = 0;
            this.ccapiButton.TabStop = true;
            this.ccapiButton.Text = "CCAPI";
            this.ccapiButton.UseVisualStyleBackColor = true;
            this.ccapiButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // tmapiButton
            // 
            this.tmapiButton.AutoSize = true;
            this.tmapiButton.Location = new System.Drawing.Point(13, 36);
            this.tmapiButton.Name = "tmapiButton";
            this.tmapiButton.Size = new System.Drawing.Size(58, 17);
            this.tmapiButton.TabIndex = 1;
            this.tmapiButton.TabStop = true;
            this.tmapiButton.Text = "TMAPI";
            this.tmapiButton.UseVisualStyleBackColor = true;
            this.tmapiButton.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // connectionLabel
            // 
            this.connectionLabel.AutoSize = true;
            this.connectionLabel.Location = new System.Drawing.Point(165, 9);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(73, 13);
            this.connectionLabel.TabIndex = 3;
            this.connectionLabel.Text = "Disconnected";
            this.connectionLabel.Click += new System.EventHandler(this.connectionLabel_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 164);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tmapiButton);
            this.Controls.Add(this.ccapiButton);
            this.Name = "Main";
            this.Text = "PS3 Discord RPC";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton ccapiButton;
        private System.Windows.Forms.RadioButton tmapiButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label connectionLabel;
    }
}