namespace SDAM_Assignment
{
    partial class BuyerDash
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.btnCart = new System.Windows.Forms.Button();
            this.BtnShop = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(28, 33);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(28, 68);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "label2";
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(28, 111);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(35, 13);
            this.lblContact.TabIndex = 2;
            this.lblContact.Text = "label3";
            // 
            // btnCart
            // 
            this.btnCart.Location = new System.Drawing.Point(394, 18);
            this.btnCart.Name = "btnCart";
            this.btnCart.Size = new System.Drawing.Size(105, 42);
            this.btnCart.TabIndex = 3;
            this.btnCart.Text = "Cart";
            this.btnCart.UseVisualStyleBackColor = true;
            this.btnCart.Click += new System.EventHandler(this.btnCart_Click);
            // 
            // BtnShop
            // 
            this.BtnShop.Location = new System.Drawing.Point(394, 68);
            this.BtnShop.Name = "BtnShop";
            this.BtnShop.Size = new System.Drawing.Size(105, 42);
            this.BtnShop.TabIndex = 4;
            this.BtnShop.Text = "Shop";
            this.BtnShop.UseVisualStyleBackColor = true;
            this.BtnShop.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.Location = new System.Drawing.Point(394, 116);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(105, 42);
            this.btnOrders.TabIndex = 5;
            this.btnOrders.Text = "Orders";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // BuyerDash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 450);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.BtnShop);
            this.Controls.Add(this.btnCart);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblName);
            this.Name = "BuyerDash";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Button btnCart;
        private System.Windows.Forms.Button BtnShop;
        private System.Windows.Forms.Button btnOrders;
    }
}