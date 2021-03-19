
namespace FoodDeliveryView
{
    partial class FormReportSetDishes
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
            this.buttonSaveToExcel = new System.Windows.Forms.Button();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.ColumnDishName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSaveToExcel
            // 
            this.buttonSaveToExcel.Location = new System.Drawing.Point(12, 12);
            this.buttonSaveToExcel.Name = "buttonSaveToExcel";
            this.buttonSaveToExcel.Size = new System.Drawing.Size(181, 25);
            this.buttonSaveToExcel.TabIndex = 0;
            this.buttonSaveToExcel.Text = "Сохранить в Excel";
            this.buttonSaveToExcel.UseVisualStyleBackColor = true;
            this.buttonSaveToExcel.Click += new System.EventHandler(this.ButtonSaveToExcel_Click);
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDishName,
            this.ColumnSetName,
            this.ColumnCount});
            this.dataGridViewReport.Location = new System.Drawing.Point(3, 46);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.Size = new System.Drawing.Size(770, 529);
            this.dataGridViewReport.TabIndex = 1;
            // 
            // ColumnDishName
            // 
            this.ColumnDishName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnDishName.FillWeight = 40F;
            this.ColumnDishName.HeaderText = "Блюдо";
            this.ColumnDishName.Name = "ColumnDishName";
            // 
            // ColumnSetName
            // 
            this.ColumnSetName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnSetName.FillWeight = 40F;
            this.ColumnSetName.HeaderText = "Набор";
            this.ColumnSetName.Name = "ColumnSetName";
            // 
            // ColumnCount
            // 
            this.ColumnCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnCount.FillWeight = 20F;
            this.ColumnCount.HeaderText = "Количество";
            this.ColumnCount.Name = "ColumnCount";
            // 
            // FormReportSetDishes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 574);
            this.Controls.Add(this.dataGridViewReport);
            this.Controls.Add(this.buttonSaveToExcel);
            this.Name = "FormReportSetDishes";
            this.Text = "Блюда по наборам";
            this.Load += new System.EventHandler(this.FormReportSetDishes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveToExcel;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDishName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
    }
}