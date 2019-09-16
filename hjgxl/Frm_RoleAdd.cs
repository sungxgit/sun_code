using System;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;


namespace hjgxl
{
    public partial class Frm_RoleAdd : Form
    {
        String str;
        //  Frm_Euser euser;
        int UserID;
        //int[] RoleID=new int[2];
        ArrayList RoleID = new ArrayList();
        ArrayList ModuleID = new ArrayList();
        SqlDataAdapter tModuleTA1, tRoleTA1, tUserRoleTA1, tUserModuleTA1, tModule1TA1;
        DataSet dm1 = new DataSet();
        SqlConnection conn = new SqlConnection(Globals.connstr);
        
        public Frm_RoleAdd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "select uid,userid,RoleID from  tUserRole where 1=2";
            try
            {
                dm1.Tables["tUserRole"].Clear();
            }
            catch { }
            tUserRoleTA1 = new SqlDataAdapter(sql, conn);
            tUserRoleTA1.Fill(dm1, "tUserRole");
            for (int i = 0; i < RoleID.Count; i++)
            {
                DataRow dr = dm1.Tables["tUserRole"].NewRow();
                dr["UserID"] = UserID;
                dr["RoleID"] = int.Parse(RoleID[i].ToString());
                dm1.Tables["tUserRole"].Rows.Add(dr);
            }
            try
            {
                SqlCommandBuilder sb = new SqlCommandBuilder(tUserRoleTA1);
                tUserRoleTA1.Update(dm1.Tables["tUserRole"]);
              // Globals.SysLogChangeEvent(this.Name, "UID", dm1.tUserRole);
               // dm1.tUserRole.AcceptChanges();
            }
            catch{ }

            sql = "select uid,userid,ModuleID from tUserModule where 1=2";
            try
            {
                dm1.Tables["tUserModule"].Clear();
            }
            catch { }
            tUserModuleTA1 = new SqlDataAdapter(sql, conn);
            tUserModuleTA1.Fill(dm1, "tUserModule");

            for (int i = 0; i < ModuleID.Count; i++)
            {
                DataRow dr = dm1.Tables["tUserModule"].NewRow();
                dr["UserID"] = UserID;
                dr["ModuleID"] = int.Parse(ModuleID[i].ToString());
                dm1.Tables["tUserModule"].Rows.Add(dr);
            }
            try
            {
                SqlCommandBuilder sb1 = new SqlCommandBuilder(tUserModuleTA1);
                tUserModuleTA1.Update(dm1.Tables["tUserModule"]);
                //  Globals.SysLogChangeEvent(this.Name, "UID", dm1.tUserModule);
                // dm1.tUserModule.AcceptChanges();
            }
            catch { }
            // euser.viewchang();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tModuleTA1.Fill(dm1.tModule);
            if (comboBox1.Text == "模块")
            {
                string sql = "SELECT DISTINCT  ModuleMemo FROM tModule where ModuleID not in(select ModuleID from  tUserModule where UserID='" + UserID+"') ";
                try
                {
                    dm1.Tables["tModule1"].Clear();
                }
                catch { }
                tModule1TA1 = new SqlDataAdapter(sql, conn);
                tModule1TA1.Fill(dm1, "tModule1");
                //tModule1TA1.Fill(dm1.tModule1);
                comboBox2.DataSource = dm1.Tables["tModule1"];
                comboBox2.DisplayMember = "ModuleMemo";
                comboBox2.ValueMember = "ModuleMemo";
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.DataSource = dm1.Tables["trole1"];
                comboBox2.DisplayMember = "RoleName";
                comboBox2.ValueMember = "RoleName";

                //comboBox2.Enabled = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            textBox2.Text = comboBox2.Text;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            button4.Enabled = true;
        }

