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
    public partial class FSh : jp
    {

        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataTable dt, dt_gxl, dt_gxl1;
        R_T sj = new R_T();
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            string sql;
            if (comboBox4.Text == "准备熟化")
            {
                sql = "select FBillNo,FName,khmc,xsdd,fworkshop,fitemid,FNumber,finterid,fqty,FGMPBatchNo from  v_xdxx1 where  fbillno in(select   distinct FICMOBillNo from  tgx  where FMustSh=1)";

            }
            else
            {
                sql = "select FBillNo,FName,khmc,xsdd,fworkshop,fitemid,FNumber,finterid,fqty,FGMPBatchNo from  v_xdxx1 where  fbillno in(select   distinct FICMOBillNo from  tgx  where FMustSh=2)";

            }
            comboBox1.DataSource = dt = sj.ds(sql, "rw", conn);
            comboBox1.DisplayMember = "FBillNo";
            comboBox1.ValueMember = "FBillNo";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drArr = dt.Select("FBillNo = '" + comboBox1.SelectedValue + "'");

                label6.Text = drArr[0]["FGMPBatchNo"].ToString();//订单号
                label8.Text = drArr[0]["Fname"].ToString();//物料
                label7.Text = drArr[0]["khmc"].ToString();//客户名称
                wl.Text = drArr[0]["fitemid"].ToString();
                jlrq.Text = DateTime.Now.ToString();
                // wlid.Text = drArr[0]["fitemid"].ToString();//物料id
                wlid.Text = drArr[0]["fnumber"].ToString().Substring(0, 7);
                ydnm.Text = drArr[0]["finterid"].ToString();
                rwl.Text = drArr[0]["fqty"].ToString();
                ////string  sql = "select  FName from V_Gx  where  fname like '%涂布%' and FNumber='"+wlid.Text+"'";
                //// comboBox3.DataSource = sj.ds(sql, "gx", conn);
                //// comboBox3.DisplayMember = "fname";
                //// comboBox3.ValueMember = "fname";
                ///


                string sql;

                if (comboBox4.Text == "准备熟化")
                { sql = "select FICMOBillNo ,FBatchNo,  FBarcode, DATEDIFF(HOUR, FBTime, GETDATE()) sc from  tgx where FMustSh=" + 1 + " and FICMOBillNo='" + comboBox1.SelectedValue + "'"; }
                else
                {
              
                       sql = "selectFICMOBillNo ,FBatchNo, FBarcode, DATEDIFF(HOUR, FBTime, GETDATE()) sc from  tgx where FMustSh=" + 2 + " and FICMOBillNo='" + comboBox1.SelectedValue + "'  and datediff(hour, FBTime,'" + DateTime.Now + "')>=" + comboBox5.Text;
                }

                try
                {
                    dt_gxl1.Clear();
                }
                catch { }
                dt_gxl1 = sj.ds(sql, "dt_gxl1", conn);
                dataGridView1.DataSource = dt_gxl1;
                




                if (!lgxl())
                {
                    MessageBox.Show("领料失败");
                }
                //string sql = "select ID, FItemID, FBatchNo, FRkdh, FYltm, Fqty, FLength, FLength- yycd kycd , FSyqty, FZt, FICMOBillNo, FLy, FName from  v_tyl  where FItemID in( select Fbase2 from v_gx where  fnumber='" + wlid.Text + "' and fbase1='1002') ";
                //try
                //{
                //    dt_ycl.Clear();
                //}
                //catch { }
                //dt_ycl = sj.ds(sql, "ycl", conn);
            }
            catch { }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & dataGridView1.CurrentCell.ColumnIndex == 0)//是否选择的是第一个单元格
            {

                //验证不通过
                try
                {
                    if (!yanzheng(dataGridView1.CurrentRow.Cells["tm"].Value.ToString(), dataGridView1.CurrentCell.RowIndex))
                    {
                        dataGridView1.CurrentRow.Cells["tm"].Value = "";
                        return;
                    }
                }
                catch {

                    return;

                }

                // dataGridView1.CurrentCell 
                //扫描原料条码  在dt_ycl中 查询
                try
                {
                    if (dt_gxl.Rows.Count != 0)
                    {

                        try
                        {
                            DataRow[] drArr = dt_gxl.Select("Fbarcode = '" + dataGridView1.CurrentCell.Value.ToString().Trim() + "'");

                            //dataGridView1.CurrentRow.Cells["kycd"].Value = drArr[0]["kycd"];//膜长
                            //dataGridView1.CurrentRow.Cells["sycd1"].Value = drArr[0]["kycd"];//膜长
                            //dataGridView1.CurrentCell = dataGridView1[4, dataGridView1.CurrentCell.RowIndex];
                            //dataGridView1.CurrentRow.Cells["mk"].Value = drArr[0]["Fmkd"];
                            //dataGridView1.CurrentRow.Cells["mh"].Value = drArr[0]["Fmhd"];
                            //dataGridView1.CurrentRow.Cells[""].Value = drArr[0][""];
                            //dataGridView1.CurrentRow.Cells[""].Value = drArr[0][""];
                            //dataGridView1.CurrentRow.Cells[""].Value = drArr[0][""];
                            if (comboBox4.Text == "结束熟化")
                            {
                                dataGridView1.CurrentRow.Cells["ljsc"].Value = (DateTime.Now - Convert.ToDateTime(drArr[0]["FBTime"])).ToString();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("错误的条码，请重新扫描");
                            dataGridView1.CurrentRow.Cells["tm"].Value = "";
                        }

                        return;
                    }
                    else
                    {
                        MessageBox.Show("请先做生产领料单");
                        dataGridView1.CurrentRow.Cells["tm"].Value = "";
                    }
                }
                catch {
                    MessageBox.Show("请先选择生产任务");
                    return;
                }


            }
        }
        public Boolean yanzheng(string tm, int r)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (i != r)
                {

                    if (dataGridView1.Rows[i].Cells["tm"].Value.ToString() == tm)
                    {
                        MessageBox.Show("请勿重复扫描");
                        return false;
                    }
                }
            }
            return true;

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string tj = "(";
                for (int i = 0; i < dataGridView1.RowCount - 2; i++)
                {
                    tj += "'" + dataGridView1.Rows[i].Cells["tm"].Value + "',";
                }
                tj += "'" + dataGridView1.Rows[dataGridView1.RowCount - 2].Cells["tm"].Value + "')";
                string updatesql;
                if (comboBox4.Text == "准备熟化")
                {
                    updatesql = "update tgx set FMustSh=2 ,FBTime='" + dateTimePicker1.Text + "'  where FBarcode in " + tj;
                }
                else
                {
                    updatesql = "update tgx set FMustSh=3 ,FETime='" + dateTimePicker1.Text + "'  where FBarcode in " + tj;
                }
                try
                {
                    conn.Open();
                }
                catch { }
                SqlCommand cmd = new SqlCommand(updatesql, conn);
                cmd.ExecuteNonQuery();
                // dataGridView1.DataSource = null;
                try
                {
                    dt_gxl1.Clear();
                }
                catch { }
                dataGridView1.Rows.Clear();
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    //这里可以编写你需要的任意关于按钮事件的操作~
                    try
                    {
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    }
                    catch { }
                }
                //DGV下拉框的取值
            }
        }

 
        public Boolean lgxl()
        {

            try
            {
                string sql;

                if (comboBox4.Text == "准备熟化")
                { sql = "select * from  tgx where FMustSh=" + 1 + " and FICMOBillNo='" + comboBox1.SelectedValue + "'"; }
                else
                {
                    sql = "select * from  tgx where FMustSh=" + 2 + " and FICMOBillNo='" + comboBox1.SelectedValue + "'  and datediff(hour, FBTime,'"+DateTime.Now+"')>="+comboBox5.Text;
                }

                try
                {
                    dt_gxl.Clear();
                }
                catch { }
                dt_gxl = sj.ds(sql, "dt_gxl", conn);
                dataGridView2.DataSource = dt_gxl;
               // MessageBox.Show(dt_gxl.Rows.Count.ToString());
                return true;
            }
            catch
            {

                return false;
            }
        }

        private void comboBox4_DropDown(object sender, EventArgs e)
        {
            try
            {
                dt_gxl1.Clear();
            }
            catch { }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;

                if (comboBox4.Text == "准备熟化")
                { sql = "select FICMOBillNo ,FBatchNo, FBarcode, DATEDIFF(HOUR, FBTime, GETDATE()) sc from  tgx where FMustSh=" + 1 + " and FICMOBillNo like '%" + comboBox1.SelectedValue + "%'"; }
                else
                {

                    sql = "select FICMOBillNo ,FBatchNo, FBarcode, DATEDIFF(HOUR, FBTime, GETDATE()) sc from  tgx where FMustSh=" + 2 + " and FICMOBillNo like '%" + comboBox1.SelectedValue + "%'  and datediff(hour, FBTime,'" + DateTime.Now + "')>=" + comboBox5.Text;
                }

                try
                {
                    dt_gxl1.Clear();
                }
                catch { }
                dt_gxl1 = sj.ds(sql, "dt_gxl1", conn);
                dataGridView1.DataSource = dt_gxl1;





                if (!lgxl())
                {
                    MessageBox.Show("领料失败");
                }
                //string sql = "select ID, FItemID, FBatchNo, FRkdh, FYltm, Fqty, FLength, FLength- yycd kycd , FSyqty, FZt, FICMOBillNo, FLy, FName from  v_tyl  where FItemID in( select Fbase2 from v_gx where  fnumber='" + wlid.Text + "' and fbase1='1002') ";
                //try
                //{
                //    dt_ycl.Clear();
                //}
                //catch { }
                //dt_ycl = sj.ds(sql, "ycl", conn);
            }
            catch { }
        }

        public FSh()
        {
            InitializeComponent();
            panel3.Visible = false;
            panel4.Visible = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                // e.Handled = false;
                return true;
            }

            if (keyData == Keys.Right)
            {
                // e.Handled = false;
                return true;
            }
            if (keyData == Keys.Left)
            {
                // e.Handled = false;
                return true;
            }
            if (keyData == Keys.Up)
            {
                // e.Handled = false;
                return true;
            }
            if (keyData == Keys.Down)
            {
                // e.Handled = false;
                return true;
            }
            if (keyData == Keys.Enter)//屏蔽datagridview回车换行
            {
                if (this.dataGridView1.IsCurrentCellInEditMode)
                {
                    dataGridView1.EndEdit();
                    //  dataGridView1.BeginEdit(true);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);


            //if (keyData == Keys.Enter && dataGridView1.Focused)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

    }
}
