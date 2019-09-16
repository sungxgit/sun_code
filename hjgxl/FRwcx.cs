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

    public partial class FRwcx : Form
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(Globals.connstr);
        DataTable dt;
        R_T sj = new R_T();
   
        public FRwcx()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql="";
            if (comboBox1.Text == "生产进度")
            {
                sql = "select   FBatchNo '订单',FWork '工序', sum(FMcd) '累计量' from tgx where (FBatchNo like '%" + tj.Text+ "%' or  FICMOBillNo like '%"+tj.Text+"%') group by FWork,FBatchNo";//累计生产量查询
            }
            if (comboBox1.Text == "发料任务")
            {
                sql = "select V_Flrw.FICMOBillNo '任务单号',V_Flrw.FBatchNo '订单号',  FName '物料' from  V_Flrw ,t_ICItem  where FICMOBillNo in(select FBillNo from ICMO  where FStatus=1)  and V_Flrw.FItemID=t_ICItem.FItemID and  (FBatchNo like '%" + tj.Text + "%' or  FICMOBillNo like '%" + tj.Text + "%')";//领料情况查询
            }
            if (comboBox1.Text == "未发料任务")
            {
                sql = "select FBillNo '任务单号',FGMPBatchNo '订单号',FName '产品名称',CJ '任务车间',khmc '客户名称' from v_xdxx  where FBillNo not in(select FICMOBillNo from  V_Flrw) and (FBillNo like '%" + tj.Text + "%' or  FGMPBatchNo like '%" + tj.Text + "%')";
            }
            dataGridView1.DataSource = null;
            try
            {
                dataGridView1.DataSource = sj.ds(sql, "dt", conn);
            }
            catch { }
        }
    }
}
