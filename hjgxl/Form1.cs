using System;
using System.Windows.Forms;
using System.Data;

namespace hjgxl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //////this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            //////this.skinEngine1.SkinFile = Application.StartupPath + "GlassGreen.ssk";
            for (int i = 0; i < menuStrip1.Items.Count; i++)
            {
                ToolStripMenuItem tm = (ToolStripMenuItem)menuStrip1.Items[i];
                for (int j = 0; j < tm.DropDownItems.Count; j++)
                {
                    tm.DropDownItems[j].Enabled = false;
                }
            }
            MI_Sys_Dl.Text = "登录";
            MI_Sys_Dl.Enabled = true;
            MI_Sys_Xtsz.Enabled = true;
            SysLog.WriteLog("打开软件");
      


        }

        private void 系统信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            AboutBox1 ab = new AboutBox1();
            
            ab.ShowDialog();
        }

        private void ys_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is ys)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            ys ys1 = new ys();
            ys1.MdiParent = this;
            ys1.WindowState = FormWindowState.Maximized;
            ys1.Show();
            
        }

        private void xtsz_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is Fxtsz)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            Fxtsz xtsz = new Fxtsz();
            xtsz.MdiParent = this;
            xtsz.WindowState = FormWindowState.Maximized;
            xtsz.Show();
        }

        private void yltm_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FYltm)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FYltm yltm = new FYltm();
            yltm.MdiParent = this;
            yltm.WindowState = FormWindowState.Maximized;
            yltm.Show();
        }

        private void jp_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is jp)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            Fjp jp = new Fjp
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            jp.Show();
        }

        private void qxgl_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is Fuser)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            Fuser user = new Fuser(this);
            user.MdiParent = this;
            user.WindowState = FormWindowState.Maximized;
            user.Show();
        }





        private void qxfp(DataTable dt)
        {
            for (int i = 0; i < menuStrip1.Items.Count; i++)
            {
                ToolStripMenuItem tm = (ToolStripMenuItem)menuStrip1.Items[i];
                for (int j = 0; j < tm.DropDownItems.Count; j++)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (tm.DropDownItems[j].Name == dt.Rows[k]["ModulePage"].ToString())
                        {
                            tm.DropDownItems[j].Enabled = true;                        
                            break;
                        }
                    }
                }
            }
            //Mi_Clpl.Enabled = true;
            //Mi_Czpl.Enabled = true;
            //Mi_Sppl.Enabled = true;
            //Mi_Blpl.Enabled = true;
            //startbar.Text = "当前用户:" + Globals.user;
            //startbar1.Text = "客户端IP:" + Globals.IP;

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MI_Sys_Dl.Text == "用户注销")
            {
                //关闭所有打开的子窗口
                
                foreach (Form _form in this.MdiChildren)
                {
                    _form.Close();
                }
                
                for (int i = 0; i < menuStrip1.Items.Count; i++)
                {
                    ToolStripMenuItem tm = (ToolStripMenuItem)menuStrip1.Items[i];
                    for (int j = 0; j < tm.DropDownItems.Count; j++)
                    {
                        tm.DropDownItems[j].Enabled = false;
                    }
                }
                MI_Sys_Dl.Text = "登录";
                MI_Sys_Dl.Enabled = true;
                MI_Sys_Xtsz.Enabled = true;
                toolStripStatusLabel1.Text ="";
                //MI_Sys_Xtdl.Enabled = true;
                //MI_Sys_about.Enabled = true;
                //MI_Sys_Exit.Enabled = true;
                //MI_Sys_Cssz.Enabled = true;
            }
            else
            {
                //菜单控制
                Frm_Dl dl = new Frm_Dl();
                dl.ShowDialog();
                if (dl.flag == 0)
                {
                    //Application.Exit();
                    MI_Sys_Xtsz.Enabled = true;
                    return;
                }
                qxfp(dl.temptb);
                dl.temptb.Clear();
                //tmessageTA1.Fill(dm1.tmessage);
                //if (dm1.tmessage.Count > 0)
                //{
                //    APP_Show("Sys-Message");
                //}
                //thread = new Thread(new ThreadStart(rollcaption));
                //// thread.Start();          
                //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
                MI_Sys_Xgmm.Enabled = true;
                MI_Sys_Dl.Text = "用户注销";
                toolStripStatusLabel1.Text = Globals.usernm;
            }


        }

        private void MI_Sys_Tb_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is Ftb)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            Ftb tb = new Ftb();
            tb.MdiParent = this;
            tb.WindowState = FormWindowState.Maximized;
            tb.Show();
        }

        private void MI_Sys_Xgmm_Click(object sender, EventArgs e)
        {
            FRM_Yhgl yhgl = new FRM_Yhgl();
            yhgl.ShowDialog();
        }

        private void MI_Sys_Fh_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FFh)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FFh fh = new FFh();
            fh.MdiParent = this;
            fh.WindowState = FormWindowState.Maximized;
            fh.Show();
        }

        private void MI_Sys_Sh_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FSh)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FSh sh = new FSh();
            sh.MdiParent = this;
            sh.WindowState = FormWindowState.Maximized;
            sh.Show();
        }

        private void MI_Sys_Fq_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FFq)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FFq fq = new FFq();
            fq.MdiParent = this;
            fq.WindowState = FormWindowState.Maximized;
            fq.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (MI_Sys_Dl.Text == "用户注销")
            {
                //关闭所有打开的子窗口

                foreach (Form _form in this.MdiChildren)
                {
                    _form.Close();
                }

                for (int i = 0; i < menuStrip1.Items.Count; i++)
                {
                    ToolStripMenuItem tm = (ToolStripMenuItem)menuStrip1.Items[i];
                    for (int j = 0; j < tm.DropDownItems.Count; j++)
                    {
                        tm.DropDownItems[j].Enabled = false;
                    }
                }
                MI_Sys_Dl.Text = "登录";
                MI_Sys_Dl.Enabled = true;
                MI_Sys_About.Enabled = true;
                //MI_Sys_Xtdl.Enabled = true;
                //MI_Sys_about.Enabled = true;
                //MI_Sys_Exit.Enabled = true;
                //MI_Sys_Cssz.Enabled = true;
            }
            else
            {
                //菜单控制
                Frm_Dl dl = new Frm_Dl();
                dl.ShowDialog();
                if (dl.flag == 0)
                {
                    //  Application.Exit();
                    MI_Sys_Xtsz.Enabled = true;
                    return;
                }
                qxfp(dl.temptb);
                dl.temptb.Clear();
                //tmessageTA1.Fill(dm1.tmessage);
                //if (dm1.tmessage.Count > 0)
                //{
                //    APP_Show("Sys-Message");
                //}
                //thread = new Thread(new ThreadStart(rollcaption));
                //// thread.Start();          
                //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
                MI_Sys_Xgmm.Enabled = true;
                MI_Sys_Dl.Text = "用户注销";
                toolStripStatusLabel1.Text = Globals.usernm;
            }
        }

        private void MI_Sys_Hj_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FHj)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FHj hj = new FHj();
            hj.MdiParent = this;
            hj.WindowState = FormWindowState.Maximized;
            hj.Show();
        }

        private void MI_Sys_Bz_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FDb)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FDb db = new FDb();
            db.MdiParent = this;
            db.WindowState = FormWindowState.Maximized;
            db.Show();
        }

        private void MI_Sys_Yldy_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FYltmdy)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FYltmdy yldy = new FYltmdy();
            yldy.MdiParent = this;
            yldy.WindowState = FormWindowState.Maximized;
            yldy.Show();
        }

        private void MI_Sys_Zd_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FZd)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FZd zd = new FZd();
            zd.MdiParent = this;
            zd.WindowState = FormWindowState.Maximized;
            zd.Show();
        }

        private void MI_Sys__Click(object sender, EventArgs e)
        {

            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FRwcx)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FRwcx rp1 = new FRwcx();
            rp1.MdiParent = this;
            rp1.WindowState = FormWindowState.Maximized;
            rp1.Show();
        }

        private void MI_Sys_Bmd_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is Frpt)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            Frpt rp = new Frpt();
            rp.MdiParent = this;
            rp.WindowState = FormWindowState.Maximized;
            rp.Show();
        }

        private void MI_Sys_Sjxg_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FXggx)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FXggx xggx = new FXggx();
            xggx.MdiParent = this;
            xggx.WindowState = FormWindowState.Maximized;
            xggx.Show();
        }

        private void MI_Sys_Cpzs_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is Ftmzs)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            Ftmzs tmzs = new Ftmzs();
            tmzs.MdiParent = this;
            tmzs.WindowState = FormWindowState.Maximized;
            tmzs.Show();
        }

        private void MI_Sys_Tmbd_Click(object sender, EventArgs e)
        {
            foreach (Form forms in this.MdiChildren)
            {
                if (forms is FTmbd)
                {
                    forms.Focus();
                    forms.WindowState = FormWindowState.Maximized;
                    return;
                }
            }
            FTmbd tmbd = new FTmbd();
            tmbd.MdiParent = this;
            tmbd.WindowState = FormWindowState.Maximized;
            tmbd.Show();
        }
    }
}
