using FoodDeliveryBusinnesLogic.BusinessLogics;
using FoodDeliveryBusinnesLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace FoodDeliveryView
{
    public partial class FormSetDish : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id
        {
            get { return Convert.ToInt32(comboBoxDish.SelectedValue); }
            set { comboBoxDish.SelectedValue = value; }
        }
        public string DishName { get { return comboBoxDish.Text; } }
        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }

        public FormSetDish(DishLogic logic)
        {
            InitializeComponent();
            List<DishViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxDish.DisplayMember = "DishName";
                comboBoxDish.ValueMember = "Id";
                comboBoxDish.DataSource = list;
                comboBoxDish.SelectedItem = null;
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxDish.SelectedValue == null)
            {
                MessageBox.Show("Выберите блюдо", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
