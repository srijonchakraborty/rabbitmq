
namespace RabbitMQ
{
    partial class Consumer
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
            this.btnGET = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtBxDataShow = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGET
            // 
            this.btnGET.Location = new System.Drawing.Point(12, 259);
            this.btnGET.Name = "btnGET";
            this.btnGET.Size = new System.Drawing.Size(75, 23);
            this.btnGET.TabIndex = 4;
            this.btnGET.Text = "Get";
            this.btnGET.UseVisualStyleBackColor = true;
            this.btnGET.Click += new System.EventHandler(this.btnGET_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(592, 259);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtBxDataShow
            // 
            this.txtBxDataShow.AcceptsReturn = true;
            this.txtBxDataShow.Location = new System.Drawing.Point(11, 41);
            this.txtBxDataShow.Multiline = true;
            this.txtBxDataShow.Name = "txtBxDataShow";
            this.txtBxDataShow.ReadOnly = true;
            this.txtBxDataShow.Size = new System.Drawing.Size(656, 212);
            this.txtBxDataShow.TabIndex = 6;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(11, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // Consumer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 294);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtBxDataShow);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGET);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Consumer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consumer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGET;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtBxDataShow;
        private System.Windows.Forms.Button btnConnect;
    }
}

