using FoodDeliveryBusinnesLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;

namespace FoodDeliveryView
{
    public partial class FormMails : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly MailLogic logic;

        public FormMails(MailLogic mailLogic)
        {
            logic = mailLogic;
            InitializeComponent();
        }

        private void FormMails_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                Program.ConfigGrid(logic.Read(null), dataGridViewMails);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
