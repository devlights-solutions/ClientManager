namespace Framework.Common.Win.Notifications
{
    partial class FrmGenericMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGenericMessage));
            this.btnAceptar = new Telerik.WinControls.UI.RadButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picImagen = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnAceptar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAceptar.Location = new System.Drawing.Point(297, 126);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 24);
            this.btnAceptar.TabIndex = 11;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(157, 31);
            this.lblTitle.Name = "lblTitulo";
            this.lblTitle.Size = new System.Drawing.Size(61, 25);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Titulo";
            // 
            // picImagen
            // 
            this.picImagen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.picImagen.Image = global::Framework.Common.Win.Resources.Resource.Error;
            this.picImagen.Location = new System.Drawing.Point(23, 31);
            this.picImagen.Name = "picImagen";
            this.picImagen.Size = new System.Drawing.Size(100, 93);
            this.picImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImagen.TabIndex = 10;
            this.picImagen.TabStop = false;
            // 
            // lblMensaje
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(159, 58);
            this.lblMessage.MaximumSize = new System.Drawing.Size(240, 0);
            this.lblMessage.Name = "lblMensaje";
            this.lblMessage.Size = new System.Drawing.Size(237, 80);
            this.lblMessage.TabIndex = 12;
            this.lblMessage.Text = resources.GetString("lblMensaje.Text");
            // 
            // FrmMensajeGenerico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(425, 162);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picImagen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmMensajeGenerico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mensaje";
            this.Load += new System.EventHandler(this.FrmMensajeGenerico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnAceptar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnAceptar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picImagen;
        private System.Windows.Forms.Label lblMessage;
    }
}