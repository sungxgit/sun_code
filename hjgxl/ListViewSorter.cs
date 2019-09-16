using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;



namespace hjgxl
{
    public class ListViewSorter : System.Collections.IComparer
    {
        private int sortIndex;
        private int sortMode;

        public ListViewSorter(int p_SortIndex, int p_SortMode)
        {

            this.sortIndex = p_SortIndex;
            this.sortMode = p_SortMode;
        }
        public int Compare(object x, object y)
        {
            ListViewItem item1, item2;
            item1 = (ListViewItem)x;
            item2 = (ListViewItem)y;
            string strX, strY;
            try
            {

                strX = item1.SubItems[this.sortIndex].ToString();
                strY = item2.SubItems[this.sortIndex].ToString();
                if (this.sortMode == 1)//增序
                {

                    if (string.Compare(strX, strY) < 0)
                    {
                        return -1;
                    }

                    else if (string.Compare(strX, strY) == 0)
                    {
                        return 0;
                    }

                    else
                    {
                        return 1;
                    }
                }

                else
                {
                    if (string.Compare(strX, strY) < 0)
                    {
                        return 1;
                    }
                    else if (string.Compare(strX, strY) == 0)
                    {
                        return 0;
                    }

                    else
                    {
                        return -1;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return -1;
            }
        }
    }
}
