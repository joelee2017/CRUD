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

        private void btnQuery_Click(object sender, EventArgs e)//查詢事件
        {
            NorthwindEntities dc = new NorthwindEntities();
            dataGridView1.DataSource = dc.Employees.Select(emp => new
            {
                編號 = emp.EmployeeID,
                姓 = emp.FirstName,
                名 = emp.LastName,
                職務 = emp.Title,

            }).ToArray();


        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Employees emp = new Employees();
            emp.FirstName = txtFirstName.Text;
            emp.LastName = txtLastName.Text;
            emp.Title = txtTitle.Text;
            NorthwindEntities dc = new NorthwindEntities();
            dc.Employees.Add(emp);
            dc.SaveChanges();
            MessageBox.Show("新增成功");
            btnQuery_Click(sender, e);//查詢事件呼叫

        }

        private void btnUpate_Click(object sender, EventArgs e)//更新事件
        {
            NorthwindEntities dc = new NorthwindEntities();
            Employees empUpdate = dc.Employees.Find(int.Parse(txtUpdateID.Text));
            empUpdate.FirstName = txtUpdateFirstName.Text;
            empUpdate.LastName = txtUpdateLastName.Text;
            empUpdate.Title = txtUpdateTitle.Text;
            dc.Entry(empUpdate).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();
            MessageBox.Show("更新成功");
            btnQuery_Click(sender, e);
        }

        private void btnFind_Click(object sender, EventArgs e)//查詢事件
        {
            NorthwindEntities dc = new NorthwindEntities();
            Employees empFind = dc.Employees.Find(int.Parse(txtUpdateID.Text));
            txtUpdateFirstName.Text = empFind.FirstName;
            txtUpdateLastName.Text = empFind.LastName;
            txtUpdateTitle.Text = empFind.Title;
           

        }

        private void btnDelete_Click(object sender, EventArgs e)//刪除事件
        {
            

            if(MessageBox.Show("確定刪除嗎?", "刪除確認", MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                                ==DialogResult.Yes)
            {
                NorthwindEntities dc = new NorthwindEntities();
                Employees empDeleted = dc.Employees.Find(Convert.ToInt32(txtEmployeeID.Text));
                if (empDeleted == null)
                {
                    MessageBox.Show($"無  {txtEmployeeID.Text}  員工記錄");
                }
                else
                {
                    dc.Employees.Remove(empDeleted);
                    dc.SaveChanges();
                    MessageBox.Show("刪除成功");
                    btnQuery_Click(sender, e);
                }
            }
        }
    }
}
