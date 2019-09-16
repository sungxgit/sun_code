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
    public partial class Ftmzs : Form
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataTable dt;
        R_T sj = new R_T();
        string sql;
        public Ftmzs()
        {
            InitializeComponent();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                sql = "select FBarcode '条码',FWork '工序',FMcd '长度',FJlr '记录人',FJlrq '记录日期',FICMOBillNo '任务单号',FBatchNo '订单号' ,FETime-FBTime '熟化时长' from  tgx  where  FBarcode='" + tm.Text.Trim()+"'";
                try
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = sj.ds(sql, "tb", conn);
                }
                catch { }
                try
                {
                    sql = "select FYctm '原料条码',FBatchNo '原料批次',FSycd '原料使用长度' ,FName '原料名称'  from v_ylgx1 where  FCptm='" + dataGridView1.Rows[0].Cells["条码"].Value.ToString() + "'";
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = sj.ds(sql, "tb1", conn);
                }
                catch { }
            }
        }
        //上查
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                sql = "select FBarcode '条码',FWork '工序',FMcd '长度',FJlr '记录人',FJlrq '记录日期',FICMOBillNo '任务单号',FBatchNo '订单号' ,FETime-FBTime '熟化时长' from  tgx  where  FBarcode  in (select FYctm from V_ylgx1  where Fcptm = '" + dataGridView1.CurrentRow.Cells["条码"].Value.ToString() + "')";
          
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = sj.ds(sql, "tb", conn);
            }
            catch {
                MessageBox.Show("查询已到头");
                dataGridView2.DataSource = null;
            }
            try
            {
                sql = "select FYctm '原料条码',FBatchNo '原料批次',FName '原料名称' ,FCptm '产品条码'  from v_ylgx1 where  FCptm='" + dataGridView1.CurrentRow.Cells["条码"].Value.ToString() + "'";
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = sj.ds(sql, "tb1", conn);
            }
            catch {
                MessageBox.Show("查询已到头");
                dataGridView2.DataSource = null;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                sql = "select FBarcode '条码',FWork '工序',FMcd '长度',FJlr '记录人',FJlrq '记录日期',FICMOBillNo '任务单号',FBatchNo '订单号' ,FETime-FBTime '熟化时长' from  tgx  where  FBarcode in ( select FCptm from  V_ylgx1  where FYctm='" + dataGridView1.CurrentRow.Cells["条码"].Value.ToString() + "') ";

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = sj.ds(sql, "tb", conn);
            }
            catch {
                MessageBox.Show("查询已到头");
                dataGridView2.DataSource = null;
            }
            try
            {
                sql = "select FYctm '原料条码',FBatchNo '原料批次',FName '原料名称' ,FSycd '原料使用长度' ,FCptm '产品条码'  from v_ylgx1 where Fcptm ='" + dataGridView1.CurrentRow.Cells["条码"].Value.ToString() + "'";
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = sj.ds(sql, "tb1", conn);
            }
            catch {
                MessageBox.Show("查询已到头");
                dataGridView2.DataSource = null;
            }

           
          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                sql = "select FYctm '原料条码',FBatchNo '原料批次',FName '原料名称', FSycd '原料使用长度' ,FCptm '产品条码'  from v_ylgx1 where Fcptm ='" + dataGridView1.CurrentRow.Cells["条码"].Value.ToString() + "'";
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = sj.ds(sql, "tb1", conn);
            }
            catch
            {
                MessageBox.Show("查询已到头");
                dataGridView2.DataSource = null;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                sql = "select FBarcode '条码',FWork '工序',FMcd '长度',FJlr '记录人',FJlrq '记录日期',FICMOBillNo '任务单号',FBatchNo '订单号' ,FETime-FBTime '熟化时长' from  tgx  where  FBarcode in ( select FCptm from  V_ylgx1  where FYctm='" + dataGridView2.CurrentRow.Cells["原料条码"].Value.ToString() + "') ";

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = sj.ds(sql, "tb", conn);
            }
            catch
            {
                MessageBox.Show("查询已到头");
                dataGridView2.DataSource = null;
            }
        }

        private void textBox1_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    sql = "select FBarcode '条码',FWork '工序',FMcd '长度',FJlr '记录人',FJlrq '记录日期',FICMOBillNo '任务单号',FBatchNo '订单号' ,FETime-FBTime '熟化时长' from  tgx  where  FBarcode in ( select FCptm from  V_ylgx1  where FYctm='" + yltm.Text.Trim() + "') ";

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = sj.ds(sql, "tb", conn);
                }
                catch
                {
                    MessageBox.Show("查询已到头");
                    dataGridView2.DataSource = null;
                }
            }
        }
    }
}
