using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;

namespace FoodDeliveryView
{
    public partial class FormStores : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly StoreLogic logic;

        public FormStores(StoreLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }

        private void FormStores_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);
                if (list != null)
                {
                    dataGridViewStores.DataSource = list;
                    dataGridViewStores.Columns[0].Visible = false;
                    dataGridViewStores.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridViewStores.Columns[4].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormStore>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewStores.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormStore>();
                form.Id = Convert.ToInt32(dataGridViewStores.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewStores.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewStores.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        logic.Delete(new StoreBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
