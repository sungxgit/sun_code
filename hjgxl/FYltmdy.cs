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
    public partial class FYltmdy : Form
    {
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataSet ds = new DataSet();
        string tm, tm1,wl1,wl2;
        string cd1, cd2;
        string zl1, zl2, rq, rq1,gys,gys1,ph1,ph2,pici1,pici2;

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            string sql = " select FItemID, FName,fnumber from  v_tyl  where fname like '%" + comboBox1.Text + "%' group by FItemID, FName,fnumber ";
            try { ds.Tables["wl"].Clear(); } catch { }
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            // ds = new DataSet();
            da.Fill(ds, "wl");
            comboBox1.DataSource = ds.Tables["wl"];
            comboBox1.DisplayMember = "FName";
            comboBox1.ValueMember = "FItemID";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tmjh;
            tmjh = "(";
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {

               
                if ((bool)dataGridView1.Rows[i].Cells["delbj"].Value)
                {
                    tmjh += "'" + dataGridView1.Rows[i].Cells["条码"].Value + "',";

                }
            }
            tmjh = tmjh.Substring(0, tmjh.Length - 1);
            tmjh = tmjh + ")";
            try
            {
                Globals.delyl(conn, tmjh);
            }
            catch { }

            string sql;
            if (checkBox1.Checked)
            {
                sql = "select FYltm 条码,FName 物料名,FNumber 物料编号,Fqty 原重量,(FLength-yycd)*FCoefficient 剩余重量,FLength 长度,(FLength-yycd) 剩余长度,Fbatchno '批次号',FRkdh 入库单号,gys 供应商 ,Frkrq 入库日期,hjph 合金牌号,Fxh 箱号 from  v_tyl where frkrq='" + dateTimePicker1.Text + "' and FItemID in( select FItemID from  t_ICItem  where FName like '%" + comboBox1.Text + "%' or  FNumber like '%" + comboBox1.Text + "%') and fxh like '%" + xh.Text.Trim() + "%' and  FYltm like '%" + tmh.Text.Trim() + "%'";
            }
            else
            {
                sql = "select FYltm 条码,FName 物料名,FNumber 物料编号,Fqty 原重量,(FLength-yycd)*FCoefficient 剩余重量,FLength 长度,(FLength-yycd) 剩余长度,Fbatchno '批次号',FRkdh 入库单号,gys 供应商 ,Frkrq 入库日期,hjph 合金牌号,Fxh 箱号 from  v_tyl where FItemID in( select FItemID from  t_ICItem  where FName like '%" + comboBox1.Text + "%' or  FNumber like '%" + comboBox1.Text + "%') and fxh like '%" + xh.Text.Trim() + "%' and  FYltm like '%" + tmh.Text.Trim() + "%'";
            }
            dataGridView1.DataSource = null;
            // dataGridView1.Rows.Clear();
            dataGridView1.DataSource = sj.ds(sql, "tyl", conn);
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells["delbj"].Value = false;
            }

        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //string sql = " select FItemID, FName,fnumber from  v_tyl group by FItemID, FName,fnumber ";
            //try { ds.Tables["wl"].Clear(); } catch { }
            //SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            //// ds = new DataSet();
            //da.Fill(ds, "wl");
            //comboBox1.DataSource = ds.Tables["wl"];
            //comboBox1.DisplayMember = "FName";
            //comboBox1.ValueMember = "FItemID";

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat sfm = new StringFormat();
            RectangleF fw;
            sfm.Alignment = StringAlignment.Center;
            sfm.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(wl1, new Font("黑体", 8), Brushes.Black, 10, 5);
            Image im = barcode128.BarCode.BuildBarCode(tm);
            Rectangle destRect1 = new Rectangle(10, 20, 160, 40);
            e.Graphics.DrawImage(im, destRect1);
            SizeF sf = e.Graphics.MeasureString(tm, new Font("黑体", 15));
            int x = 220 / 2 - (int)sf.Width / 2 + 10;
            int y = 60;
            e.Graphics.DrawString(tm, new Font("黑体", 10), Brushes.Black, x, y);
            e.Graphics.DrawString("长" + cd1 + "m      重" + zl1 + "kg", new Font("黑体", 8), Brushes.Black, 10, 80);
            e.Graphics.DrawString("入库日期:" + rq, new Font("黑体", 8), Brushes.Black, 10, 100);
            e.Graphics.DrawString( gys, new Font("黑体", 7), Brushes.Black, 10, 120);
            e.Graphics.DrawString(ph1, new Font("黑体", 8), Brushes.Black, 10, 130);
            e.Graphics.DrawString(pici1, new Font("黑体", 8), Brushes.Black, 100, 130);
            if (tm1 != "")
            {
              
                im = barcode128.BarCode.BuildBarCode(tm1);
                destRect1 = new Rectangle(210, 20, 160, 40);
                e.Graphics.DrawString(wl2, new Font("黑体", 8), Brushes.Black, 210, 5);
                e.Graphics.DrawImage(im, destRect1);
                sf = e.Graphics.MeasureString(tm1, new Font("黑体", 15));
                x = 610 / 2 - (int)sf.Width / 2 + 10;
                y = 60;
                e.Graphics.DrawString(tm1, new Font("黑体", 10), Brushes.Black, x, y);
                e.Graphics.DrawString("长" + cd2+"m      重" + zl2 + "kg", new Font("黑体", 8), Brushes.Black, 210, 80);
                e.Graphics.DrawString("入库日期:" + rq1, new Font("黑体", 8), Brushes.Black, 210, 100);
                e.Graphics.DrawString( gys1, new Font("黑体", 7), Brushes.Black, 210, 120);
                e.Graphics.DrawString(ph2, new Font("黑体", 8), Brushes.Black, 220, 130);
                e.Graphics.DrawString(pici2, new Font("黑体", 8), Brushes.Black, 300, 130);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1;)
            {
                tm = dataGridView1.Rows[i].Cells["条码"].Value.ToString();
                cd1 =decimal.Parse( dataGridView1.Rows[i].Cells["剩余长度"].Value.ToString()).ToString("#0");
                if (zlkz.Checked) {
                    zl1 = decimal.Parse(dataGridView1.Rows[i].Cells["原重量"].Value.ToString()).ToString("#0.00");

                }
                else
                {
                    zl1 = decimal.Parse(dataGridView1.Rows[i].Cells["剩余重量"].Value.ToString()).ToString("#0.00");
                }
                rq =DateTime.Parse( dataGridView1.Rows[i].Cells["入库日期"].Value.ToString()).ToString("d");
                gys= dataGridView1.Rows[i].Cells["供应商"].Value.ToString();
                wl1= dataGridView1.Rows[i].Cells["物料名"].Value.ToString();
                pici1= dataGridView1.Rows[i].Cells["批次号"].Value.ToString();
                try
                {
                    ph1 = dataGridView1.Rows[i].Cells["合金牌号"].Value.ToString().Split('-')[2];
                }
                catch { ph1 = ""; }
                tm1 = "";

                if ((dataGridView1.RowCount - 1) > i + 1)
                {
                    tm1 =  dataGridView1.Rows[i + 1].Cells["条码"].Value.ToString();
                    wl2 = dataGridView1.Rows[i+1].Cells["物料名"].Value.ToString();
                    cd2 = decimal.Parse(dataGridView1.Rows[i + 1].Cells["剩余长度"].Value.ToString()).ToString("#0");
                    if (zlkz.Checked) {
                        zl2 = decimal.Parse(dataGridView1.Rows[i + 1].Cells["原重量"].Value.ToString()).ToString("#0.00");
                    }
                    else { 
                    zl2 = decimal.Parse(dataGridView1.Rows[i + 1].Cells["剩余重量"].Value.ToString()).ToString("#0.00");}
                    rq1 =DateTime.Parse( dataGridView1.Rows[i + 1].Cells["入库日期"].Value.ToString()).ToString("d");
                    pici2 = dataGridView1.Rows[i+1].Cells["批次号"].Value.ToString();
                    try
                    {
                        ph2 = dataGridView1.Rows[i + 1].Cells["合金牌号"].Value.ToString().Split('-')[2];
                    }
                    catch { ph2 = ""; }
                    gys1 = dataGridView1.Rows[i+1].Cells["供应商"].Value.ToString();
                }
                printDocument1.Print();
                i += 2;
            }

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

            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewCheckBoxColumn)
                {
                    //这里可以编写你需要的任意关于按钮事件的操作~
                    try
                    {
                        dataGridView1.CurrentRow.Cells["delbj"].Value =true;
                    }
                    catch { }
                }
                //DGV下拉框的取值
            }



        }

        R_T sj = new R_T();
        DataTable dt;
        public FYltmdy()
        {
            InitializeComponent();
            printDocument1.PrinterSettings.PrinterName = Encrypt.Encrypt.getConfig("config.xml", "p1");
            printDocument1.PrintController = new System.Drawing.Printing.StandardPrintController();
            //string sql = " select FItemID, FName,fnumber from  v_tyl group by FItemID, FName,fnumber ";
            //try { ds.Tables["wl"].Clear(); } catch { }
            //SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            //// ds = new DataSet();
            //da.Fill(ds, "wl");
            //comboBox1.DataSource = ds.Tables["wl"];
            //comboBox1.DisplayMember = "FName";
            //comboBox1.ValueMember = "FItemID";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            if (checkBox1.Checked)
            {
                sql = "select FYltm 条码,FName 物料名,FNumber 物料编号,Fqty 原重量,(FLength-yycd)*FCoefficient 剩余重量,FLength 长度,(FLength-yycd) 剩余长度,Fbatchno '批次号',FRkdh 入库单号,gys 供应商 ,Frkrq 入库日期,hjph 合金牌号,Fxh 箱号 from  v_tyl where frkrq='" + dateTimePicker1.Text + "' and FItemID in( select FItemID from  t_ICItem  where FName like '%" + comboBox1.Text + "%' or  FNumber like '%" + comboBox1.Text + "%') and fxh like '%" + xh.Text.Trim() + "%' and  FYltm like '%" + tmh.Text.Trim() + "%'";
            }
            else
            {
                sql = "select FYltm 条码,FName 物料名,FNumber 物料编号,Fqty 原重量,(FLength-yycd)*FCoefficient 剩余重量,FLength 长度,(FLength-yycd) 剩余长度,Fbatchno '批次号',FRkdh 入库单号,gys 供应商 ,Frkrq 入库日期,hjph 合金牌号,Fxh 箱号 from  v_tyl where FItemID in( select FItemID from  t_ICItem  where FName like '%" + comboBox1.Text + "%' or  FNumber like '%" + comboBox1.Text + "%') and fxh like '%" + xh.Text.Trim() + "%' and  FYltm like '%" + tmh.Text.Trim() + "%'";
            }
            dataGridView1.DataSource = null;
           // dataGridView1.Rows.Clear();
            dataGridView1.DataSource = sj.ds(sql, "tyl", conn);
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells["delbj"].Value = false;
            }
        }
    }
}
