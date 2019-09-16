using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace hjgxl
{
    public partial class FXggx : jp
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataTable dt, dt_ycl, dt_gxl, sl;
        R_T sj = new R_T();
        SqlCommand cmd = new SqlCommand();
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!Globals.CheckNumber(dataGridView1.Rows[i].Cells["使用长度"].Value.ToString()))
                {
                    MessageBox.Show("使用长度必须为数字");
                    return;
                }
            }
            if (mcd.Text != "")
            {
                if (!Globals.CheckNumber(mcd.Text.ToString().Trim()))
                {
                    MessageBox.Show("长度必须为数字");
                    mcd.Text = null;
                    return;

                }

            }
            if (mzl.Text != "")
            {
                if (!Globals.CheckNumber(mzl.Text.ToString().Trim()))
                {
                    MessageBox.Show("重量必须为数字");
                    mzl.Text = null;
                    return;

                }

            }

            string sql;
            try { conn.Open(); } catch { }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                sql = "update Tylcpgx set FSycd='"+dataGridView1.Rows[i].Cells["使用长度"].Value.ToString()+ "' where FYctm='"+ dataGridView1.Rows[i].Cells["原料条码"].Value.ToString() + "' and FCptm='"+gctm.Text+"'";
                cmd = new SqlCommand(sql, conn);
               
                cmd.ExecuteNonQuery();
            }
            string nr = "";
            if (mcd.Text == "")
            {
                if (gctm.Text != "")
                {
                    nr = "fqty = '" + mzl.Text + "'";
                }
            }
            else if (mzl.Text == "")
            {
                nr = "fmcd='" + mcd.Text + "' ";

            }
            else {
                nr = " fmcd='" + mcd.Text + "' ,fqty='" + mzl.Text + "'";
            }
            
            sql = "update tgx set "+nr+ " where FBarcode='"+gctm.Text+"'";
            cmd = new SqlCommand(sql, conn);
           
            cmd.ExecuteNonQuery();
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!Globals.CheckNumber(dataGridView1.Rows[i].Cells["使用长度"].ErrorText))
                {
                    MessageBox.Show("使用长度必须为数字!");
                    return;
                }
            }
        }

        Boolean dl = false;//双原料
        public FXggx()
        {
            InitializeComponent();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                string sql = "select * from  tgx where FBarcode='"+cxtm.Text+"'";
                string sql2 = "select * from  V_ylgx1  where   FYctm ='"+cxtm.Text+"'";
                if (sj.ds(sql2, "yl1", conn).Rows.Count > 0) {
                    MessageBox.Show("已经生成后续条码，禁止修改");
                    return;
                }
                else
                {
                    string sql1 = "select FYctm '原料条码',fname '物料',FSycd '使用长度' from  V_ylgx1 where FCptm='" + cxtm.Text + "'";
                    dataGridView1.DataSource = sj.ds(sql1, "yl", conn);

                    dt= sj.ds(sql, "gx", conn);
                    dataGridView1.Columns[0].ReadOnly=true;
                    dataGridView1.Columns[1].ReadOnly = true;
                    mcd.Text = dt.Rows[0]["fmcd"].ToString();
                    comboBox1.Text= dt.Rows[0]["FICMOBillNo"].ToString();
                    label6.Text = dt.Rows[0]["FBatchNo"].ToString();
                    gctm.Text= dt.Rows[0]["FBarcode"].ToString();
                    jtmc.Text= dt.Rows[0]["Fscjt"].ToString();
                    mkd.Text= dt.Rows[0]["FMkd"].ToString();
                    mhd.Text= dt.Rows[0]["FMhd"].ToString();
                    mzl.Text= dt.Rows[0]["FQty"].ToString();
                }

            }
        }
    }
}
