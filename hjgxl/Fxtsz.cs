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
using Microsoft.Win32;
using System.Drawing.Printing;

namespace hjgxl
{
    public partial class Fxtsz : Form
    {
        public Fxtsz()
        {
            InitializeComponent();
            bind();
            GetComList();
            GetPrintList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string conn = Encrypt.Encrypt.Encode("server=" + fwq.Text.Trim() + ";database=" + sjk.Text.Trim() + ";uid=" + yhm.Text.Trim() + ";pwd=" + mm.Text.Trim(), "1", "2");
            // = conn;
            Globals.connstr=Encrypt.Encrypt.Decode(conn, "1", "2");
            Encrypt.Encrypt.saveConfig(conn, "config.xml", "sqlserver");
            MessageBox.Show("保存成功");
            return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connstr = "server=" + fwq.Text.Trim() + ";database=" + sjk.Text.Trim() + ";uid=" + yhm.Text.Trim() + ";pwd=" + mm.Text.Trim();
            // con = new MySqlConnection(conn);
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("联接数据库失败");
                return;
            }
            MessageBox.Show("联接成功");
            conn.Close();
            conn.Dispose();
            return;
        }
        public void bind()
        {
            try
            {
                // string connstr = Encrypt.Encrypt.Decode(Encrypt.Encrypt.getConfig("config.xml", "sqlserver"), "1", "2");
                string[] str1 = Globals.connstr.Split(';');
                fwq.Text = str1[0].Split('=')[1];
                sjk.Text = str1[1].Split('=')[1];
                yhm.Text = str1[2].Split('=')[1];
                mm.Text = str1[3].Split('=')[1];
                try
                {
                    comboBox1.Text = Encrypt.Encrypt.getConfig("config.xml", "p1");
                }
                catch { }
                ck.Text = Encrypt.Encrypt.getConfig("config.xml", "com");
                btl.Text = Encrypt.Encrypt.getConfig("config.xml", "baudrate");
            }
            catch { }
            //ds = new DataSet();
            SqlConnection conn = new SqlConnection( Globals.connstr);
            // conn = new SqlConnection(connstr);
            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("数据库联接失败，请重新设置");
                return;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Encrypt.Encrypt.saveConfig(ck.Text, "config.xml", "com");
            Encrypt.Encrypt.saveConfig(btl.Text, "config.xml", "baudrate");
            MessageBox.Show("保存成功");
            return;
        }
        private void GetComList()
        {
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                // this.comboBox1.Items.Clear();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    this.ck.Items.Add(sValue);
                }
            }
        }
        private void GetPrintList()
        {
           
            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                comboBox1.Items.Add(sPrint);
               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Encrypt.Encrypt.saveConfig(comboBox1.Text, "config.xml", "p1");
        }
    }
}
