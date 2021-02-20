using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.BusinessLogics;
using FoodDeliveryBusinnesLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace FoodDeliveryView
{
    public partial class FormStoreReplenishment : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly StoreLogic storeLogic;

        public FormStoreReplenishment(StoreLogic logic_S,DishLogic logic_D)
        {
            InitializeComponent();
            this.storeLogic = logic_S;
            List<StoreViewModel> list_S = logic_S.Read(null);
            if (list_S != null)
            {
                comboBoxStore.DisplayMember = "StoreName";
                comboBoxStore.ValueMember = "Id";
                comboBoxStore.DataSource = list_S;
                comboBoxStore.SelectedItem = null;
            }

            List<DishViewModel> list_D = logic_D.Read(null);
            if (list_D != null)
            {
                comboBoxDish.DisplayMember = "DishName";
                comboBoxDish.ValueMember = "Id";
                comboBoxDish.DataSource = list_D;
                comboBoxDish.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxStore.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxDish.SelectedValue == null)
            {
                MessageBox.Show("Выберите блюдо", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            storeLogic.AddDishes(new AddDishesToStoreBindingModel
            {
                StoreId = Convert.ToInt32(comboBoxStore.SelectedValue),
                DishId = Convert.ToInt32(comboBoxDish.SelectedValue),
                Count = Convert.ToInt32(textBoxCount.Text)
            });

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
