using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace hjgxl
{
    public partial class RK : Form
    {
        SqlConnection conn;
        DataTable dt;
        R_T sj = new R_T();
        tj tj;

        public RK()
        {
            InitializeComponent();
        }
        public RK(tj tj1)
        {
            InitializeComponent();
            string sql = "select FItemID,fname,FNumber,FSPGroupID from  t_Stock  ";
            conn = new SqlConnection(Globals.connstr);
            this.tj = tj1;
            comboBox1.DataSource = dt = sj.ds(sql, "ck", conn);
            comboBox1.DisplayMember = "fname";
            comboBox1.ValueMember = "FSPGroupID";
            
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataRow[] drArr = dt.Select("fname = '" + comboBox1.Text + "'");

            tj.ck = drArr[0]["FItemID"].ToString();
            tj.cw = comboBox2.SelectedValue.ToString();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql1 = "select FSPID, FName,FNumber from  t_StockPlace  where FSPGroupID=" + comboBox1.SelectedValue.ToString();
                comboBox2.DataSource = sj.ds(sql1, "cw", conn);
                comboBox2.DisplayMember = "fname";
                comboBox2.ValueMember = "FSPID";
            }
            catch { }
 

        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string sql1 = "select FSPID, FName,FNumber from  t_StockPlace  where FSPGroupID=" + comboBox1.SelectedValue.ToString();
            //    comboBox2.DataSource = sj.ds(sql1, "cw", conn);
            //    comboBox2.DisplayMember = "fname";
            //    comboBox2.ValueMember = "FSPID";
            //}
            //catch { }
        }
    }
}
