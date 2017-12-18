using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        NorthwindEntities dc = new NorthwindEntities();


        /// <summary>
        /// Log 產生監視檔
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            dc.Database.Log = Message =>
            {
                using (StreamWriter sw = File.AppendText("EFlog.txt"))
                {
                    sw.WriteLine(Message);
                }
            };
        }
        

        
        private void btnQuery_Click(object sender, EventArgs e)//查詢事件
        {
            dataGridView1.DataSource = dc.Employees.Select(emp => new
            {
                編號 = emp.EmployeeID,
                姓 = emp.FirstName,
                名 = emp.LastName,
                職務 = emp.Title,

            }).ToArray();


        }

        /// <summary>
        /// dc.SaveChanges();儲存至DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Employees emp = new Employees();
            emp.FirstName = txtFirstName.Text;
            emp.LastName = txtLastName.Text;
            emp.Title = txtTitle.Text;

            dc.Employees.Add(emp);
            dc.SaveChanges();
            MessageBox.Show("新增成功");
            btnQuery_Click(sender, e);//查詢事件呼叫

        }

        private void btnUpate_Click(object sender, EventArgs e)//更新事件
        {

            Employees empUpdate = dc.Employees.Find(int.Parse(txtUpdateID.Text));//員工編號需轉int
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

            Employees empFind = dc.Employees.Find(int.Parse(txtUpdateID.Text));//員工編號需轉int
            txtUpdateFirstName.Text = empFind.FirstName;
            txtUpdateLastName.Text = empFind.LastName;
            txtUpdateTitle.Text = empFind.Title;
           

        }


        /// <summary>
        /// MessageBoxButtons.YesNo,MessageBoxIcon.Question
        /// 確認要顯示的事件(YesNo)按鈕，以及(Question)符號
        /// DialogResult.Yes
        /// 表示要傳回yes
        /// dc.Employees.Find(Convert.ToInt32(txtEmployeeID.Text));
        /// (Find)查詢(Convert.ToInt32)轉型的txtEmployeeID.Text)值
        /// dc.Employees.Remove刪除DB資料來源(empDeleted);
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)//刪除事件
        {           

            if(MessageBox.Show("確定刪除嗎?", "刪除確認", MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                                ==DialogResult.Yes)
            {
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
