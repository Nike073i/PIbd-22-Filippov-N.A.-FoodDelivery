using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.BusinessLogics;
using FoodDeliveryBusinnesLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace FoodDeliveryView
{
    public partial class FormSet : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly SetLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> setDishes;

        public FormSet(SetLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }

        private void FormSet_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SetViewModel view = logic.Read(new SetBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.SetName;
                        textBoxPrice.Text = view.Price.ToString();
                        setDishes = view.SetDishes;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                setDishes = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (setDishes != null)
                {
                    dataGridViewDishes.Rows.Clear();
                    foreach (var sd in setDishes)
                    {
                        dataGridViewDishes.Rows.Add(new object[] { sd.Key, sd.Value.Item1, sd.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSetDish>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (setDishes.ContainsKey(form.Id))
                {
                    setDishes[form.Id] = (form.DishName, form.Count);
                }
                else
                {
                    setDishes.Add(form.Id, (form.DishName, form.Count));
                }
                LoadData();
            }
        }
        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewDishes.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormSetDish>();
                int id = Convert.ToInt32(dataGridViewDishes.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = setDishes[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    setDishes[form.Id] = (form.DishName, form.Count);
                    LoadData();
                }
            }
        }
        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewDishes.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        setDishes.Remove(Convert.ToInt32(dataGridViewDishes.SelectedRows[0].Cells[0].Value));
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
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (setDishes == null || setDishes.Count == 0)
            {
                MessageBox.Show("Заполните блюда", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new SetBindingModel
                {
                    Id = id,
                    SetName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    SetDishes = setDishes
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
