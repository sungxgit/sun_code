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
    public partial class Frksj : Form
    {
        SqlConnection conn=new SqlConnection(Globals.connstr);
        DataTable dt;
        R_T sj = new R_T();
        tj gxtj;
        public Frksj()
        {
            InitializeComponent();
        }
        public Frksj(DataTable dt,tj gxtj) {
            InitializeComponent();
            dataGridView1.DataSource = dt;
            this.gxtj = gxtj;
            gxtj.cancel = true;

        
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
           gxtj.gxtj="(";
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["Column1"].Value.ToString() == "True")
                {
                    gxtj.gxtj += "'" + dataGridView1.Rows[i].Cells["条码"].Value + "',";
                   
                }
            }
            gxtj.gxtj = gxtj.gxtj.Substring(0, gxtj.gxtj.Length - 1);
            gxtj.gxtj = gxtj.gxtj + ")";
           
          
            gxtj.cancel = false;
            this.Close();


          
        }

        private void Frksj_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells["Column1"].Value = true;
                dataGridView1.Rows[i].Cells["条码"].ReadOnly = true;
                dataGridView1.Rows[i].Cells["长度"].ReadOnly = true;
                try
                {
                    gxtj.cl = gxtj.cl + decimal.Parse(dataGridView1.Rows[i].Cells["长度"].Value.ToString());
                }
                catch { }
            }
            label2.Text = gxtj.cl.ToString();

            string sql = "select FItemID,fname,FNumber,FSPGroupID from  t_Stock  ";

           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewCheckBoxColumn)
                {
                    //这里可以编写你需要的任意关于按钮事件的操作~
                    try
                    {
                        dataGridView1.EndEdit();
                    }
                    catch { }
                }
                //DGV下拉框的取值
            }




            try
            {
                gxtj.cl = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {

                    try
                    {
                        if (dataGridView1.Rows[i].Cells["Column1"].Value.ToString() == "True")
                        {
                            gxtj.cl = gxtj.cl + decimal.Parse(dataGridView1.Rows[i].Cells["长度"].Value.ToString());
                        }
                    }
                    catch { }
                }
                label2.Text = gxtj.cl.ToString();
            }
            catch { }
        }

      
    }
}
