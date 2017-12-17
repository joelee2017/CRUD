using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            NorthwindEntities dc = new NorthwindEntities();
            dataGridView1.DataSource = dc.Employees.Select(emp => new
            {
                編號 = emp.EmployeeID,
                姓 = emp.FirstName,
                名 = emp.LastName,

            }).ToArray();


        }
    }
}
