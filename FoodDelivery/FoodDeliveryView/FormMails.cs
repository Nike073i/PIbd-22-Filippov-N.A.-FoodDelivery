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
            try
            {
                var list = logic.Read(null);

                if (list != null)
                {
                    dataGridViewMails.DataSource = list;
                    dataGridViewMails.Columns[0].Visible = false;
                    dataGridViewMails.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
