using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;

namespace hjgxl
{
    public partial class Frm_ModuleAdd : Form
    {
        int RoleID;
        String str;
        SqlConnection conn = new SqlConnection(Globals.connstr);
        ArrayList ModuleID = new ArrayList();
        DataSet dm1 = new DataSet();
        SqlDataAdapter tModuleTA1, tRoleModuleTA1;
        public Frm_ModuleAdd()
        {
            InitializeComponent();
        }
        public Frm_ModuleAdd(int RoleID)
        {
            InitializeComponent();
            //  this.role = role;
            this.RoleID = RoleID;
            string sql = "SELECT DISTINCT  ModuleMemo FROM tModule WHERE ModuleID not in( select ModuleID from  tRoleModule where RoleID='" + RoleID + "')";
            tModuleTA1 = new SqlDataAdapter(sql, conn);
            try
            {
                dm1.Tables["tModule1"].Clear();
            }
            catch { }
            tModuleTA1.Fill(dm1, "tModule1");
            comboBox1.DataSource = dm1.Tables["tModule1"];
            comboBox1.DisplayMember = "ModuleMemo";
            comboBox1.ValueMember = "ModuleMemo";
            

            //tModule1TA1.Fill(dm1.tModule1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                str = textBox1.Lines[textBox1.Text.Substring(0, textBox1.SelectionStart).Split('\n').Length - 1];
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("检查字段不能为空", "注意");
                return;
            }
            try
            {
                string sql = "SELECT ModuleID AS ID, AppSystem AS 系统名称, ModuleName AS 模块名称, ModulePage AS 模块项, ModuleMemo AS 备注 FROM tModule WHERE  ModuleMemo='"+comboBox1.Text+"' ";
                tModuleTA1 = new SqlDataAdapter(sql, conn);

                try
                {
                    dm1.Tables["tModule"].Clear();
                }
                catch { }
                tModuleTA1.Fill(dm1, "tModule");
                // tModuleTA1.FillBy(dm1.tModule, str, comboBox1.Text);
                DataView dv = new DataView(dm1.Tables["tModule"]);
                ModuleID.Clear();
                string sql1 = "SELECT COUNT(*) FROM tRoleModule where roleid='"+ RoleID+"' and moduleid='"+ int.Parse(dv[0][0].ToString())+"'";
                try
                {
                    dm1.Tables["tRoleModule"].Clear();
                }
                catch { }
                tRoleModuleTA1 = new SqlDataAdapter(sql, conn);
                tRoleModuleTA1.Fill(dm1, "tRoleModule");
                if (dm1.Tables["tRoleModule"].Rows[0][0].ToString()== "0")
                {
                    MessageBox.Show("此模块已在该组中存在", "失败");
                    return;
                }

                ModuleID.Add(dv[0][0]);
                textBox1.Text = comboBox1.Text+ "(" + dv[0][3].ToString() + ")";
            }
            catch (IndexOutOfRangeException)
            {

                MessageBox.Show("此模块不存在", "警告");
                return;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "select RoleID, ModuleID from  tRoleModule where 1=2";
            tRoleModuleTA1 = new SqlDataAdapter(sql, conn);
            try
            {
                dm1.Tables["tRoleModule"].Clear();
            }
            catch { }
            tRoleModuleTA1.Fill(dm1, "tRoleModule");

            for (int i = 0; i < ModuleID.Count; i++)
            {
                DataRow dr = dm1.Tables["tRoleModule"].NewRow();
                dr["RoleID"] = RoleID;
                dr["ModuleID"] = int.Parse(ModuleID[i].ToString());
                dm1.Tables["tRoleModule"].Rows.Add(dr);
            }
            try
            {

                SqlCommandBuilder sb1 = new SqlCommandBuilder(tRoleModuleTA1);
                tRoleModuleTA1.Update(dm1.Tables["tRoleModule"]);               
            }
            catch (ArgumentNullException) { }
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
