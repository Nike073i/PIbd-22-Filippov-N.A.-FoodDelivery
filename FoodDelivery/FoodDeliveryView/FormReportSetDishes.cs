using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;

namespace FoodDeliveryView
{
    public partial class FormReportSetDishes : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReportSetDishes(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void FormReportSetDishes_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = logic.GetSetDish();
                if (dict != null)
                {
                    dataGridViewReport.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridViewReport.Rows.Add(new object[] {
                            elem.SetName,
                            "",
                            ""
                        });
                        foreach (var listElem in elem.Dishes)
                        {
                            dataGridViewReport.Rows.Add(new object[] {
                                "",
                                listElem.Item1,
                                listElem.Item2
                            });
                        }
                        dataGridViewReport.Rows.Add(new object[] {
                            "Итого",
                            "",
                            elem.TotalCount
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
                        logic.SaveSetDishesToExcelFile(new ReportBindingModel
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
