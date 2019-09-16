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
    public partial class jp : Form
    {

        public jp()
        {
            InitializeComponent();
           
        }

        public void clearjm()
        {

            gctm.Clear();
            mcd.Clear();
           

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



            //DGV下拉框的取值
        }
       

    }





}

