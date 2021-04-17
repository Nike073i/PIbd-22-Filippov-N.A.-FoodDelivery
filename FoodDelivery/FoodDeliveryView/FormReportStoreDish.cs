using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;

namespace FoodDeliveryView
{
    public partial class FormReportStoreDish : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;

        public FormReportStoreDish(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void FormReportStoreDishes_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = logic.GetStoreDish();
                if (dict != null)
                {
                    dataGridViewReport.Rows.Clear();
                    foreach (var storeDish in dict)
                    {
                        dataGridViewReport.Rows.Add(new object[] {
                            storeDish.StoreName,
                            "",
                            ""
                        });
                        foreach (var dish in storeDish.Dishes)
                        {
                            dataGridViewReport.Rows.Add(new object[] {
                                "",
                                dish.Item1,
                                dish.Item2
                            });
                        }
                        dataGridViewReport.Rows.Add(new object[] {
                            "Итого",
                            "",
                            storeDish.TotalCount
                        });
                        dataGridViewReport.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonSaveToExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveStoreDishToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
