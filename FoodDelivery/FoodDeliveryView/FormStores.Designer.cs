
namespace FoodDeliveryView
{
    partial class FormStores
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
            this.dataGridViewStores = new System.Windows.Forms.DataGridView();
            this.buttonRef = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonUpd = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStores)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewStores
            // 
            this.dataGridViewStores.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewStores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStores.Location = new System.Drawing.Point(0, 3);
            this.dataGridViewStores.Name = "dataGridViewStores";
            this.dataGridViewStores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStores.Size = new System.Drawing.Size(469, 427);
            this.dataGridViewStores.TabIndex = 14;
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(491, 162);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(116, 27);
            this.buttonRef.TabIndex = 13;
            this.buttonRef.Text = "Обновить";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.ButtonRef_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(491, 114);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(116, 27);
            this.buttonDel.TabIndex = 12;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.ButtonDel_Click);
            // 
            // buttonUpd
            // 
            this.buttonUpd.Location = new System.Drawing.Point(491, 68);
            this.buttonUpd.Name = "buttonUpd";
            this.buttonUpd.Size = new System.Drawing.Size(116, 27);
            this.buttonUpd.TabIndex = 11;
            this.buttonUpd.Text = "Изменить";
            this.buttonUpd.UseVisualStyleBackColor = true;
            this.buttonUpd.Click += new System.EventHandler(this.ButtonUpd_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(491, 23);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(116, 27);
            this.buttonAdd.TabIndex = 10;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // FormStores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 434);
            this.Controls.Add(this.dataGridViewStores);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUpd);
            this.Controls.Add(this.buttonAdd);
            this.Name = "FormStores";
            this.Text = "Склады";
            this.Load += new System.EventHandler(this.FormStores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewStores;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonUpd;
        private System.Windows.Forms.Button buttonAdd;
    }
}