        public Frm_RoleAdd(int UserID)
        {
            InitializeComponent();
            // this.euser = euser;
            this.UserID = UserID;
            comboBox1.Text = "组";
            string sql = "select RoleID, RoleName, RoleMemo from tRole where RoleID not in(select RoleID from  tUserRole where UserID ='"+UserID+"')";
            tRoleTA1 = new SqlDataAdapter(sql,conn);
            tRoleTA1.Fill(dm1, "trole1");
            comboBox2.DataSource = dm1.Tables["trole1"];
            comboBox2.DisplayMember = "RoleName";
            comboBox2.ValueMember = "RoleName";


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RoleID.Clear();
            ModuleID.Clear();

            //当前光标行的字符串
            try
            {
                str = textBox2.Lines[textBox2.Text.Substring(0, textBox2.SelectionStart).Split('\n').Length - 1];
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("检查字段不能为空", "注意");
                return;
            }
            if (comboBox1.Text == "组")
            {
                try
                {
                    string sql = "SELECT RoleID AS 编号, RoleName AS 名称, RoleMemo AS 备注 FROM tRole where RoleName = '"+comboBox2.Text+"'";
                    tRoleTA1 = new SqlDataAdapter(sql, conn);
                    try
                    {
                        dm1.Tables["tRole"].Clear();
                    }
                    catch { }
                    tRoleTA1.Fill(dm1, "tRole");
                   // tRoleTA1.FillBy(dm1.tRole, str);
                    DataView dv = new DataView(dm1.Tables["tRole"]);
                    
                    sql = "SELECT COUNT(*) hj FROM tUserRole  where UserID='"+UserID+"' and RoleID='"+ dv[0][0].ToString()+"'";
                    tUserRoleTA1 = new SqlDataAdapter(sql, conn);
                    try
                    {
                        dm1.Tables["tUserRole"].Clear();
                    }
                    catch { }
                    tUserRoleTA1.Fill(dm1, "tUserRole");
                    if (dm1.Tables["tUserRole"].Rows[0]["hj"].ToString() != "0")
                    {
                        MessageBox.Show("此用户已在该组中", "失败");
                        return;
                    }
                    RoleID.Add(dv[0][0]);
                    textBox2.Text = comboBox2.Text + "(" + dv[0][2].ToString() + ")";

                }
                catch 
                {
                    MessageBox.Show("此组不存在", "警告");
                    return;
                }

            }//检查模块
            else
            {
                try
                {
                    string sql1 = "SELECT ModuleID AS ID, AppSystem AS 系统名称, ModuleName AS 模块名称, ModulePage AS 模块项, ModuleMemo AS 备注 FROM tModule WHERE  (ModuleMemo = '" + comboBox2.Text+"')";
                    tModuleTA1 = new SqlDataAdapter(sql1, conn);
                    try
                    {
                        dm1.Tables["tModule"].Clear();
                    }
                    catch { }

                    //tModuleTA1.FillBy(dm1.tModule, str, comboBox2.Text);
                    tModuleTA1.Fill(dm1, "tModule");
                    DataView dv = new DataView(dm1.Tables["tModule"]);
                  
                    sql1 = "SELECT COUNT(*) hj FROM tUserModule where (UserID='"+UserID+"') and moduleid='"+ dv[0][0].ToString()+"'";
                    tUserModuleTA1 = new SqlDataAdapter(sql1, conn);
                    try
                    {
                        dm1.Tables["tUserModule"].Clear();
                    }
                    catch { }
                    tUserModuleTA1.Fill(dm1, "tUserModule");
                    if (dm1.Tables["tUserModule"].Rows[0]["hj"].ToString() != "0")
                    {
                        MessageBox.Show("此用户已在该模块中", "失败");
                        return;
                    }
                    ModuleID.Add(dv[0][0]);
                    textBox2.Text = comboBox2.Text + "(" + dv[0][3].ToString() + ")";
                }
                catch (IndexOutOfRangeException)
                {

                    MessageBox.Show("此模块不存在", "警告");
                    return;
                }

            }
            button2.Enabled = true;
        }
    }
}
