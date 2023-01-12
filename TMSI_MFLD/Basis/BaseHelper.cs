using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMSI_MFLD.Basis
{
    public class BaseHelper
    {
        /// <summary>
        /// 获取控件的绝对位置中的X值
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public static int GetControlAbsoluteXValue(Control ctl)
        {
            if (ctl.Parent == null)
            {
                return ctl.Location.X;
            }
            else
            {
                return ctl.Location.X + GetControlAbsoluteXValue(ctl.Parent);
            }
        }

        /// <summary>
        /// 获取控件的绝对位置中的Y值
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public static int GetControlAbsoluteYValue(Control ctl)
        {
            if (ctl.Parent == null)
            {
                return ctl.Location.Y;
            }
            else
            {
                return ctl.Location.Y + GetControlAbsoluteYValue(ctl.Parent);
            }
        }

        /// <summary>
        /// 返回选中列的坐标(用于选择之后动态生成下拉框和文本输入控件)
        /// </summary>
        /// <param name="dgv">加载的表格</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="width">宽</param>
        /// <param name="hight">高</param>
        public static void DgvCellCoordinate(DataGridView dgv, out int x, out int y, out int width, out int hight)
        {
            x = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, false).Left + dgv.PointToScreen(dgv.Location).X;
            y = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, false).Top + dgv.PointToScreen(dgv.Location).Y;
            width = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, false).Width;
            hight = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, true).Height;
        }

        /// <summary>
        /// 返回控件坐标
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void DgvCoordinate(DataGridView dgv, out int x, out int y)
        {
            x = BaseHelper.GetControlAbsoluteXValue(dgv);
            y = BaseHelper.GetControlAbsoluteYValue(dgv);
        }

        /// <summary>
        /// 返回行号列号
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="Cell"></param>
        /// <param name="Row"></param>
        public static void DgvCellRow(DataGridView dgv, out int Cell, out int Row)
        {
            Cell = dgv.CurrentRow.Index;
            Row = dgv.CurrentCell.ColumnIndex;
        }
    }
}
