using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace hjgxl
{
    class FPt
    {
        System.Drawing.Printing.PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();
        string nr;
        int ys;
        public FPt(string nr)
        {
            this.nr = nr;
            this.ys = 0;
            try
            {
                printDocument1.PrinterSettings.PrinterName = Encrypt.Encrypt.getConfig("config.xml", "p1");
                printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintPage);
                printDocument1.PrintController = new System.Drawing.Printing.StandardPrintController();
                printDocument1.Print();
            }
            catch {
                MessageBox.Show("打印机设置错误，请到系统设置中先设置打印机，并补打标签");
               // return;
            }
        }
        public FPt(string nr, int ys)
        {
            this.nr = nr;
            this.ys = ys;
            try
            {
                printDocument1.PrinterSettings.PrinterName = Encrypt.Encrypt.getConfig("config.xml", "p1");
                printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintPage);
                printDocument1.PrintController = new System.Drawing.Printing.StandardPrintController();
                printDocument1.Print();
            }
            catch {
                MessageBox.Show("打印机设置错误，请到系统设置中先设置打印机，并补打标签");
               // return ;
            }
        }
        private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            if (ys == 0)
            {

                e.Graphics.DrawString(nr.Split('|')[0] + "半成品标签", new Font("黑体", 14, FontStyle.Regular), Brushes.Black, 20, 10);
                e.Graphics.DrawString("任务单号：" + nr.Split('|')[1], new Font("黑体", 9, FontStyle.Regular), Brushes.Black, 1, 20);
                for (int i = 0; i < 50; i++)
                {
                    e.Graphics.DrawString("-", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 20 + 1 * i, 23);
                }

                if (nr.Split('|')[2].Length < 18)
                {
                    e.Graphics.DrawString("产品名称：" + nr.Split('|')[2], new Font("黑体", 9, FontStyle.Regular), Brushes.Black, 1, 30);
                }
                else
                {
                    e.Graphics.DrawString("产品名称：" + nr.Split('|')[2].Substring(0, 18), new Font("黑体", 9, FontStyle.Regular), Brushes.Black, 1, 27);
                    e.Graphics.DrawString(nr.Split('|')[2].Substring(18, nr.Split('|')[2].Length - 18), new Font("黑体", 9, FontStyle.Regular), Brushes.Black, 17, 30);


                }
                for (int i = 0; i < 50; i++)
                {
                    e.Graphics.DrawString("-", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 17 + 1 * i, 33);
                }
                e.Graphics.DrawString("规格(长 X 宽)： " + nr.Split('|')[9] + "m X" + nr.Split('|')[11], new Font("黑体", 9, FontStyle.Regular), Brushes.Black, 1, 36);
                for (int i = 0; i < 45; i++)
                {
                    e.Graphics.DrawString("-", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 25 + 1 * i, 39);
                }
                e.Graphics.DrawString("产品结构：", new Font("黑体", 9, FontStyle.Regular), Brushes.Black, 1, 43);
                e.Graphics.DrawString("次工序：" + nr.Split('|')[10], new Font("黑体", 9, FontStyle.Regular), Brushes.Black, 45, 43);
                Image im = barcode128.BarCode.BuildBarCode(nr.Split('|')[3]);
                Rectangle destRect1 = new Rectangle(4, 48, 60, 10);
                e.Graphics.DrawImage(im, destRect1);
                e.Graphics.DrawString(nr.Split('|')[3], new Font("黑体", 12), Brushes.Black, 16, 58);
                e.Graphics.DrawString("机台/班：" + nr.Split('|')[4] + "/" + nr.Split('|')[8], new Font("黑体", 9, FontStyle.Regular), Brushes.Black, 1, 64);
                e.Graphics.DrawString("填表人：" + nr.Split('|')[5], new Font("黑体", 9, FontStyle.Regular), Brushes.Black, 45, 64);
                e.Graphics.DrawString("标签号(从卷心到卷表)：   标记数：", new Font("黑体", 9, FontStyle.Regular), Brushes.Black, 4, 69);
                for (int i = 0; i < 59; i++)
                {
                    e.Graphics.DrawString("-", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 5 + 1 * i, 71);
                    e.Graphics.DrawString("-", new Font("黑体", 12, FontStyle.Regular), Brushes.Black, 5 + 1 * i, 75);
                    e.Graphics.DrawString("-", new Font("黑体", 12, FontStyle.Regular), Brushes.Black, 5 + 1 * i, 79);
                }
                for (int i = 0; i < 6; i++)
                {
                    e.Graphics.DrawString("|", new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 4, 73 + i);
                    e.Graphics.DrawString("|", new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 10, 73 + i);
                    e.Graphics.DrawString("|", new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 24, 73 + i);
                    e.Graphics.DrawString("|", new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 30, 73 + i);
                    e.Graphics.DrawString("|", new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 44, 73 + i);
                    e.Graphics.DrawString("|", new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 50, 73 + i);
                    e.Graphics.DrawString("|", new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 64, 73 + i);
                }
                e.Graphics.DrawString("1", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 6, 73);
                e.Graphics.DrawString("4", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 6, 77);
                e.Graphics.DrawString("2", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 26, 73);
                e.Graphics.DrawString("5", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 26, 77);
                e.Graphics.DrawString("3", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 46, 73);
                e.Graphics.DrawString("6", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 46, 77);


                Rectangle destRect12 = new Rectangle(20, 82, 40, 8);
                e.Graphics.DrawImage(im, destRect12);
                e.Graphics.DrawString(nr.Split('|')[3], new Font("黑体", 6), Brushes.Black, 30, 91);
                e.Graphics.DrawString("记录时间：" + nr.Split('|')[6], new Font("黑体", 9), Brushes.Black, 4, 93);
                for (int i = 0; i < 40; i++)
                {
                    e.Graphics.DrawString("-", new Font("黑体", 10, FontStyle.Regular), Brushes.Black, 20 + 1 * i, 95);
                }
                e.Graphics.DrawString("质量评定：▢合格；▢不合格；▢待定  质检：" + nr.Split('|')[7], new Font("黑体", 9), Brushes.Black, 2, 97);

            }
            else if (ys == 1)
            {

                e.Graphics.DrawString("华健药包:" + nr.Split('|')[0], new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 5, 1);
                e.Graphics.DrawString(nr.Split('|')[2], new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 5, 4);
                e.Graphics.DrawString("自卷芯起接头个数及位置            个", new Font("黑体", 5, FontStyle.Regular), Brushes.Black, 5, 7);
                e.Graphics.DrawString("1:           M 2           M 3           M", new Font("黑体", 6, FontStyle.Regular), Brushes.Black, 5, 9);
                e.Graphics.DrawString("备注：无接头处为空格，客户如有质量投诉需提供此标签记录", new Font("黑体", 5, FontStyle.Regular), Brushes.Black, 5, 11);
                Image im = barcode128.BarCode.BuildBarCode(nr.Split('|')[1]);
                Rectangle destRect1 = new Rectangle(7, 13, 50, 6);
                e.Graphics.DrawImage(im, destRect1);
                e.Graphics.DrawString(nr.Split('|')[1], new Font("黑体", 6), Brushes.Black, 20, 19);

            }
            else if (ys == 2)
            {
                e.Graphics.DrawString("华健药包", new Font("黑体", 14, FontStyle.Bold), Brushes.Black, 25, 3);
                if (nr.Split('|')[0].Length < 8)
                {
                    e.Graphics.DrawString("客户名称:" + nr.Split('|')[0].Substring(0, 7), new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 5, 8);
                }
                else
                {
                    e.Graphics.DrawString("客户名称:" + nr.Split('|')[0].Substring(0, 7), new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 5, 8);
                    e.Graphics.DrawString(nr.Split('|')[0].Substring(7, nr.Split('|')[0].Length - 7), new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 5, 13);

                }
                //  e.Graphics.DrawString("产品名称:" +nr.Split('|')[1], new Font("黑体", 10, FontStyle.Regular), Brushes.Black,5, 13);


                if (nr.Split('|')[1].Length < 11)
                {
                    e.Graphics.DrawString("产品名称：" + nr.Split('|')[1], new Font("黑体", 10, FontStyle.Bold), Brushes.Black, 5, 18);
                }
                else
                {
                    e.Graphics.DrawString("产品名称：" + nr.Split('|')[1].Substring(0, 10), new Font("黑体", 10, FontStyle.Bold), Brushes.Black, 5, 18);
                    e.Graphics.DrawString(nr.Split('|')[1].Substring(10, nr.Split('|')[1].Length - 10), new Font("黑体", 10, FontStyle.Bold), Brushes.Black, 5, 23);
                }


                e.Graphics.DrawString(nr.Split('|')[2], new Font("黑体", 10, FontStyle.Bold), Brushes.Black, 5, 28);
                e.Graphics.DrawString("国家Ⅰ类药包注册号:" + nr.Split('|')[3], new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 5, 33);
               // e.Graphics.DrawString("国家Ⅰ类药包注册号：国药包字" + nr.Split('|')[3], new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 5, 33);

                e.Graphics.DrawString("生产批号" + nr.Split('|')[5], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 5, 38);
                e.Graphics.DrawString("规格：" + nr.Split('|')[4], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 10, 43);
                e.Graphics.DrawString("数量：" + nr.Split('|')[6], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 10, 48);
                e.Graphics.DrawString("净重：" + nr.Split('|')[7], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 10, 53);
                e.Graphics.DrawString("箱号：" + nr.Split('|')[8], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 10, 58);
                e.Graphics.DrawString("生产日期:" + nr.Split('|')[9], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 5, 63);
                Image im = barcode128.BarCode.BuildBarCode(nr.Split('|')[10]);
                Rectangle destRect1 = new Rectangle(5, 68, 50, 6);
                e.Graphics.DrawImage(im, destRect1);
                e.Graphics.DrawString(nr.Split('|')[10], new Font("黑体", 8), Brushes.Black, 20, 74);
            }
            else if (ys == 3)
            {

                e.Graphics.DrawString("华健药包", new Font("黑体", 14, FontStyle.Bold), Brushes.Black, 25, 3);
                if (nr.Split('|')[0].Length < 8)
                {
                    e.Graphics.DrawString("客户名称:" + nr.Split('|')[0], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 5, 8);
                }
                else
                {
                    e.Graphics.DrawString("客户名称:" + nr.Split('|')[0].Substring(0, 7), new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 5, 8);
                    e.Graphics.DrawString(nr.Split('|')[0].Substring(7, nr.Split('|')[0].Length - 7), new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 5, 13);

                }
                //  e.Graphics.DrawString("产品名称:" +nr.Split('|')[1], new Font("黑体", 10, FontStyle.Regular), Brushes.Black,5, 13);


                if (nr.Split('|')[1].Length < 11)
                {
                    e.Graphics.DrawString("产品名称：" + nr.Split('|')[1], new Font("黑体", 10, FontStyle.Bold), Brushes.Black, 5, 18);
                }
                else
                {
                    e.Graphics.DrawString("产品名称：" + nr.Split('|')[1].Substring(0, 10), new Font("黑体", 10, FontStyle.Bold), Brushes.Black, 5, 18);
                    e.Graphics.DrawString(nr.Split('|')[1].Substring(10, nr.Split('|')[1].Length - 10), new Font("黑体", 10, FontStyle.Bold), Brushes.Black, 5, 23);
                }


                e.Graphics.DrawString(nr.Split('|')[2], new Font("黑体", 10, FontStyle.Bold), Brushes.Black, 5, 28);
                e.Graphics.DrawString("国家Ⅰ类药包注册号:" + nr.Split('|')[3], new Font("黑体", 8, FontStyle.Regular), Brushes.Black, 5, 33);
                e.Graphics.DrawString("生产批号" + nr.Split('|')[5], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 5, 38);
                e.Graphics.DrawString("规格：" + nr.Split('|')[4], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 10, 45);
               // e.Graphics.DrawString("数量：" + nr.Split('|')[6], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 10, 48);
                e.Graphics.DrawString("数量：" + nr.Split('|')[7], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 10, 53);
                e.Graphics.DrawString("箱号：" + nr.Split('|')[8], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 10, 58);
                e.Graphics.DrawString("生产日期:" + nr.Split('|')[9], new Font("黑体", 13, FontStyle.Bold), Brushes.Black, 5, 63);
                Image im = barcode128.BarCode.BuildBarCode(nr.Split('|')[10]);
                Rectangle destRect1 = new Rectangle(5, 68, 50, 6);
                e.Graphics.DrawImage(im, destRect1);
                e.Graphics.DrawString(nr.Split('|')[10], new Font("黑体", 8), Brushes.Black, 20, 74);


             

            }

            else if (ys == 4)
            {



                e.Graphics.PageUnit = GraphicsUnit.Display;


                e.Graphics.DrawString(nr.Split('|')[0], new Font("黑体", 8), Brushes.Black, 10, 5);
                Image im = barcode128.BarCode.BuildBarCode(nr.Split('|')[1]);
                Rectangle destRect1 = new Rectangle(10, 20, 220, 40);
                e.Graphics.DrawImage(im, destRect1);
                SizeF sf = e.Graphics.MeasureString(nr.Split('|')[1], new Font("黑体", 15));
                int x = 280 / 2 - (int)sf.Width / 2 + 10;
                int y = 60;
                e.Graphics.DrawString(nr.Split('|')[1], new Font("黑体", 10), Brushes.Black, x, y);
                e.Graphics.DrawString("长" + nr.Split('|')[2] + "m      重" + nr.Split('|')[3] + "kg", new Font("黑体", 8), Brushes.Black, 10, 80);
                e.Graphics.DrawString("入库日期:" + nr.Split('|')[4], new Font("黑体", 8), Brushes.Black, 10, 100);
                e.Graphics.DrawString(nr.Split('|')[5], new Font("黑体", 7), Brushes.Black, 10, 120);//供应商
                e.Graphics.DrawString(nr.Split('|')[6], new Font("黑体", 8), Brushes.Black, 10, 130);//合金牌号
                e.Graphics.DrawString(nr.Split('|')[7], new Font("黑体", 8), Brushes.Black, 100, 130);

               

            }
        }
    }
}
