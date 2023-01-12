using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMSI_MFLD.Controls.DgvHelper
{
    /// <summary>
    /// DataGridView二维表头实现类
    /// </summary>
    public class DataGridViewHelper
    {
        #region 类型定义

        /// <summary>
        /// 列头
        /// </summary>
        public struct TopHeader
        {
            /// <summary>
            /// 猎头
            /// </summary>
            /// <param name="index">列索引</param>
            /// <param name="span">合并数量</param>
            /// <param name="text">显示内容</param>
            public TopHeader(int index, int span, string text)
            {
                this.Index = index;
                this.Span = span;
                this.Text = text;
            }
            public int Index;
            public int Span;
            public string Text;
        }

        #endregion

        #region 对象成员

        #region 字段定义

        private int top = 0;
        private int left = 0;
        private int height = 0;
        private int width1 = 0;

        private List<TopHeader> _headers = new List<TopHeader>();

        #endregion

        #region 属性定义

        public List<TopHeader> Headers
        {
            get { return _headers; }
        }

        #endregion

        #region 构造方法
        public DataGridViewHelper(DataGridView gridview)
        {
            gridview.CellPainting += new DataGridViewCellPaintingEventHandler(gridview_CellPainting);
        }

        #endregion

        #region 事件处理

        public void gridview_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            #region 重绘datagridview表头
            DataGridView dgv = (DataGridView)(sender);
            if (e.RowIndex != -1) return;
            foreach (TopHeader item in Headers)
            {
                if (e.ColumnIndex >= item.Index && e.ColumnIndex < item.Index + item.Span)
                {
                    if (e.ColumnIndex == item.Index)
                    {
                        top = e.CellBounds.Top;
                        left = e.CellBounds.Left - 1;
                        height = e.CellBounds.Height;
                    }

                    if (item.Index == 0)
                    {
                        left = e.CellBounds.Left;
                    }
                    int width = 0;
                    for (int i = item.Index; i < item.Span + item.Index; i++)
                    {
                        width += dgv.Columns[i].Width;
                    }
                    Rectangle rect = new Rectangle(left, top, width, e.CellBounds.Height);

                    using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                    {
                        //抹去原来的cell背景
                        e.Graphics.FillRectangle(backColorBrush, rect);
                    }
                    //如果是一列
                    if (item.Span == 1)
                    {
                        using (Pen gridLinePen = new Pen(dgv.GridColor))
                        {
                            e.Graphics.DrawLine(gridLinePen, left, top, left + width, top);
                            width1 = 0;
                            e.Graphics.DrawLine(gridLinePen, left, top, left, top + height);
                            for (int i = item.Index; i < item.Span + item.Index; i++)
                            {
                                width1 += dgv.Columns[i].Width;
                                //e.Graphics.DrawLine(gridLinePen, left + width1, top + height / 2, left + width1, top + height);
                            }
                            SizeF sf = e.Graphics.MeasureString(item.Text, e.CellStyle.Font);
                            float lstr = (width - sf.Width) / 2;
                            float rstr = (height / 2 - sf.Height);
                            //画出文本框
                            if (item.Text != "")
                            {
                                //StringFormat drawFormat = new StringFormat();
                                //drawFormat.Alignment = StringAlignment.Center;
                                e.Graphics.DrawString(item.Text, e.CellStyle.Font,
                                                        new SolidBrush(e.CellStyle.ForeColor),
                                                        left + lstr,
                                                        height / 2,
                                                        StringFormat.GenericDefault);

                            }
                            width = 0;
                            width1 = 0;
                            for (int i = item.Index; i < item.Span + item.Index; i++)
                            {
                                string columnValue = dgv.Columns[i].HeaderText;
                                width1 = dgv.Columns[i].Width;
                                sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                                lstr = (width1 - sf.Width) / 2;
                                rstr = (height / 2 - sf.Height);
                                if (columnValue != "")
                                {
                                    e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                               new SolidBrush(e.CellStyle.ForeColor),
                                                                 left + width + lstr,
                                                                 top + height + rstr,
                                                                 StringFormat.GenericDefault);
                                }
                                width += dgv.Columns[i].Width;
                            }
                        }
                    }
                    else
                    {
                        using (Pen gridLinePen = new Pen(dgv.GridColor))
                        {
                            e.Graphics.DrawLine(gridLinePen, left, top, left + width, top);
                            e.Graphics.DrawLine(gridLinePen, left, top + height / 2, left + width, top + height / 2);
                            width1 = 0;
                            e.Graphics.DrawLine(gridLinePen, left, top, left, top + height);
                            for (int i = item.Index; i < item.Span + item.Index; i++)
                            {
                                width1 += dgv.Columns[i].Width;
                                e.Graphics.DrawLine(gridLinePen, left + width1, top + height / 2, left + width1, top + height);
                            }
                            SizeF sf = e.Graphics.MeasureString(item.Text, e.CellStyle.Font);
                            float lstr = (width - sf.Width) / 2;
                            float rstr = (height / 2 - sf.Height) / 2;
                            //画出文本框
                            if (item.Text != "")
                            {
                                e.Graphics.DrawString(item.Text, e.CellStyle.Font,
                                                           new SolidBrush(e.CellStyle.ForeColor),
                                                             left + lstr,
                                                             top + rstr,
                                                             StringFormat.GenericDefault);
                            }
                            width = 0;
                            width1 = 0;
                            for (int i = item.Index; i < item.Span + item.Index; i++)
                            {
                                string columnValue = dgv.Columns[i].HeaderText;
                                width1 = dgv.Columns[i].Width;
                                sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                                lstr = (width1 - sf.Width) / 2;
                                rstr = (height / 2 - sf.Height) / 2;
                                if (columnValue != "")
                                {
                                    e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                               new SolidBrush(e.CellStyle.ForeColor),
                                                                 left + width + lstr,
                                                                 top + height / 2 + rstr,
                                                                 StringFormat.GenericDefault);
                                }
                                width += dgv.Columns[i].Width;
                            }
                        }
                    }
                    e.Handled = true;
                }
            }
            #endregion
        }

        #endregion

        #endregion

        #region 静态常量定义

        //private static readonly int RowHeight = 36;         //定义行高

        #endregion

        #region 类成员

        /// <summary>
        /// 设置网格控件样式
        /// </summary>
        /// <param name="dataGridView">网格控件对象</param>
        public static void SetStyle(DataGridView dataGridView, DataGridViewAutoSizeColumnsMode autoSizeColumnsMode)
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;//211, 223, 240
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            //dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            //dataGridView.RowTemplate.Height = 36;
            dataGridView.RowTemplate.ReadOnly = true;
            //dataGridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

            #region 启用双缓冲解决大数据闪烁问题

            Type dgvType = dataGridView.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dataGridView, true, null);

            #endregion

            #region 只读、尾行、删除、排序、行高、列宽

            dataGridView.ReadOnly = true;                               //设置为只读
            dataGridView.AllowUserToAddRows = false;                    //禁用自动添加尾行
            dataGridView.AllowUserToDeleteRows = false;                 //禁用删除
            dataGridView.AllowUserToOrderColumns = false;               //禁用排序
            dataGridView.AllowUserToResizeRows = false;                 //禁用调整行高    
            dataGridView.AllowUserToResizeColumns = false;              //禁用调整列宽

            dataGridView.AutoGenerateColumns = false;                   //禁用自动生成列

            #endregion

            #region 只读、尾行、删除、排序、行高、列宽

            dataGridView.ReadOnly = false;                               //设置为只读
            dataGridView.AllowUserToAddRows = false;                    //禁用自动添加尾行
            dataGridView.AllowUserToDeleteRows = false;                 //禁用删除
            dataGridView.AllowUserToOrderColumns = false;               //禁用排序
            dataGridView.AllowUserToResizeRows = false;                 //禁用调整行高    
            dataGridView.AllowUserToResizeColumns = false;              //禁用调整列宽

            dataGridView.AutoGenerateColumns = false;                   //禁用自动生成列

            #endregion

            #region 影响性能

            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;     //禁用调整列宽
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;             //禁用调整行高

            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;     //禁用列标题分行显示
            //dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;                                //禁用列自动调整尺寸
            //dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;                      //列宽调整至单元格的最大宽度（包括标题列单元格）
            dataGridView.AutoSizeColumnsMode = autoSizeColumnsMode;

            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;                                      //禁用行高自动调整

            #endregion

            #region 网格线、选择模式

            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;                //禁用网格线
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView.RowHeadersVisible = false;                                         //隐藏列头
            //dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;           //整行选中
            dataGridView.MultiSelect = false;                                               //禁用选中多行

            #endregion

            #region 设置背景色、字体、字号、行高

            dataGridView.BackgroundColor = System.Drawing.Color.White;                      //设置背景色
            //dataGridView.DefaultCellStyle.Font = new System.Drawing.Font(dataGridView.Font.FontFamily, dataGridView.Font.Size);        //设置字体, 默认为Tahoma
            //dataGridView.ColumnHeadersHeight = 36;                                          //设置列标题高度
            //dataGridView.RowTemplate.Height = 36;                                           //设置行高

            #endregion

            #region 标题样式

            dataGridView.EnableHeadersVisualStyles = false;
            //标题颜色
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor =Color.FromArgb(255, 255, 255);
            //禁止点击标题重新排序。
            for (int i = 0; i < dataGridView.Columns.Count; i++) { dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable; }


            #endregion

            #region 订阅行绘制事件，实现奇偶行变色

            dataGridView.RowPrePaint -= dataGridView_RowPrePaint;
            dataGridView.RowPrePaint += dataGridView_RowPrePaint;

            #endregion
        }

        #region 奇偶行变色

        private static void dataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex % 2 == 0)
                {
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(251, 251, 243);        //设置偶数行背景色
                }
                else
                {
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(242, 246, 251);        //设置奇数行背景色
                }
            }
        }

        #endregion

        #endregion
    }
}
