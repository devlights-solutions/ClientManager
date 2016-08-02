
namespace Framework.Common.Win.UserControls
{
    partial class UcGridPager
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcGridPager));
            this.CbxPageSize = new Telerik.WinControls.UI.RadDropDownList();
            this.LblOf = new Telerik.WinControls.UI.RadLabel();
            this.TxtPageTotal = new Telerik.WinControls.UI.RadTextBox();
            this.MetroBlueTheme = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
            this.BtnLastPage = new Telerik.WinControls.UI.RadButton();
            this.BtnNextPage = new Telerik.WinControls.UI.RadButton();
            this.BtnPreviousPage = new Telerik.WinControls.UI.RadButton();
            this.BtnFirstPage = new Telerik.WinControls.UI.RadButton();
            this.TxtCurrentPage = new Framework.Common.Win.Controls.NumericTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.CbxPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LblOf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPageTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnLastPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNextPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPreviousPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnFirstPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCurrentPage)).BeginInit();
            this.SuspendLayout();
            // 
            // CbxPageSize
            // 
            this.CbxPageSize.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.CbxPageSize.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem1.Text = "10";
            radListDataItem2.Text = "20";
            radListDataItem3.Text = "50";
            radListDataItem4.Text = "100";
            this.CbxPageSize.Items.Add(radListDataItem1);
            this.CbxPageSize.Items.Add(radListDataItem2);
            this.CbxPageSize.Items.Add(radListDataItem3);
            this.CbxPageSize.Items.Add(radListDataItem4);
            this.CbxPageSize.Location = new System.Drawing.Point(312, 0);
            this.CbxPageSize.Margin = new System.Windows.Forms.Padding(0);
            this.CbxPageSize.Name = "CbxPageSize";
            this.CbxPageSize.Size = new System.Drawing.Size(59, 28);
            this.CbxPageSize.TabIndex = 6;
            this.CbxPageSize.ThemeName = "TelerikMetroBlue";
            // 
            // LblOf
            // 
            this.LblOf.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblOf.Location = new System.Drawing.Point(140, 0);
            this.LblOf.Margin = new System.Windows.Forms.Padding(0);
            this.LblOf.Name = "LblOf";
            this.LblOf.Size = new System.Drawing.Size(26, 24);
            this.LblOf.TabIndex = 6;
            this.LblOf.Text = "De";
            this.LblOf.ThemeName = "TelerikMetroBlue";
            // 
            // TxtPageTotal
            // 
            this.TxtPageTotal.Enabled = false;
            this.TxtPageTotal.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPageTotal.Location = new System.Drawing.Point(171, 0);
            this.TxtPageTotal.Margin = new System.Windows.Forms.Padding(0);
            this.TxtPageTotal.Name = "TxtPageTotal";
            this.TxtPageTotal.Size = new System.Drawing.Size(55, 28);
            this.TxtPageTotal.TabIndex = 3;
            this.TxtPageTotal.TabStop = false;
            this.TxtPageTotal.Text = "1";
            this.TxtPageTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtPageTotal.ThemeName = "TelerikMetroBlue";
            // 
            // BtnLastPage
            // 
            this.BtnLastPage.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLastPage.Image = ((System.Drawing.Image)(resources.GetObject("BtnLastPage.Image")));
            this.BtnLastPage.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnLastPage.Location = new System.Drawing.Point(272, 0);
            this.BtnLastPage.Margin = new System.Windows.Forms.Padding(0);
            this.BtnLastPage.Name = "BtnLastPage";
            this.BtnLastPage.Size = new System.Drawing.Size(34, 29);
            this.BtnLastPage.TabIndex = 5;
            this.BtnLastPage.ThemeName = "TelerikMetroBlue";
            // 
            // BtnNextPage
            // 
            this.BtnNextPage.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNextPage.Image = ((System.Drawing.Image)(resources.GetObject("BtnNextPage.Image")));
            this.BtnNextPage.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnNextPage.Location = new System.Drawing.Point(232, 0);
            this.BtnNextPage.Margin = new System.Windows.Forms.Padding(0);
            this.BtnNextPage.Name = "BtnNextPage";
            this.BtnNextPage.Size = new System.Drawing.Size(34, 29);
            this.BtnNextPage.TabIndex = 4;
            this.BtnNextPage.ThemeName = "TelerikMetroBlue";
            // 
            // BtnPreviousPage
            // 
            this.BtnPreviousPage.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("BtnPreviousPage.Image")));
            this.BtnPreviousPage.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnPreviousPage.Location = new System.Drawing.Point(43, 0);
            this.BtnPreviousPage.Margin = new System.Windows.Forms.Padding(0);
            this.BtnPreviousPage.Name = "BtnPreviousPage";
            this.BtnPreviousPage.Size = new System.Drawing.Size(34, 29);
            this.BtnPreviousPage.TabIndex = 1;
            this.BtnPreviousPage.ThemeName = "TelerikMetroBlue";
            // 
            // BtnFirstPage
            // 
            this.BtnFirstPage.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFirstPage.Image = ((System.Drawing.Image)(resources.GetObject("BtnFirstPage.Image")));
            this.BtnFirstPage.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnFirstPage.Location = new System.Drawing.Point(0, 0);
            this.BtnFirstPage.Margin = new System.Windows.Forms.Padding(0);
            this.BtnFirstPage.Name = "BtnFirstPage";
            this.BtnFirstPage.Size = new System.Drawing.Size(34, 29);
            this.BtnFirstPage.TabIndex = 0;
            this.BtnFirstPage.ThemeName = "TelerikMetroBlue";
            // 
            // TxtCurrentPage
            // 
            this.TxtCurrentPage.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCurrentPage.Location = new System.Drawing.Point(83, 0);
            this.TxtCurrentPage.Margin = new System.Windows.Forms.Padding(0);
            this.TxtCurrentPage.Name = "TxtCurrentPage";
            this.TxtCurrentPage.Size = new System.Drawing.Size(55, 28);
            this.TxtCurrentPage.TabIndex = 7;
            this.TxtCurrentPage.TabStop = false;
            this.TxtCurrentPage.Text = "1";
            this.TxtCurrentPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtCurrentPage.ThemeName = "TelerikMetroBlue";
            // 
            // UcGridPager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TxtCurrentPage);
            this.Controls.Add(this.TxtPageTotal);
            this.Controls.Add(this.LblOf);
            this.Controls.Add(this.CbxPageSize);
            this.Controls.Add(this.BtnLastPage);
            this.Controls.Add(this.BtnNextPage);
            this.Controls.Add(this.BtnPreviousPage);
            this.Controls.Add(this.BtnFirstPage);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UcGridPager";
            this.Size = new System.Drawing.Size(370, 29);
            this.Load += new System.EventHandler(this.UcGridPager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CbxPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LblOf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPageTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnLastPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNextPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPreviousPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnFirstPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCurrentPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton BtnFirstPage;
        private Telerik.WinControls.UI.RadButton BtnPreviousPage;
        private Telerik.WinControls.UI.RadButton BtnNextPage;
        private Telerik.WinControls.UI.RadButton BtnLastPage;
        private Telerik.WinControls.UI.RadDropDownList CbxPageSize;
        private Telerik.WinControls.UI.RadLabel LblOf;
        private Telerik.WinControls.UI.RadTextBox TxtPageTotal;
        private Framework.Common.Win.Controls.NumericTextbox TxtCurrentPage;
        private Telerik.WinControls.Themes.TelerikMetroBlueTheme MetroBlueTheme;

    }
}
