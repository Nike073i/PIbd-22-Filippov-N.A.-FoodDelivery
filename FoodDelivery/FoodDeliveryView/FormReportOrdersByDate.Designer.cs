
namespace FoodDeliveryView
{
    partial class FormReportOrdersByDate
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReportOrdersByDateViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewerOrders = new Microsoft.Reporting.WinForms.ReportViewer();
            this.buttonMake = new System.Windows.Forms.Button();
            this.buttonSaveToPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ReportOrdersByDateViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportOrdersByDateViewModelBindingSource
            // 
            this.ReportOrdersByDateViewModelBindingSource.DataSource = typeof(FoodDeliveryBusinnesLogic.ViewModels.ReportOrdersByDateViewModel);
            // 
            // reportViewerOrders
            // 
            reportDataSource1.Name = "DataSetOrdersByDate";
            reportDataSource1.Value = this.ReportOrdersByDateViewModelBindingSource;
            this.reportViewerOrders.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerOrders.LocalReport.ReportEmbeddedResource = "FoodDeliveryView.ReportOrdersByDate.rdlc";
            this.reportViewerOrders.Location = new System.Drawing.Point(12, 44);
            this.reportViewerOrders.Name = "reportViewerOrders";
            this.reportViewerOrders.ServerReport.BearerToken = null;
            this.reportViewerOrders.Size = new System.Drawing.Size(638, 394);
            this.reportViewerOrders.TabIndex = 0;
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(386, 12);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(112, 26);
            this.buttonMake.TabIndex = 1;
            this.buttonMake.Text = "Сформировать";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.ButtonMake_Click);
            // 
            // buttonSaveToPdf
            // 
            this.buttonSaveToPdf.Location = new System.Drawing.Point(516, 14);
            this.buttonSaveToPdf.Name = "buttonSaveToPdf";
            this.buttonSaveToPdf.Size = new System.Drawing.Size(134, 22);
            this.buttonSaveToPdf.TabIndex = 2;
            this.buttonSaveToPdf.Text = "В Pdf";
            this.buttonSaveToPdf.UseVisualStyleBackColor = true;
            this.buttonSaveToPdf.Click += new System.EventHandler(this.ButtonToPdf_Click);
            // 
            // FormReportOrdersByDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 448);
            this.Controls.Add(this.buttonSaveToPdf);
            this.Controls.Add(this.buttonMake);
            this.Controls.Add(this.reportViewerOrders);
            this.Name = "FormReportOrdersByDate";
            this.Text = "Отчет по заказам по датам";
            ((System.ComponentModel.ISupportInitialize)(this.ReportOrdersByDateViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerOrders;
        private System.Windows.Forms.Button buttonMake;
        private System.Windows.Forms.Button buttonSaveToPdf;
        private System.Windows.Forms.BindingSource ReportOrdersByDateViewModelBindingSource;
    }
}